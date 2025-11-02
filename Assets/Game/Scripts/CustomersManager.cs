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

        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
            OnCustomerAdded.Invoke();
            OnCertainCustomerAdded.Invoke(customer);
        }

        public void RemoveCustomer(Customer customer)
        {
            customers.Remove(customer);
        }
    }
}