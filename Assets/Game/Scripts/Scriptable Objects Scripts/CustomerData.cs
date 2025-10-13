using UnityEngine;

namespace foxRestaurant
{
    [CreateAssetMenu(fileName = "CustomerData", menuName = "Scriptable Objects/CustomerData")]
    public class CustomerData : ItemData
    {
        [field: SerializeField] public int HungerPoints { get; set; }
        [field: SerializeField] public float Patience { get; set; }
    }
}