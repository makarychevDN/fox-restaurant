using UnityEngine;

namespace foxRestaurant
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
    public class RestaurantEncounterData : ScriptableObject
    {
        [field: SerializeField] public TextAsset CsvFile { get; private set; }
        [field: SerializeField] public ItemsDataList AllPossibleItemData { get; private set; }
    }
}