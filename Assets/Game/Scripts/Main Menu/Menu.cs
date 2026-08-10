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

        protected async void SwitchPanelsWithAnimation(MonoBehaviour currentPanel, MonoBehaviour nextPanel)
        {
            await fading.FadeIn();
            SwitchPanels(currentPanel, nextPanel);
            await fading.FadeOut();
        }

        protected async void SwitchPanels(MonoBehaviour currentPanel, MonoBehaviour nextPanel)
        {
            currentPanel.gameObject.SetActive(false);
            nextPanel.gameObject.SetActive(true);
        }

        protected async void SwitchPanels(GameObject currentPanel, GameObject nextPanel)
        {
            currentPanel.SetActive(false);
            nextPanel.SetActive(true);
        }

        protected void InitSettingsMenuPanel()
        {
            settingsMenuPanel.BackButton.onClick.AddListener(() => SwitchPanelsWithAnimation(settingsMenuPanel, titleMenuPanel));

            settingsMenuPanel.ScreenSettingsButton.onClick.AddListener(() => SwitchPanels(settingsMenuPanel.TitlePanel, settingsMenuPanel.ScreenSettingsPanel));
            settingsMenuPanel.SoundSettingsButton.onClick.AddListener(() => SwitchPanels(settingsMenuPanel.TitlePanel, settingsMenuPanel.SoundSettingsPanel));
            settingsMenuPanel.LanguageSettingsButton.onClick.AddListener(() => SwitchPanels(settingsMenuPanel.TitlePanel, settingsMenuPanel.LanguageSettingsPanel));
            settingsMenuPanel.DifficultySettingsButton.onClick.AddListener(() => SwitchPanels(settingsMenuPanel.TitlePanel, settingsMenuPanel.DifficultySettingsPanel));

            settingsMenuPanel.ScreenSettingsBackButton.onClick.AddListener(() => SwitchPanels(settingsMenuPanel.ScreenSettingsPanel, settingsMenuPanel.TitlePanel));
            settingsMenuPanel.SoundSettingsBackButton.onClick.AddListener(() => SwitchPanels(settingsMenuPanel.SoundSettingsPanel, settingsMenuPanel.TitlePanel));
            settingsMenuPanel.LanguageSettingsBackButton.onClick.AddListener(() => SwitchPanels(settingsMenuPanel.LanguageSettingsPanel, settingsMenuPanel.TitlePanel));
            settingsMenuPanel.DifficultySettingsBackButton.onClick.AddListener(() => SwitchPanels(settingsMenuPanel.DifficultySettingsPanel, settingsMenuPanel.TitlePanel));
        }

        protected virtual void InitTitleMenuPanel()
        {
            titleMenuPanel.SettingsButton.onClick.AddListener(() => SwitchPanelsWithAnimation(titleMenuPanel, settingsMenuPanel));
            titleMenuPanel.ExitButton.onClick.AddListener(Extensions.Quit);
        }
    }
}