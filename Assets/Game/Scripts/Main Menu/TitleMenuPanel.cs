using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class TitleMenuPanel : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button restartWaveButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button supportAuthorButton;

        public Button PlayButton => playButton;
        public Button ResumeButton => resumeButton;
        public Button RestartWaveButton => restartWaveButton;
        public Button SettingsButton => settingsButton;
        public Button MainMenuButton => mainMenuButton;
        public Button ExitButton => exitButton;
        public Button SupportAuthorButton => supportAuthorButton;
    }
}
