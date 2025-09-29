using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private List<CalendarCell> calendarCells;
        [SerializeField] private LevelLoader levelLoader;
        [SerializeField] private Button launchLevelButton;
        [SerializeField] private Image fading;

        private void Awake()
        {
            foreach (var cell in calendarCells)
            {
                cell.Button.onClick.AddListener(() => levelLoader.SetEncaunters(cell.EncountersList));
            }

            launchLevelButton.onClick.AddListener(LaunchButtonClickedHandler);
        }

        private async void LaunchButtonClickedHandler()
        {
            await fading.FadeIn();
            levelLoader.LoadLevel();
        }
    }
}