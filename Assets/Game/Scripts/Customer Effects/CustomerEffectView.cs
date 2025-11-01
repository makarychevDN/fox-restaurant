using UnityEngine;

namespace foxRestaurant
{
    public abstract class CustomerEffectView<TInstance> : MonoBehaviour, ICustomerEffectView where TInstance : ICustomerEffectInstance
    {
        protected TInstance Instance { get; private set; }

        public virtual void Init(TInstance instance)
        {
            Instance = instance;
            OnInit();
        }

        public void InitBase(ICustomerEffectInstance instance)
        {
            Init((TInstance)instance);
        }

        protected abstract void OnInit();
    }
}