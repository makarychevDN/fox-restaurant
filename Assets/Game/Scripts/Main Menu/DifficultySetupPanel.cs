using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class DifficultySetupPanel : MonoBehaviour
    {
        [SerializeField] private List<Toggle> difficultyToggles;

        [SerializeField] private Toggle storyModeToggle;
        [SerializeField] private Toggle easyToggle;
        [SerializeField] private Toggle normalToggle;
        [SerializeField] private Toggle hardToggle;
        [SerializeField] private LocalizedString storyModeDifficultyDescription;
        [SerializeField] private LocalizedString easyDifficultyDescription;
        [SerializeField] private LocalizedString normalDifficultyDescription;
        [SerializeField] private LocalizedString hardDifficultyDescription;
        [SerializeField] private LocalizeStringEvent difficultyDescriptionEvent;

        private void Awake()
        {
            storyModeToggle.onValueChanged.AddListener(OnStoryModeChanged);
            easyToggle.onValueChanged.AddListener(OnEasyChanged);
            normalToggle.onValueChanged.AddListener(OnNormalChanged);
            hardToggle.onValueChanged.AddListener(OnHardChanged);
        }

        private void OnEnable()
        {
            var difficulty = GameSettings.Difficulty;

            if (difficulty == Difficulty.None)
            {
                difficulty = Difficulty.Easy;
                SetDifficulty(Difficulty.Easy, easyDifficultyDescription);
            }

            UpdateTogglesVisualization(difficulty);
        }

        private void OnStoryModeChanged(bool isOn)
        {
            if (!isOn)
                return;

            SetDifficulty(Difficulty.StoryMode, storyModeDifficultyDescription);
        }

        private void OnEasyChanged(bool isOn)
        {
            if (!isOn)
                return;

            SetDifficulty(Difficulty.Easy, easyDifficultyDescription);
        }

        private void OnNormalChanged(bool isOn)
        {
            if (!isOn)
                return;

            SetDifficulty(Difficulty.Normal, normalDifficultyDescription);
        }

        private void OnHardChanged(bool isOn)
        {
            if (!isOn)
                return;

            SetDifficulty(Difficulty.Hard, hardDifficultyDescription);
        }

        private void SetDifficulty(Difficulty difficulty, LocalizedString difficultyDescription)
        {
            GameSettings.Difficulty = difficulty;
            PlayerPrefs.SetInt("Difficulty", (int)GameSettings.Difficulty);
            difficultyDescriptionEvent.StringReference = difficultyDescription;
        }

        private void UpdateTogglesVisualization(Difficulty difficulty)
        {
            storyModeToggle.SetIsOnWithoutNotify(difficulty == Difficulty.StoryMode);
            easyToggle.SetIsOnWithoutNotify(difficulty == Difficulty.Easy);
            normalToggle.SetIsOnWithoutNotify(difficulty == Difficulty.Normal);
            hardToggle.SetIsOnWithoutNotify(difficulty == Difficulty.Hard);

            switch (GameSettings.Difficulty)
            {
                case Difficulty.StoryMode:
                    difficultyDescriptionEvent.StringReference = storyModeDifficultyDescription;
                    break;
                case Difficulty.Easy:
                    difficultyDescriptionEvent.StringReference = easyDifficultyDescription;
                    break;
                case Difficulty.Normal:
                    difficultyDescriptionEvent.StringReference = normalDifficultyDescription;
                    break;
                case Difficulty.Hard:
                    difficultyDescriptionEvent.StringReference = hardDifficultyDescription;
                    break;
            }
        }
    }
}