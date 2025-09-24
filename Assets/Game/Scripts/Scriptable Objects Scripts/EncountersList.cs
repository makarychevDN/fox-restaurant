using System.Collections.Generic;
using UnityEngine;

namespace foxRestaurant
{
    [CreateAssetMenu(fileName = "EncountersList", menuName = "Scriptable Objects/EncountersList")]
    public class EncountersList : ScriptableObject
    {
        [SerializeField] private List<Encounter> encounterPrefabs;
    }
}