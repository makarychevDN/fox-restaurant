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
        private MainMenu mainMenu;

        public void Init(MainMenu mainMenu)
        {
            this.mainMenu = mainMenu;

            foreach (var cell in calendarCells)
            {
                cell.Button.onClick.AddListener(() => levelLoader.SetEncaunters(cell.EncountersList));
            }

            launchLevelButton.onClick.AddListener(LaunchButtonClickedHandler);
            backButton.onClick.AddListener(() => mainMenu.EnablePanel(this, mainMenu.StartMenuPanel));
        }

        private async void LaunchButtonClickedHandler()
        {
            await mainMenu.Fading.FadeIn();
            levelLoader.LoadLevel();
        }
    }
}