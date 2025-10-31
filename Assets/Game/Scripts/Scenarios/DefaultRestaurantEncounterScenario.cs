using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class DefaultRestaurantEncounterScenario : RestaurantScenario
    {
        RestaurantEncounter restaurantEncounter;

        public void CustomerItemPlacedHandler()
        {
            var customerItem = restaurantEncounter.ItemsSpawner.SpawnCustomerItem();

            if (customerItem == null)
                return;

            customerItem.OnDestroyed.AddListener(CustomerItemPlacedHandler);
        }

        protected override async Task StartScenarioTyped(RestaurantEncounter ecnounter)
        {
            restaurantEncounter = ecnounter;
            var customerItem = restaurantEncounter.ItemsSpawner.SpawnCustomerItem();
            customerItem.OnDestroyed.AddListener(CustomerItemPlacedHandler);

            await Task.Delay(100000);
        }
    }
}