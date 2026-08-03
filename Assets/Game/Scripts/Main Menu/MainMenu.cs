using System;
using UnityEngine;

namespace foxRestaurant
{
    public class MainMenu : Menu
    {
        [SerializeField] private EncountersListAsset firstLevelEncountersListAsset;
        [SerializeField] private EncountersListAsset secondLevelEncountersListAsset;
        [SerializeField] private LevelLoader levelLoader;
        [SerializeField] protected SupportAuthorMenuPanel supportAuthorMenuPanel;
        [SerializeField] protected SelectLevelPanel selectLevelPanel;
        private static bool appIsStartedAlready = false;

        protected override void Init()
        {
            fading.material = new Material(fading.material);
            fading.FadeOut();

            selectLevelPanel.gameObject.SetActive(appIsStartedAlready);
            appIsStartedAlready = true;

            base.Init();
        }

        protected override void InitTitleMenuPanel()
        {
            base.InitTitleMenuPanel();

            var thereIsSavedGame = false;

            titleMenuPanel.ResumeButton.gameObject.SetActive(thereIsSavedGame);
            titleMenuPanel.SelectLevelButton.gameObject.SetActive(thereIsSavedGame);
            titleMenuPanel.PlayButton.gameObject.SetActive(!thereIsSavedGame);

            titleMenuPanel.PlayButton.onClick.AddListener(() => SwitchPanels(titleMenuPanel, selectLevelPanel));
            titleMenuPanel.SelectLevelButton.onClick.AddListener(() => SwitchPanels(titleMenuPanel, selectLevelPanel));

            selectLevelPanel.BackButton.onClick.AddListener(() => SwitchPanels(selectLevelPanel, titleMenuPanel));
            selectLevelPanel.Level1Button.onClick.AddListener(() => LaunchLevel(firstLevelEncountersListAsset, 0));
            selectLevelPanel.Level2Button.onClick.AddListener(() => LaunchLevel(secondLevelEncountersListAsset, 0));

            titleMenuPanel.SupportAuthorButton.onClick.AddListener(() => SwitchPanels(titleMenuPanel, supportAuthorMenuPanel));

            supportAuthorMenuPanel.BackButton.onClick.AddListener(() => SwitchPanels(supportAuthorMenuPanel, titleMenuPanel));
        }

        private async void LaunchLevel(EncountersListAsset encountersListAsset, int startEncounterIndex)
        {
            levelLoader.SetupLevel(encountersListAsset, startEncounterIndex);
            await fading.FadeIn();
            levelLoader.LoadLevel();
        }
    }
}