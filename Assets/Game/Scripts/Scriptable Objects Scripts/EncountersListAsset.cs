using System.Collections.Generic;
using UnityEngine;

namespace foxRestaurant
{
    [CreateAssetMenu(fileName = "EncountersList", menuName = "Scriptable Objects/EncountersList")]
    public class EncountersListAsset : ScriptableObject
    {
        [field : SerializeField] public List<Encounter> Encounters {  get; private set; }
    }
}