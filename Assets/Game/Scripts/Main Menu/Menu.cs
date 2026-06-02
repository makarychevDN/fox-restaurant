using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public abstract class Menu : MonoBehaviour
    {
        [SerializeField] protected Image fading;
        [SerializeField] protected TitleMenuPanel TitleMenuPanel;
        [SerializeField] protected SettingsMenuPanel SettingsMenuPanel;

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
            SettingsMenuPanel.BackButton.onClick.AddListener(() => SwitchPanels(SettingsMenuPanel, TitleMenuPanel));
        }

        protected virtual void InitTitleMenuPanel()
        {
            TitleMenuPanel.SettingsButton.onClick.AddListener(() => SwitchPanels(TitleMenuPanel, SettingsMenuPanel));
            TitleMenuPanel.ExitButton.onClick.AddListener(Extensions.Quit);
        }
    }
}