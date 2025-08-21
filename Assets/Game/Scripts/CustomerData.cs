using UnityEngine;

[CreateAssetMenu(fileName = "CustomerData", menuName = "Scriptable Objects/CustomerData")]
public class CustomerData : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; set; }
    [field: SerializeField] public int HungerPoints { get; set; }
    [field: SerializeField] public int Patience { get; set; }
}
