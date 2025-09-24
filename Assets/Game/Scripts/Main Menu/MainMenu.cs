using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private List<CalendarCell> calendarCells;
        [SerializeField] private LevelLoader levelLoader;
        [SerializeField] private Button launchLevelButton;

        private void Awake()
        {
            foreach (var cell in calendarCells)
            {
                cell.Button.onClick.AddListener(() => levelLoader.SetEncaunters(cell.EncountersList));
            }

            launchLevelButton.onClick.AddListener(levelLoader.LoadLevel);
        }
    }
}