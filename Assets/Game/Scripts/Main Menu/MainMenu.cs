using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class MainMenu : MonoBehaviour
    {
        [field: SerializeField] public StartMenuPanel StartMenuPanel { get; private set; }
        [field: SerializeField] public CalendarMenuPanel CalendarMenuPanel { get; private set; }
        [field: SerializeField] public SettingsMenuPanel SettingsMenuPanel { get; private set; }
        [field: SerializeField] public Image Fading { get; private set; }


        private void Awake()
        {
            Fading.material = new Material(Fading.material);
            Fading.FadeOut();

            CalendarMenuPanel.Init(this);
            SettingsMenuPanel.Init(this);
            StartMenuPanel.Init(this);
        }

        public async void EnablePanel(MonoBehaviour currentPanel, MonoBehaviour nextPanel)
        {
            await Fading.FadeIn();
            currentPanel.gameObject.SetActive(false);
            nextPanel.gameObject.SetActive(true);
            await Fading.FadeOut();
        }
    }
}