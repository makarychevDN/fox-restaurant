using System;
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

        [SerializeField] private Character redTheCook;

        private List<Customer> customersToFeed = new();
        private List<ItemSlot> itemSlots;
        private TaskCompletionSource<bool> completionSource = new();
        private int switchConeAndPopsicleCount;

        protected override async Task StartScenarioTyped(RestaurantEncounter encounter)
        {
            itemSlots = encounter.SlotsManager.Slots.Where(slot => slot.RequiredItemsType == ItemType.Food && slot.gameObject.activeSelf).ToList();

            await Task.Delay(500);
            await redTheCook.Say("*sigh*<pause:1> Another day, another dollar.");

            await FixWave(encounter, new List<ItemData> { iceCreamConeData, iceCreamConeData }, "", 
                (duck, () => iceCreamConeData), (kitty, () => iceCreamConeData));
            await FixWave(encounter, new List<ItemData> { popsicleData, iceCreamConeData }, "",
                (duck, () => iceCreamConeData), (kitty, () => popsicleData));
            await FixWave(encounter, new List<ItemData> { popsicleData, iceCreamConeData },
                "Damn,<pause:0.5> this kiddo seems tough,<pause:0.5> he needs at least two icecreams.",
                (doggo, SwitchConeAndPopsicle));
            await redTheCook.Say("Big boy, huh?<pause:0.75> The bigger they are, the harder they fall.");

            await completionSource.Task;
        }

        private async Task FixWave(RestaurantEncounter encounter, List<ItemData> itemsToSpawnData, string commentary, params (CustomerData, Func<ItemData>)[] customersAndTheirOrders)
        {
            bool success = false;
            while (!success)
            {
                customersToFeed.Clear();

                for (int i = 0; i < itemsToSpawnData.Count; i++)
                {
                    await Task.Delay(500);
                    encounter.ItemsSpawner.SpawnFoodItem(encounter, itemsToSpawnData[i], itemSlots[i]);
                }

                foreach(var customerAndOrder in customersAndTheirOrders)
                {
                    await Task.Delay(500);

                    var customer = encounter.CustomerSpawner.TryToSpawnCustomer(
                        customerAndOrder.Item1,
                        customerAndOrder.Item2
                    );

                    customersToFeed.Add(customer);
                }

                if (commentary != "")
                    await redTheCook.Say(commentary);

                var tasks = customersToFeed.Select(WaitForCustomerToLeave).ToArray();
                var results = await Task.WhenAll(tasks);

                success = results.All(r => r);
                if (!success)
                    await redTheCook.Say("damn...<pause:1> Ok, let's try again");
            }
        }

        private ItemData SwitchConeAndPopsicle()
        {
            switchConeAndPopsicleCount++;
            return switchConeAndPopsicleCount % 2 == 0 ? popsicleData : iceCreamConeData;
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