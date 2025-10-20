using UnityEngine;

namespace foxRestaurant
{
    public class DefaultRestaurantEncounterScenario : RestaurantScenario
    {
        RestaurantEncounter restaurantEncounter;

        public override void Init(RestaurantEncounter restaurantEncounter)
        {
            this.restaurantEncounter = restaurantEncounter;
            var customerItem = restaurantEncounter.ItemsSpawner.SpawnCustomerItem();
            customerItem.OnDestroyed.AddListener(CustomerItemPlacedHandler);
        }

        public void CustomerItemPlacedHandler()
        {
            var customerItem = restaurantEncounter.ItemsSpawner.SpawnCustomerItem();

            if (customerItem == null)
                return;

            customerItem.OnDestroyed.AddListener(CustomerItemPlacedHandler);
        }
    }
}