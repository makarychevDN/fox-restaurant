using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private Transform customerSlotsParent;
    [SerializeField] private Customer customerPrefab;
    [SerializeField] private List<Customer> customers;
    private Level level;

    public Transform CustomerSlotsParent => customerSlotsParent;

    public void Init(Level level)
    {
        this.level = level;
        customers.ForEach(customer => customer.Init(level, CustomerSlotsParent));
    }

    public void SpawnCustomer()
    {
        var customer = Instantiate(customerPrefab);
        customer.Init(level, CustomerSlotsParent);
    }
}
