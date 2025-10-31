using UnityEngine;

namespace foxRestaurant
{
    [CreateAssetMenu(fileName = "taunt effect", menuName = "Scriptable Objects/Customer Effects/taunt effect")]
    public class TauntEffect : ScriptableObject, ICustomerEffect
    {
        public ICustomerEffectInstance CreateInstance()
        {
            return new TauntEffectInstance();
        }
    }

    public class TauntEffectInstance : ICustomerEffectInstance
    {
        public void Apply(Customer customer)
        {
            Debug.Log($"muhaha, Taunt from {customer}");
        }
    }
}