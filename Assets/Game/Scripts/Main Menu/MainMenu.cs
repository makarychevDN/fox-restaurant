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
        [SerializeField] private Image fading;
        private Material material;

        private void Awake()
        {
            fading.material = new Material(fading.material);
            fading.material.AnimateThreshold("_Fading", 1, 0, 1f);

            foreach (var cell in calendarCells)
            {
                cell.Button.onClick.AddListener(() => levelLoader.SetEncaunters(cell.EncountersList));
            }

            launchLevelButton.onClick.AddListener(LaunchButtonClickedHandler);
        }

        private async void LaunchButtonClickedHandler()
        {
            await fading.material.AnimateThreshold("_Fading", 0, 1, 1f);
            levelLoader.LoadLevel();
        }
    }
}