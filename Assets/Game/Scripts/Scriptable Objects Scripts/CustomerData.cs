using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace foxRestaurant
{
    [CreateAssetMenu(fileName = "CustomerData", menuName = "Scriptable Objects/CustomerData")]
    public class CustomerData : ItemData
    {
        [field: SerializeField] public int HungerPoints { get; set; }
        [field: SerializeField] public float Patience { get; set; }
        [SerializeField] private List<ScriptableObject> effects;

        public IEnumerable<ICustomerEffect> Effects => effects.OfType<ICustomerEffect>();
    }
}