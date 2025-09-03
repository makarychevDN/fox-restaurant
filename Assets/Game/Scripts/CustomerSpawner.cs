using System;
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

        public Customer SpawnCustomer(Transform parentToPlaceCustomer, CustomerData customerData, Func<ItemData> getItemDataToOrderFunc)
        {
            var customer = Instantiate(customerPrefab);
            customer.transform.parent = parentToPlaceCustomer;
            customer.transform.localPosition = Vector3.zero;
            customer.transform.localScale = Vector3.one;
            customer.transform.localRotation = Quaternion.identity;
            customer.Init(level, customerData, getItemDataToOrderFunc);
            return customer;
        }

        public Customer TryToSpawnCustomer(CustomerData customerData, Func<ItemData> getItemDataToOrderFunc)
        {
            var parentToSpawnCustomer = 
                customerParents.Where(customerParent => customerParent.childCount == 0).ToList().GetRandomElement();

            if (parentToSpawnCustomer == null)
                return null;

            var customer = SpawnCustomer(
                parentToSpawnCustomer, 
                customerData,
                getItemDataToOrderFunc
            );

            return customer;
        }
    }
}