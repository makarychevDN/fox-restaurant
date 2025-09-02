using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


namespace foxRestaurant
{
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField] private Customer customerPrefab;
        [SerializeField] private List<Transform> customerParents;
        private Level level;

        public UnityEvent OnCustomerSpawningRejected;

        public void Init(Level level)
        {
            this.level = level;
        }

        public void SpawnCustomer(Transform parentToPlaceCustomer, CustomerData customerData)
        {
            var customer = Instantiate(customerPrefab);
            customer.transform.parent = parentToPlaceCustomer;
            customer.transform.localPosition = Vector3.zero;
            customer.transform.localScale = Vector3.one;
            customer.transform.localRotation = Quaternion.identity;
            customer.Init(level, customerData);
        }

        public void TryToSpawnCustomer(CustomerData customerData)
        {
            var parentToSpawnCustomer = 
                customerParents.Where(customerParent => customerParent.childCount == 0).ToList().GetRandomElement();

            if (parentToSpawnCustomer == null)
                return;

            SpawnCustomer(
                customerParents.Where(customerParent => customerParent.childCount == 0).ToList().GetRandomElement(), 
                customerData
            );
        }
    }
}