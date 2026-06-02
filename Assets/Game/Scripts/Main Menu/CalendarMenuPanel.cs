using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class CalendarMenuPanel : MonoBehaviour
    {
        [SerializeField] private List<CalendarCell> calendarCells;
        [SerializeField] private LevelLoader levelLoader;
        [SerializeField] private Button launchLevelButton;
        [SerializeField] private Button backButton;
        [SerializeField] private EncountersListAsset firstLevelEncountersListAsset;
    }
}