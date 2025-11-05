using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class StartMenuPanel : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button exitButton;

        [SerializeField] private CalendarMenuPanel ñalendarMenuPanelxitButton;

        public void Init(MainMenu mainMenu)
        {
            //playButton.onClick.AddListener(() => mainMenu.EnablePanel(this, mainMenu.CalendarMenuPanel));
            playButton.onClick.AddListener(ñalendarMenuPanelxitButton.LaunchTheFirstLevel);
            settingsButton.onClick.AddListener(() => mainMenu.EnablePanel(this, mainMenu.SettingsMenuPanel));
            exitButton.onClick.AddListener(Extensions.Quit);
        }
    }
}
