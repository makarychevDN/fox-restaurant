using UnityEngine;

namespace foxRestaurant
{
    public class Scenario : MonoBehaviour
    {
        private CustomerSpawner customerSpawner;

        public void Init(CustomerSpawner customerSpawner, Level level)
        {
            this.customerSpawner = customerSpawner;

            for(int i = 0; i < 4; i++)
            {
                customerSpawner.TryToSpawnCustomer(level.DecksManager.GetRandomCustomer(), () => level.DecksManager.GetRandomDish());
            }
        }
    }
}