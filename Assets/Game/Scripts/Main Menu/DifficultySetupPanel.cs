using System;
using System.Collections.Generic;
using TMPro;
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
        [SerializeField] private TMP_Text CurrentCustomerPatienceLabel;
        [SerializeField] private TMP_Text NextCustomerPatienceLabel;

        private int defaultCurrentCustomerPatience = 60;
        private int defaultNextCustomerPatience = 45;

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
            UpdatePatienceLabelsDueGameSettings();
        }

        private void UpdateTogglesVisualization(Difficulty difficulty)
        {
            storyModeToggle.SetIsOnWithoutNotify(difficulty == Difficulty.StoryMode);
            easyToggle.SetIsOnWithoutNotify(difficulty == Difficulty.Easy);
            normalToggle.SetIsOnWithoutNotify(difficulty == Difficulty.Normal);
            hardToggle.SetIsOnWithoutNotify(difficulty == Difficulty.Hard);

            UpdateDescriptionDueGameSettings();
        }

        private void UpdatePatienceLabelsDueGameSettings()
        {
            CurrentCustomerPatienceLabel.text = (defaultCurrentCustomerPatience + GameSettings.GetAdditionalPatienceForDifficulty()).ToString();
            NextCustomerPatienceLabel.text = (defaultNextCustomerPatience + GameSettings.GetAdditionalPatienceForDifficulty()).ToString();
        }

        public void UpdateDescriptionDueGameSettings()
        {
            UpdateDescription(GameSettings.Difficulty);
        }

        private void UpdatePatienceLabels(Difficulty difficulty)
        {
            CurrentCustomerPatienceLabel.text = (defaultCurrentCustomerPatience + GameSettings.ConvertDifficultyToAdditionalPatience(difficulty)).ToString();
            NextCustomerPatienceLabel.text = (defaultNextCustomerPatience + GameSettings.ConvertDifficultyToAdditionalPatience(difficulty)).ToString();
        }

        public void UpdateDescription(Difficulty difficulty)
        {
            UpdatePatienceLabels(difficulty);
            switch (difficulty)
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

        public void UpdateDescriptionStoryMode() => UpdateDescription(Difficulty.StoryMode);
        public void UpdateDescriptionEasy() => UpdateDescription(Difficulty.Easy);
        public void UpdateDescriptionNormal() => UpdateDescription(Difficulty.Normal);
        public void UpdateDescriptionHard() => UpdateDescription(Difficulty.Hard);
    }
}