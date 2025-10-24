using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class PirateRestaurantScenarioPart1 : BaseScenario<RestaurantEncounter>
    {
        [SerializeField] private ItemData iceCreamConeData;
        [SerializeField] private ItemData popsicleData;
        [SerializeField] private ItemData iceCreamPlateData;
        [SerializeField] private ItemData cherrySyrupData;
        [SerializeField] private ItemData cherryIceCreamPlateData;

        [SerializeField] private CustomerData doggo;
        [SerializeField] private CustomerData kitty;
        [SerializeField] private CustomerData duck;

        [SerializeField] private List<ItemSlot> itemSlots;

        [SerializeField] private Character redTheCook;

        private TaskCompletionSource<bool> completionSource = new();
        private List<Customer> customersToFeed = new();

        protected override async Task StartScenarioTyped(RestaurantEncounter encounter)
        {
            itemSlots = encounter.SlotsManager.Slots.Where(slot => slot.RequiredItemsType == ItemType.Food && slot.gameObject.activeSelf).ToList();

            await Task.Delay(500);
            await redTheCook.Say("*sigh*<pause:1> Another day, another dollar");

            await FirstWave(encounter);

            await completionSource.Task;
        }

        private async Task FirstWave(RestaurantEncounter encounter)
        {
            bool success = false;
            while (!success)
            {
                for (int i = 0; i < 2; i++)
                {
                    await Task.Delay(500);
                    encounter.ItemsSpawner.SpawnFoodItem(encounter, iceCreamConeData, itemSlots[i + 1]);
                }

                for (int i = 0; i < 2; i++)
                {
                    await Task.Delay(500);

                    var customer = encounter.CustomerSpawner.TryToSpawnCustomer(
                        encounter.DecksManager.GetRandomCustomer(),
                        () => iceCreamConeData
                    );

                    customersToFeed.Add(customer);
                }

                var tasks = customersToFeed.Select(WaitForCustomerToLeave).ToArray();
                var results = await Task.WhenAll(tasks);

                success = results.All(r => r);
                if (!success)
                    await redTheCook.Say("damn...<pause:1> ok, let's try again");
                else
                    await redTheCook.Say("Hell yeah!");
            }
        }

        private Task<bool> WaitForCustomerToLeave(Customer customer)
        {
            var tcs = new TaskCompletionSource<bool>();

            void OnLeftHandler(bool wasSatisfied)
            {
                customer.OnLeft.RemoveListener(OnLeftHandler);
                tcs.TrySetResult(wasSatisfied);
            }

            customer.OnLeft.AddListener(OnLeftHandler);
            return tcs.Task;
        }

        public void Complete()
        {
            if (completionSource != null && !completionSource.Task.IsCompleted)
            {
                completionSource.SetResult(true);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Complete();
            }
        }
    }
}