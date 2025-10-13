using UnityEngine;

namespace foxRestaurant
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}