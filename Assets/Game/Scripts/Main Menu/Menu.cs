using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public abstract class Menu : MonoBehaviour
    {
        [SerializeField] protected Image fading;
        [SerializeField] protected TitleMenuPanel titleMenuPanel;
        [SerializeField] protected SettingsMenuPanel settingsMenuPanel;

        protected void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            InitTitleMenuPanel();
            InitSettingsMenuPanel();
        }

        protected async void SwitchPanels(MonoBehaviour currentPanel, MonoBehaviour nextPanel)
        {
            await fading.FadeIn();
            currentPanel.gameObject.SetActive(false);
            nextPanel.gameObject.SetActive(true);
            await fading.FadeOut();
        }

        protected void InitSettingsMenuPanel()
        {
            settingsMenuPanel.BackButton.onClick.AddListener(() => SwitchPanels(settingsMenuPanel, titleMenuPanel));
        }

        protected virtual void InitTitleMenuPanel()
        {
            titleMenuPanel.SettingsButton.onClick.AddListener(() => SwitchPanels(titleMenuPanel, settingsMenuPanel));
            titleMenuPanel.ExitButton.onClick.AddListener(Extensions.Quit);
        }
    }
}