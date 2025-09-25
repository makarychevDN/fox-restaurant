using UnityEngine;

namespace foxRestaurant
{
    [CreateAssetMenu(fileName = "DataBetweenScenesContainer", menuName = "Scriptable Objects/DataBetweenScenesContainer")]
    public class DataBetweenScenesContainer : ScriptableObject
    {
        [field : SerializeField] public EncountersListAsset EncountersList { get; set; }
    }
}