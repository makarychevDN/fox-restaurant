using UnityEngine;

namespace foxRestaurant
{
    [CreateAssetMenu(fileName = "RestaurantEncounterData", menuName = "Scriptable Objects/RestaurantEncounterData")]
    public class RestaurantEncounterData : ScriptableObject
    {
        [field: SerializeField] public TextAsset CsvFile { get; private set; }
        [field: SerializeField] public ItemsDataList AllPossibleItemData { get; private set; }
    }
}