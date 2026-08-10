using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class SettingsMenuPanel : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button screenSettingsButton;
        [SerializeField] private Button soundSettingsButton;
        [SerializeField] private Button languageSettingsButton;
        [SerializeField] private Button difficultySettingsButton;

        [Header("Back Buttons")]
        [SerializeField] private Button screenSettingsBackButton;
        [SerializeField] private Button soundSettingsBackButton;
        [SerializeField] private Button languageSettingsBackButton;
        [SerializeField] private Button difficultySettingsBackButton;

        [Header("Panels")]
        [SerializeField] private GameObject titlePanel;
        [SerializeField] private GameObject screenSettingsPanel;
        [SerializeField] private GameObject soundSettingsPanel;
        [SerializeField] private GameObject languageSettingsPanel;
        [SerializeField] private GameObject difficultySettingsPanel;

        [Header("Global Back Button")]
        [SerializeField] private Button backButton;

        public Button BackButton => backButton;
        public Button ScreenSettingsBackButton => screenSettingsBackButton;
        public Button SoundSettingsBackButton => soundSettingsBackButton;
        public Button LanguageSettingsBackButton => languageSettingsBackButton;
        public Button DifficultySettingsBackButton => difficultySettingsBackButton;

        public Button ScreenSettingsButton => screenSettingsButton;
        public Button LanguageSettingsButton => languageSettingsButton;
        public Button SoundSettingsButton => soundSettingsButton;
        public Button DifficultySettingsButton => difficultySettingsButton;

        public GameObject TitlePanel => titlePanel;
        public GameObject ScreenSettingsPanel => screenSettingsPanel;
        public GameObject SoundSettingsPanel => soundSettingsPanel;
        public GameObject LanguageSettingsPanel => languageSettingsPanel;
        public GameObject DifficultySettingsPanel => difficultySettingsPanel;
    }
}