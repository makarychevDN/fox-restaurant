using UnityEngine;

namespace foxRestaurant
{
    public class Scenario : MonoBehaviour
    {
        private CustomerSpawner customerSpawner;
        RestaurantEncounter level;

        public void Init(CustomerSpawner customerSpawner, RestaurantEncounter level)
        {
            this.customerSpawner = customerSpawner;
            this.level = level;

            SpawnCustomer();
        }

        [ContextMenu("Spawn Customer")]
        public void SpawnCustomer()
        {
            customerSpawner.TryToSpawnCustomer(level.DecksManager.GetRandomCustomer(), () => level.DecksManager.GetRandomDish());
            Invoke(nameof(SpawnCustomer), 5f);
        }
    }
}