using UnityEngine;

namespace foxRestaurant
{
    public class Scenario : MonoBehaviour
    {
        private CustomerSpawner customerSpawner;
        Level level;

        public void Init(CustomerSpawner customerSpawner, Level level)
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