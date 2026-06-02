using System;
using UnityEngine;

namespace foxRestaurant
{
    public class MainMenu : Menu
    {
        [SerializeField] private EncountersListAsset firstLevelEncountersListAsset;
        [SerializeField] private LevelLoader levelLoader;

        protected override void Init()
        {
            fading.material = new Material(fading.material);
            fading.FadeOut();
            base.Init();
        }

        protected override void InitTitleMenuPanel()
        {
            base.InitTitleMenuPanel();
            TitleMenuPanel.PlayButton.onClick.AddListener(LaunchTheFirstLevel);
        }

        private async void LaunchTheFirstLevel()
        {
            levelLoader.SetEncaunters(firstLevelEncountersListAsset);
            await fading.FadeIn();
            levelLoader.LoadLevel();
        }
    }
}