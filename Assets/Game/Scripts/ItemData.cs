using UnityEngine;

namespace foxRestaurant
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField] public Sprite Sprite { get; set; }
        [field: SerializeField] public ItemData FryingResult { get; set; }
        [field: SerializeField] public ItemData SlicingResult { get; set; }
    }
}