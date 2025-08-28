using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace foxRestaurant
{
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField] private Transform customerSlotsParent;
        [SerializeField] private Customer customerPrefab;
        [SerializeField] private List<Customer> customers;
        [SerializeField] private List<CustomerData> availableCustomersData;
        private Level level;

        public UnityEvent OnCustomerSpawningRejected;

        public Transform CustomerSlotsParent => customerSlotsParent;

        public void Init(Level level)
        {
            this.level = level;
            customers.ForEach(customer =>
            {
                customer.Init(level, CustomerSlotsParent);
                customer.SetCustomerData(availableCustomersData.GetRandomElement());
            });
        }

        public void SpawnCustomer(CustomerData customerData, Transform parentToPlaceCustomer)
        {
            var customer = Instantiate(customerPrefab);
            customer.transform.parent = parentToPlaceCustomer;
            customer.transform.localPosition = Vector3.zero;
            customer.transform.localScale = Vector3.one;
            customer.Init(level, CustomerSlotsParent);
            customer.SetCustomerData(customerData);
        }
    }
}