using UnityEngine;

namespace foxRestaurant
{
    public interface ICustomerEffect
    {
        public ICustomerEffectInstance CreateInstance();
    }
}