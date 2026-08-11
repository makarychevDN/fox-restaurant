using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class DifficultySetupPanel : MonoBehaviour
    {
        [SerializeField] private ContentSizeFittersRebilder contentSizeFittersRebilder;
        [SerializeField] private List<Toggle> difficultyToggles;

        [SerializeField] private Toggle storyModeToggle;
        [SerializeField] private Toggle easyToggle;
        [SerializeField] private Toggle normalToggle;
        [SerializeField] private Toggle hardToggle;

        private void Awake()
        {
            storyModeToggle.onValueChanged.AddListener(OnStoryModeChanged);
            easyToggle.onValueChanged.AddListener(OnEasyChanged);
            normalToggle.onValueChanged.AddListener(OnNormalChanged);
            hardToggle.onValueChanged.AddListener(OnHardChanged);
        }

        private void OnEnable()
        {
            contentSizeFittersRebilder.Rebuild();

            var difficulty = GameSettings.Difficulty;

            if (difficulty == Difficulty.None)
            {
                difficulty = Difficulty.Easy;
                SetDifficulty(Difficulty.Easy);
            }

            UpdateTogglesVisualization(difficulty);
        }

        private void OnStoryModeChanged(bool isOn)
        {
            if (!isOn)
                return;

            SetDifficulty(Difficulty.StoryMode);
        }

        private void OnEasyChanged(bool isOn)
        {
            if (!isOn)
                return;

            SetDifficulty(Difficulty.Easy);
        }

        private void OnNormalChanged(bool isOn)
        {
            if (!isOn)
                return;

            SetDifficulty(Difficulty.Normal);
        }

        private void OnHardChanged(bool isOn)
        {
            if (!isOn)
                return;

            SetDifficulty(Difficulty.Hard);
        }

        private void SetDifficulty(Difficulty difficulty)
        {
            GameSettings.Difficulty = difficulty;
            PlayerPrefs.SetInt("Difficulty", (int)GameSettings.Difficulty);
        }

        private void UpdateTogglesVisualization(Difficulty difficulty)
        {
            storyModeToggle.SetIsOnWithoutNotify(difficulty == Difficulty.StoryMode);
            easyToggle.SetIsOnWithoutNotify(difficulty == Difficulty.Easy);
            normalToggle.SetIsOnWithoutNotify(difficulty == Difficulty.Normal);
            hardToggle.SetIsOnWithoutNotify(difficulty == Difficulty.Hard);
        }
    }
}