using UnityEngine;

namespace foxRestaurant
{
    public abstract class CustomerEffectView<TInstance> : MonoBehaviour where TInstance : ICustomerEffectInstance
    {
        protected TInstance Instance { get; private set; }

        public virtual void Init(TInstance instance)
        {
            Instance = instance;
            OnInit();
        }

        protected abstract void OnInit();
    }
}