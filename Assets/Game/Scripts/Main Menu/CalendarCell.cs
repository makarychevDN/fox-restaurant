using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class CalendarCell : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private EncountersList encountersList;

        public Button Button => button;
        public EncountersList EncountersList => encountersList;
    }
}