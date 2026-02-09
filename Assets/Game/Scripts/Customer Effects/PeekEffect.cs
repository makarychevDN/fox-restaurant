using UnityEngine;

namespace foxRestaurant
{
    [CreateAssetMenu(fileName = "peek effect", menuName = "Scriptable Objects/Customer Effects/peek effect")]
    public class PeekEffect : ScriptableObject, ICustomerEffect, IAbleToReturnViewPrefab
    {
        public ICustomerEffectInstance CreateInstance() => new PeekEffectInstance();

        public GameObject GetViewPrefab() => new GameObject();
    }

    public class PeekEffectInstance : ICustomerEffectInstance
    {
        private Customer owner;
        private RestaurantEncounter encounter;

        public void Apply(Customer customer, RestaurantEncounter encounter)
        {
            owner = customer;
            this.encounter = encounter;
            encounter.CustomersManager.OnCustomerWasFed.AddListener(AnyCustomerWasFedHandler);
            customer.OnLeft.AddListener(RemoveAllListeners);
        }

        private void AnyCustomerWasFedHandler(Customer customer, ItemData itemData)
        {
            if (owner == customer)
                return;

            owner.SetOrderData(itemData);
        }

        private void RemoveAllListeners()
        {
            owner.OnLeft.RemoveListener(RemoveAllListeners);
            encounter.CustomersManager.OnCustomerWasFed.RemoveListener(AnyCustomerWasFedHandler);
        }
    }

}