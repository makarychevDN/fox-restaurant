using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class TitleMenuPanel : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button exitButton;

        public Button PlayButton => playButton;
        public Button SettingsButton => settingsButton;
        public Button ExitButton => exitButton;

        //public void Init(MainMenu mainMenu)
        //{
            /*playButton.onClick.AddListener(ñalendarMenuPanelxitButton.LaunchTheFirstLevel);
            settingsButton.onClick.AddListener(() => mainMenu.EnablePanel(this, mainMenu.SettingsMenuPanel));
            exitButton.onClick.AddListener(Extensions.Quit);*/
        //}
    }
}
