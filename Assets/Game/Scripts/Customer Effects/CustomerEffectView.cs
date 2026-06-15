using UnityEngine;

namespace foxRestaurant
{
    public abstract class CustomerEffectView<TInstance> : MonoBehaviour, ICustomerEffectView where TInstance : ICustomerEffectInstance
    {
        protected TInstance Instance { get; private set; }

        public virtual void Init(TInstance instance, RestaurantEncounter restaurantEncounter)
        {
            Instance = instance;
            OnInit(restaurantEncounter);
        }

        public void InitBase(ICustomerEffectInstance instance, RestaurantEncounter restaurantEncounter)
        {
            Init((TInstance)instance, restaurantEncounter);
        }

        protected abstract void OnInit(RestaurantEncounter restaurantEncounter);
    }
}