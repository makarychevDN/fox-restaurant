using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace foxRestaurant
{
    public class CurrentWaveManager : MonoBehaviour, ITickable
    {
        [SerializeField] private float patienceOfCustomerBeforeSpawn = 40;
        private RestaurantEncounter encounter;
        private List<QueuedCustomer> queue;
        private List<Customer> spawnedCustomers = new();
        private UniTaskCompletionSource<bool> waveTcs;
        bool waveIsExecuting;
        private float nextCustomersPatienceTimer;
        private int fedCustomersCount;
        private int customersToFeedCount;

        public UnityEvent<CustomerData> OnNextCustomerUpdated;
        public UnityEvent<float> OnNextCustomersPatienceUpdated;
        public UnityEvent<int> OnFedCustomersCountUpdated;
        public UnityEvent<int> OnCustomersToFeedCountUpdated;
        public UnityEvent OnNextCustomersTimeIsUp;

        public void Init(RestaurantEncounter encounter)
        {
            this.encounter = encounter;
            encounter.Ticker.AddTickable(this);
        }

        public async UniTask DoWaveTillComplete(WaveConfig waveConfig)
        {
            bool success = false;
            while (!success)
            {
                success = await ExecuteWave(waveConfig);

                if (!success)
                    await ExecuteTasksList(BuildOnFailTasks(waveConfig));
            }
        }

        public async UniTask<bool> ExecuteWave(WaveConfig waveConfig)
        {
            encounter.ItemSpawnTimer.Pause();
            encounter.Ticker.Pause();
            encounter.BlockInput();
            queue = waveConfig.Customers.OrderBy(x => x.SeatPlace == null).ToList();

            fedCustomersCount = 0;
            customersToFeedCount = waveConfig.CustomersToFeed <= -1 ?
                customersToFeedCount = queue.Count - encounter.SeatPlacesManager.SeatPlaces.Count
                : waveConfig.CustomersToFeed;
            customersToFeedCount = Math.Clamp(customersToFeedCount, 0, queue.Count);
            OnCustomersToFeedCountUpdated.Invoke(customersToFeedCount);
            OnFedCustomersCountUpdated.Invoke(fedCustomersCount);

            await ExecuteTasksList(waveConfig.BeforeWave);

            int initSpawnCount = Math.Min(queue.Count, encounter.SeatPlacesManager.FreeSeatPlaces.Count);
            for (int i = 0; i < initSpawnCount; i++)
            {
                SpawnCustomer();
                RefreshDataAfterCustomerSpawned();
                await UniTask.Delay(500, cancellationToken: destroyCancellationToken);
            }

            await ExecuteTasksList(waveConfig.AfterInitSpawn);

            encounter.ItemSpawnTimer.Unpause();
            encounter.Ticker.SetRegularTickingSpeed();
            encounter.UnblockInput();
            waveIsExecuting = true;
            waveTcs = new UniTaskCompletionSource<bool>();
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
            {
                OnNextCustomersTimeIsUp.Invoke();
                AbortWave();
            }

            if (encounter.CustomerSpawner.IsPossibleToSpawnCustomer)
            {
                SpawnCustomer();
                RefreshDataAfterCustomerSpawned();
            }
        }

        private async UniTask ExecuteTasksList(Func<UniTask>[] tasks)
        {
            try
            {
                for (int i = 0; i < tasks.Length; i++)
                {
                    await tasks[i]();
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
        }

        private Func<UniTask>[] BuildOnFailTasks(WaveConfig config)
        {
            if (config.OnFail.Length > 0)
                return config.OnFail;

            return new Func<UniTask>[]
            {
                () => encounter.TheMainCharacter.Say(encounter.DefaultFailurePhrase)
            };
        }

        private void SpawnCustomer()
        {
            var queuedCustomer = queue[0];
            Func<ItemData> actualFactory = queuedCustomer.OrderFactory;
            if (actualFactory == null)
                actualFactory = encounter.DecksManager.GetRandomDish;

            var spawnedCustomer = queuedCustomer.SeatPlace == null ?
                encounter.CustomerSpawner.TryToSpawnCustomer(queuedCustomer.CustomerData, actualFactory) :
                encounter.CustomerSpawner.SpawnCustomer(queuedCustomer.SeatPlace, queuedCustomer.CustomerData, actualFactory);
            spawnedCustomers.Add(spawnedCustomer);
            spawnedCustomer.OnCustomerLeftSatisfied.AddListener(CustomerLeftSatisfiedHandler);
            queue.RemoveAt(0);
        }

        private void RefreshDataAfterCustomerSpawned()
        {
            OnNextCustomerUpdated.Invoke(queue.Count == 0 ? null : queue[0].CustomerData);
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
                OnFedCustomersCountUpdated.Invoke(fedCustomersCount);
                TryCompleteSuccessfully();
            }
            else
            {
                AbortWave();
            }
        }

        private async UniTask FinishTheRestOfWave(bool success)
        {
            encounter.BlockInput();

            if (success)
            {
                OnNextCustomerUpdated.Invoke(null);
                int customersCount = encounter.CustomersManager.Customers.Count;
                var customers = new List<Customer>(encounter.CustomersManager.Customers.Where(customer => !customer.IsLeaving));
                foreach (var customer in customers)
                {
                    await UniTask.Delay(250, cancellationToken: destroyCancellationToken);
                    customer.AutoSatisfy();
                }

                //if there are still customers on the map wait till the go away after autosatisfying
                await UniTask.Delay(customersCount > 0 ? 3000 : 500, cancellationToken: destroyCancellationToken);
            }

            else
            {
                encounter.Ticker.SetX40TickingSpeed();

                while (encounter.SeatPlacesManager.FreeSeatPlaces.Count !=
                encounter.SeatPlacesManager.SeatPlaces.Count)
                {
                    await UniTask.Delay(50, cancellationToken: destroyCancellationToken);
                }
            }

            encounter.Ticker.Pause();
            encounter.UnblockInput();
        }

        private void TryCompleteSuccessfully()
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
        public Func<UniTask>[] BeforeWave { get; set; } = Array.Empty<Func<UniTask>>();

        public Func<UniTask>[] AfterInitSpawn { get; set; } = Array.Empty<Func<UniTask>>();

        public Func<UniTask>[] OnFail { get; set; } = Array.Empty<Func<UniTask>>();

        public List<QueuedCustomer> Customers { get; set; } = new List<QueuedCustomer>();

        public int CustomersToFeed = -1;
    }

    public class QueuedCustomer
    {
        public CustomerData CustomerData { get; set; }
        public Func<ItemData> OrderFactory { get; set; }
        public SeatPlace SeatPlace { get; set; }

        public QueuedCustomer(CustomerData customerData)
        {
            CustomerData = customerData;
        }

        public QueuedCustomer() { }
    }
}