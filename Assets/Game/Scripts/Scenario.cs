using UnityEngine;

namespace foxRestaurant
{
    public class Scenario : MonoBehaviour
    {
        [SerializeField] private CustomerData wolfData;
        private CustomerSpawner customerSpawner;

        public void Init(CustomerSpawner customerSpawner)
        {
            this.customerSpawner = customerSpawner;
            customerSpawner.TryToSpawnCustomer(wolfData);
            customerSpawner.TryToSpawnCustomer(wolfData);
            customerSpawner.TryToSpawnCustomer(wolfData);
            customerSpawner.TryToSpawnCustomer(wolfData);
        }
    }
}