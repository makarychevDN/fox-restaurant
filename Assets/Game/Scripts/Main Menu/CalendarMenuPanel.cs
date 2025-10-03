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
        private MainMenu mainMenu;

        public void Init(MainMenu mainMenu)
        {
            this.mainMenu = mainMenu;

            foreach (var cell in calendarCells)
            {
                cell.Button.onClick.AddListener(() => levelLoader.SetEncaunters(cell.EncountersList));
            }

            launchLevelButton.onClick.AddListener(LaunchButtonClickedHandler);
        }

        private async void LaunchButtonClickedHandler()
        {
            await mainMenu.Fading.FadeIn();
            levelLoader.LoadLevel();
        }
    }
}