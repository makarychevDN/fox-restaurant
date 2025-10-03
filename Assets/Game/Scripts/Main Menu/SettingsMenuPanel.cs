using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class SettingsMenuPanel : MonoBehaviour
    {
        [SerializeField] private Button backButton;

        public void Init(MainMenu mainMenu)
        {
            backButton.onClick.AddListener(() => mainMenu.EnablePanel(this, mainMenu.StartMenuPanel));
        }
    }
}