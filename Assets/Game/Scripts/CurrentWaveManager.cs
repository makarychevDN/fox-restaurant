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

        public async Task DoGenericWave(List<Func<Task>> tasksBeforeWaveExecution, List<Func<Task>> tasksAfterWaveInitSpawn, List<Func<Task>> tasksOnWaveFailed, params (CustomerData, Func<ItemData>)[] customersAndTheirOrders)
        {
            bool success = false;
            while (!success)
            {
                success = await ExecuteWave(tasksBeforeWaveExecution, tasksAfterWaveInitSpawn, customersAndTheirOrders);

                if (!success)
                    await ExecuteTasksList(tasksOnWaveFailed);
            }
        }

        public async Task<bool> ExecuteWave(List<Func<Task>> tasksBeforeWaveExecution, List<Func<Task>> tasksAfterWaveInitSpawn, params (CustomerData, Func<ItemData>)[] customersAndTheirOrders)
        {
            encounter.ItemSpawnTimer.Pause();
            encounter.Ticker.Pause();
            encounter.BlockInput();
            queue = customersAndTheirOrders.ToList();

            customersToFeedCount = queue.Count - (encounter.SeatPlacesManager.SeatPlaces.Count - 1);
            fedCustomersCount = 0;
            OnFedCustomersCountUpdated.Invoke(fedCustomersCount, customersToFeedCount);

            await ExecuteTasksList(tasksBeforeWaveExecution);

            int initSpawnCount = Math.Min(customersAndTheirOrders.Length, encounter.SeatPlacesManager.FreeSeatPlaces.Count);
            for(int i = 0; i < initSpawnCount; i++)
            {
                await Task.Delay(500);
                SpawnCustomer();
                RefreshDataAfterCustomerSpawned();
            }

            await ExecuteTasksList(tasksAfterWaveInitSpawn);

            encounter.ItemSpawnTimer.Unpause();
            encounter.Ticker.SetRegularTickingSpeed();
            encounter.UnblockInput();
            waveIsExecuting = true;
            waveTcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            bool success = await waveTcs.Task;
            await FinishTheRestOfWave(success);
            return success;
        }

        public async Task<bool> ExecuteWave(params (CustomerData, Func<ItemData>)[] customersAndTheirOrders)
            => await ExecuteWave(new List<Func<Task>>(), new List<Func<Task>>(), customersAndTheirOrders);

        public async Task<bool> ExecuteWave(Func<Task> taskBeforeWaveExecution, Func<Task> taskAfterWaveInitSpawn, params (CustomerData, Func<ItemData>)[] customersAndTheirOrders)
            => await ExecuteWave(
                new List<Func<Task>>() { taskBeforeWaveExecution }, 
                new List<Func<Task>>() { taskAfterWaveInitSpawn }, 
                customersAndTheirOrders
            );

        public async Task<bool> ExecuteWave(List<Func<Task>> tasksBeforeWaveExecution, Func<Task> taskAfterWaveInitSpawn, params (CustomerData, Func<ItemData>)[] customersAndTheirOrders)
            => await ExecuteWave(
                tasksBeforeWaveExecution,
                new List<Func<Task>>() { taskAfterWaveInitSpawn },
                customersAndTheirOrders
        );

        public async Task<bool> ExecuteWave(Func<Task> taskBeforeWaveExecution, List<Func<Task>> tasksAfterWaveInitSpawn, params (CustomerData, Func<ItemData>)[] customersAndTheirOrders)
            => await ExecuteWave(
                new List<Func<Task>>() { taskBeforeWaveExecution },
                tasksAfterWaveInitSpawn,
                customersAndTheirOrders
);

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

        private async Task ExecuteTasksList(List<Func<Task>> tasksBeforeWaveExecution)
        {
            for (int i = 0; i < tasksBeforeWaveExecution.Count; i++)
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
}