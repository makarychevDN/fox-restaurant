using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace foxRestaurant
{
    public class CustomersManager : MonoBehaviour
    {
        private List<Customer> customers = new();

        public List<Customer> Customers => customers;

        public UnityEvent OnCustomerAdded;
        public UnityEvent<Customer> OnCertainCustomerAdded;
        public UnityEvent<Customer, ItemData> OnCustomerWasFed;

        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
            OnCustomerAdded.Invoke();
            OnCertainCustomerAdded.Invoke(customer);
            customer.OnAteCertainFood.AddListener(itemData => CustomerWasFedHandler(customer, itemData));
        }

        public void RemoveCustomer(Customer customer)
        {
            customers.Remove(customer);
        }

        public void CustomerWasFedHandler(Customer customer, ItemData itemData)
        {
            OnCustomerWasFed.Invoke(customer, itemData);
        }
    }
}