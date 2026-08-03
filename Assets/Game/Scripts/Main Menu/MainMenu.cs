using System;
using System.Collections.Generic;
using UnityEngine;

namespace foxRestaurant
{
    public class MainMenu : Menu
    {
        [SerializeField] private List<EncountersListAsset> encounterListAssets;
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

            var thereIsSavedGame = PlayerPrefs.HasKey("CurrentEncounterIndex");

            titleMenuPanel.ResumeButton.gameObject.SetActive(thereIsSavedGame);
            titleMenuPanel.SelectLevelButton.gameObject.SetActive(thereIsSavedGame);
            titleMenuPanel.PlayButton.gameObject.SetActive(!thereIsSavedGame);

            titleMenuPanel.PlayButton.onClick.AddListener(() => SwitchPanels(titleMenuPanel, selectLevelPanel));
            titleMenuPanel.SelectLevelButton.onClick.AddListener(() => SwitchPanels(titleMenuPanel, selectLevelPanel));
            titleMenuPanel.ResumeButton.onClick.AddListener(() => LaunchSavedLevel());
            selectLevelPanel.BackButton.onClick.AddListener(() => SwitchPanels(selectLevelPanel, titleMenuPanel));

            titleMenuPanel.SupportAuthorButton.onClick.AddListener(() => SwitchPanels(titleMenuPanel, supportAuthorMenuPanel));
            supportAuthorMenuPanel.BackButton.onClick.AddListener(() => SwitchPanels(supportAuthorMenuPanel, titleMenuPanel));

            for (int i = 0; i < selectLevelPanel.LevelButtons.Count; i++)
            {
                int index = i;
                selectLevelPanel.LevelButtons[i].onClick.AddListener(() => LaunchLevel(encounterListAssets[index], index, 0));
            }
        }

        private void LaunchSavedLevel()
        {
            int startEncounterIndex = PlayerPrefs.GetInt("CurrentEncounterIndex");
            int savedLevelIndex = PlayerPrefs.GetInt("SavedLevelIndex");
            LaunchLevel(encounterListAssets[savedLevelIndex], savedLevelIndex, startEncounterIndex);
        }

        private async void LaunchLevel(EncountersListAsset encountersListAsset, int levelIndex, int startEncounterIndex)
        {
            PlayerPrefs.SetInt("SavedLevelIndex", levelIndex);
            levelLoader.SetupLevel(encountersListAsset, startEncounterIndex);
            await fading.FadeIn();
            levelLoader.LoadLevel();
        }
    }
}