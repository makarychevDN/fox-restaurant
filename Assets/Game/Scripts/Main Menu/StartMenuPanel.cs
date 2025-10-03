using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class StartMenuPanel : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button exitButton;

        public void Init(MainMenu mainMenu)
        {
            playButton.onClick.AddListener(() => mainMenu.EnablePanel(this, mainMenu.CalendarMenuPanel));
            exitButton.onClick.AddListener(Extensions.Quit);
        }
    }
}
