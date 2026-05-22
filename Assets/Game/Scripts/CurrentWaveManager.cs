using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace foxRestaurant
{
    public class CurrentWaveManager : MonoBehaviour, ITickable
    {
        [SerializeField] private float patienceOfCustomerBeforeSpawn = 40;
        private RestaurantEncounter encounter;
        private List<(CustomerData, Func<ItemData>)> queue;
        private List<Customer> spawnedCustomers = new();
        private TaskCompletionSource<bool> waveTcs;
        bool waveIsExecuting;
        private float nextCustomersPatienceTimer;
        private int fedCustomersCount;
        private int customersToFeedCount;

        public UnityEvent<CustomerData> OnNextCustomerUpdated;
        public UnityEvent<float> OnNextCustomersPatienceUpdated;
        public UnityEvent<int, int> OnFedCustomersCountUpdated;

        public void Init(RestaurantEncounter encounter)
        {
            this.encounter = encounter;
            encounter.Ticker.AddTickable(this);
        }

        public async Task DoWaveTillComplete(WaveConfig waveConfig)
        {
            bool success = false;
            while (!success)
            {
                success = await ExecuteWave(waveConfig);

                if (!success)
                    await ExecuteTasksList(waveConfig.OnFail);
            }
        }

        public async Task<bool> ExecuteWave(WaveConfig waveConfig)
        {
            encounter.ItemSpawnTimer.Pause();
            encounter.Ticker.Pause();
            encounter.BlockInput();
            queue = new List<(CustomerData, Func<ItemData>)>(waveConfig.Customers.ToList());

            fedCustomersCount = 0;
            customersToFeedCount = waveConfig.CustomersToFeed <= -1 ?
                customersToFeedCount = queue.Count - (encounter.SeatPlacesManager.SeatPlaces.Count - 1)
                : waveConfig.CustomersToFeed;
            customersToFeedCount = Math.Clamp(customersToFeedCount, 0, queue.Count);
            OnFedCustomersCountUpdated.Invoke(fedCustomersCount, customersToFeedCount);

            await ExecuteTasksList(waveConfig.BeforeWave);

            int initSpawnCount = Math.Min(queue.Count, encounter.SeatPlacesManager.FreeSeatPlaces.Count);
            for(int i = 0; i < initSpawnCount; i++)
            {
                await Task.Delay(500);
                SpawnCustomer();
                RefreshDataAfterCustomerSpawned();
            }

            await ExecuteTasksList(waveConfig.AfterInitSpawn);

            encounter.ItemSpawnTimer.Unpause();
            encounter.Ticker.SetRegularTickingSpeed();
            encounter.UnblockInput();
            waveIsExecuting = true;
            waveTcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            bool success = await waveTcs.Task;
            await FinishTheRestOfWave(success);
            return success;
        }

        public void Tick(float deltaTime)
        {
            if (!waveIsExecuting || queue.Count == 0)
                return;

            nextCustomersPatienceTimer -= deltaTime;
            OnNextCustomersPatienceUpdated.Invoke(nextCustomersPatienceTimer);

            if(nextCustomersPatienceTimer <= 0)
                AbortWave();

            if (encounter.CustomerSpawner.IsPossibleToSpawnCustomer)
            {
                SpawnCustomer();
                RefreshDataAfterCustomerSpawned();
            }
        }

        private async Task ExecuteTasksList(Func<Task>[] tasksBeforeWaveExecution)
        {
            for (int i = 0; i < tasksBeforeWaveExecution.Length; i++)
            {
                await tasksBeforeWaveExecution[i]();
            }
        }

        private void SpawnCustomer()
        {
            var customer = encounter.CustomerSpawner.TryToSpawnCustomer(queue[0].Item1, queue[0].Item2);
            spawnedCustomers.Add(customer);
            customer.OnCustomerLeftSatisfied.AddListener(CustomerLeftSatisfiedHandler);
            queue.RemoveAt(0);
        }

        private void RefreshDataAfterCustomerSpawned()
        {
            OnNextCustomerUpdated.Invoke(queue.Count == 0 ? null : queue[0].Item1);
            nextCustomersPatienceTimer = patienceOfCustomerBeforeSpawn;
            OnNextCustomersPatienceUpdated.Invoke(nextCustomersPatienceTimer);
        }

        private void CustomerLeftSatisfiedHandler(Customer customer, bool isSatisfied)
        {
            spawnedCustomers.Remove(customer);
            customer.OnCustomerLeftSatisfied.RemoveListener(CustomerLeftSatisfiedHandler);

            if (isSatisfied)
            {
                fedCustomersCount++;
                OnFedCustomersCountUpdated.Invoke(fedCustomersCount, customersToFeedCount);
                TryCompleteSuccess();
            }
            else
            {
                AbortWave();
            }
        }

        private async Task FinishTheRestOfWave(bool success)
        {
            encounter.BlockInput();

            if (success)
            {
                var customers = new List<Customer>(encounter.CustomersManager.Customers.Where(customer => !customer.IsLeaving)); 
                foreach (var customer in customers)
                {
                    await Task.Delay(250);
                    customer.AutoSatisfy();
                }

                await Task.Delay(3000);
            }

            else
            {
                encounter.Ticker.SetX40TickingSpeed();

                while (encounter.SeatPlacesManager.FreeSeatPlaces.Count !=
                encounter.SeatPlacesManager.SeatPlaces.Count)
                {
                    await Task.Delay(50);
                }
            }

            encounter.Ticker.Pause();
            encounter.UnblockInput();
        }

        private void TryCompleteSuccess()
        {
            if (fedCustomersCount < customersToFeedCount)
                return;

            waveIsExecuting = false;
            waveTcs?.TrySetResult(true);
            encounter.ItemSpawnTimer.Pause();
        }

        private void AbortWave()
        {
            waveIsExecuting = false;
            waveTcs?.TrySetResult(false);
            encounter.ItemSpawnTimer.Pause();
        }
    }

    public class WaveConfig
    {
        public Func<Task>[] BeforeWave { get; set; } = Array.Empty<Func<Task>>();

        public Func<Task>[] AfterInitSpawn { get; set; } = Array.Empty<Func<Task>>();

        public Func<Task>[] OnFail { get; set; } = Array.Empty<Func<Task>>();

        public List<(CustomerData, Func<ItemData>)> Customers { get; set; }
            = new List<(CustomerData, Func<ItemData>)>();

        public int CustomersToFeed = -1;
    }
}