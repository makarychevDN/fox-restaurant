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

        public void Apply(Customer customer, RestaurantEncounter encounter)
        {
            owner = customer;
            encounter.CustomersManager.OnCustomerWasFed.AddListener(AnyCustomerWasFedHandler);
        }

        private void AnyCustomerWasFedHandler(Customer customer, ItemData itemData)
        {
            if (owner == customer)
                return;

            owner.SetOrderData(itemData);
        }
    }

}