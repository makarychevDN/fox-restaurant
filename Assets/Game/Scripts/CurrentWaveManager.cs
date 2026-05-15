using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class CurrentWaveManager : MonoBehaviour, ITickable
    {
        private RestaurantEncounter encounter;
        private List<(CustomerData, Func<ItemData>)> queue;
        private List<Customer> spawnedCustomers = new();
        private TaskCompletionSource<bool> waveTcs;
        bool waveIsExecuting;

        public void Init(RestaurantEncounter encounter)
        {
            this.encounter = encounter;
            encounter.Ticker.AddTickable(this);
        }

        public async Task<bool> ExecuteWave(params (CustomerData, Func<ItemData>)[] customersAndTheirOrders)
        {
            queue = customersAndTheirOrders.ToList();

            int initSpawnCount = 4;
            for(int i = 0; i < initSpawnCount; i++)
            {
                await Task.Delay(500);
                SpawnCustomer();
            }

            waveIsExecuting = true;
            waveTcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            bool result = await waveTcs.Task;
            return result;
        }

        public void Tick(float deltaTime)
        {
            if (!waveIsExecuting || queue.Count == 0)
                return;

            if (encounter.CustomerSpawner.IsPossibleToSpawnCustomer)
            {
                SpawnCustomer();
            }
        }

        private void SpawnCustomer()
        {
            var customer = encounter.CustomerSpawner.TryToSpawnCustomer(queue[0].Item1, queue[0].Item2);
            spawnedCustomers.Add(customer);
            customer.OnCustomerLeft.AddListener(CustomerLeftHandler);
            customer.OnLeftSatisfied.AddListener(TryToFinishWave);
            queue.RemoveAt(0);
        }

        private void TryToFinishWave(bool customerSatisfaction)
        {
            if (customerSatisfaction)
            {
                TryCompleteSuccess();
            }
            else
            {
                AbortWave();
            }
        }

        private void CustomerLeftHandler(Customer customer)
        {
            spawnedCustomers.Remove(customer);
            customer.OnLeftSatisfied.RemoveListener(TryToFinishWave);
            customer.OnCustomerLeft.RemoveListener(CustomerLeftHandler);
        }

        private void AbortWave()
        {
            if (!waveIsExecuting)
                return;

            waveIsExecuting = false;
            waveTcs?.TrySetResult(false);
        }

        private void TryCompleteSuccess()
        {
            if (queue.Count != 0 || spawnedCustomers.Count != 0)
                return;

            waveIsExecuting = false;
            waveTcs?.TrySetResult(true);
        }
    }
}