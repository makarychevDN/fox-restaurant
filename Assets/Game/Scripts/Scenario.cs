using UnityEngine;

namespace foxRestaurant
{
    public class Scenario : MonoBehaviour
    {
        private CustomerSpawner customerSpawner;
        RestaurantEncounter restaurantEncounter;

        public void Init(CustomerSpawner customerSpawner, RestaurantEncounter restaurantEncounter)
        {
            this.customerSpawner = customerSpawner;
            this.restaurantEncounter = restaurantEncounter;
            var customerItem = restaurantEncounter.ItemsSpawner.SpawnCustomerItem();
            customerItem.OnDestroyed.AddListener(CustomerItemPlacedHandler);
        }

        public void CustomerItemPlacedHandler()
        {
            var customerItem = restaurantEncounter.ItemsSpawner.SpawnCustomerItem();
            customerItem.OnDestroyed.AddListener(CustomerItemPlacedHandler);
        }
    }
}