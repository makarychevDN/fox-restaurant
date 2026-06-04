using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace foxRestaurant
{
    public class GameplayPauseMenu : Menu
    {
        [SerializeField] private Level level;

        protected override void InitTitleMenuPanel()
        {
            base.InitTitleMenuPanel();
            titleMenuPanel.ResumeButton.onClick.AddListener(level.ClosePauseMenu);
            titleMenuPanel.RestartWaveButton.onClick.AddListener(RestartButtonClickHandler);
            titleMenuPanel.MainMenuButton.onClick.AddListener(MainMenuButtonClickHandler); //todo fade and quit to main menu
        }

        private void RestartButtonClickHandler()
        {
            RestaurantEncounter restaurantEncounter = level.CurrentEncounter as RestaurantEncounter;
            if (restaurantEncounter != null)
                restaurantEncounter.Ticker.SetX40TickingSpeed();

            level.ClosePauseMenu();
        }

        private async void MainMenuButtonClickHandler()
        {
            level.ClosePauseMenu();
            await fading.FadeIn();
            SceneManager.LoadScene("Main Menu");
        }
    }
}