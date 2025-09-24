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

            SpawnCustomer();
        }

        [ContextMenu("Spawn Customer")]
        public void SpawnCustomer()
        {
            customerSpawner.TryToSpawnCustomer(restaurantEncounter.DecksManager.GetRandomCustomer(), () => restaurantEncounter.DecksManager.GetRandomDish());
            Invoke(nameof(SpawnCustomer), 5f);
        }
    }
}