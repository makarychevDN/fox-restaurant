using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class CalendarCell : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private EncountersListAsset encountersList;

        public Button Button => button;
        public EncountersListAsset EncountersList => encountersList;
    }
}