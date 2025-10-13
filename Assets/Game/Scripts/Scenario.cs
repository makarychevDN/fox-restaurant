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
            restaurantEncounter.ItemsSpawner.SpawnCustomerItem();
        }
    }
}