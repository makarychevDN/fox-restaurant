using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class RiversideScenarioPart4 : BaseScenario<RestaurantEncounter>
    {
        [SerializeField] private Character red;
        [SerializeField] private CustomerData seal;
        [SerializeField] private ItemData iceCream;

        protected override void InitTyped(RestaurantEncounter encounter) { }

        protected override async Task StartScenarioTyped(RestaurantEncounter encounter)
        {
            List<CustomerData> customersData = new List<CustomerData>() { seal, seal, seal };
            await FixWave(encounter, customersData);

            while (true)
            {
                await red.Say("hello");
            }
        }

        public async Task FixWave(RestaurantEncounter encounter, List<CustomerData> customersToSpawnData)
        {
            bool success = false;
            int seatPlacesCount = FindObjectsByType<SeatPlace>(FindObjectsSortMode.None).ToList().Count;
            var initCustomersCount = Mathf.Min(customersToSpawnData.Count, seatPlacesCount);

            for (int i = 0; i < initCustomersCount; i++)
            {
                await Task.Delay(500);
                encounter.CustomerSpawner.TryToSpawnCustomer(customersToSpawnData[i], () => iceCream);
            }

            await Task.Delay(500);
        }

        /*7private async Task FixWave(RestaurantEncounter encounter, List<ItemData> itemsToSpawnData, params (CustomerData, Func<ItemData>)[] customersAndTheirOrders)
        {
            await FixWave(encounter, itemsToSpawnData, new List<string>(), customersAndTheirOrders);
        }

        private async Task FixWave(RestaurantEncounter encounter, List<ItemData> itemsToSpawnData, string commentary, params (CustomerData, Func<ItemData>)[] customersAndTheirOrders)
        {
            await FixWave(encounter, itemsToSpawnData, new List<string> { commentary }, customersAndTheirOrders);
        }

        private async Task FixWave(RestaurantEncounter encounter, List<ItemData> itemsToSpawnData, List<string> commentaries, params (CustomerData, Func<ItemData>)[] customersAndTheirOrders)
        {
            bool success = false;
            while (!success)
            {
                Time.timeScale = 1;
                encounter.BlockInput();
                customersToFeed.Clear();

                if (itemsToSpawnData.Count > 0)
                {
                    itemSlots.ForEach(slot => slot.Clear());
                    FindObjectsOfType<Item>().ToList().ForEach(item => Destroy(item.gameObject));
                }

                for (int i = 0; i < itemsToSpawnData.Count; i++)
                {
                    await Task.Delay(500);
                    encounter.ItemsSpawner.SpawnFoodItem(encounter, itemsToSpawnData[i], itemSlots[i]);
                }

                foreach (var customerAndOrder in customersAndTheirOrders)
                {
                    await Task.Delay(500);

                    var customer = encounter.CustomerSpawner.TryToSpawnCustomer(
                        customerAndOrder.Item1,
                        customerAndOrder.Item2
                    );

                    customersToFeed.Add(customer);
                }

                foreach (var commentary in commentaries)
                {
                    await red.Say(commentary);
                }
                encounter.UnblockInput();

                var tasks = customersToFeed.Select(WaitForCustomerToLeave).ToArray();
                var results = await Task.WhenAll(tasks);
                Time.timeScale = 1;

                success = results.All(r => r);
                if (!success)
                    await red.Say(waveIsFailedLine);
            }
        }*/
    }
}