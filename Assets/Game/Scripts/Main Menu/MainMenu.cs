using System;
using UnityEngine;

namespace foxRestaurant
{
    public class MainMenu : Menu
    {
        [SerializeField] private EncountersListAsset firstLevelEncountersListAsset;
        [SerializeField] private LevelLoader levelLoader;
        [SerializeField] protected SupportAuthorMenuPanel supportAuthorMenuPanel;

        protected override void Init()
        {
            fading.material = new Material(fading.material);
            fading.FadeOut();
            base.Init();
        }

        protected override void InitTitleMenuPanel()
        {
            base.InitTitleMenuPanel();
            titleMenuPanel.PlayButton.onClick.AddListener(LaunchTheFirstLevel);
            titleMenuPanel.SupportAuthorButton.onClick.AddListener(() => SwitchPanels(titleMenuPanel, supportAuthorMenuPanel));
            supportAuthorMenuPanel.BackButton.onClick.AddListener(() => SwitchPanels(supportAuthorMenuPanel, titleMenuPanel));
        }

        private async void LaunchTheFirstLevel()
        {
            levelLoader.SetEncaunters(firstLevelEncountersListAsset);
            await fading.FadeIn();
            levelLoader.LoadLevel();
        }
    }
}