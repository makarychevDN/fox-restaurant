using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; set; }
    [field: SerializeField] public Sprite OutlineSprite { get; set; }
    [field: SerializeField] public ItemData CookingResult { get; set; }
}
