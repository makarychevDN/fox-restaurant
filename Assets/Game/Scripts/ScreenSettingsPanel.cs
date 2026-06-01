using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class ScreenSettingsPanel : MonoBehaviour
    {        
        [SerializeField] private TMP_Dropdown resolutionsDropdown;
        [SerializeField] private Toggle fullscreenToggle;

        private int currentResolutionIndex;
        private bool currentFullscreen;
        private const string PREF_RES_INDEX = "Screen_ResIndex";
        private const string PREF_FULLSCREEN = "Screen_Fullscreen";
        private Vector2Int[] allowedResolutions =
{
            new Vector2Int(1280, 720),
            new Vector2Int(1920, 1080),
            new Vector2Int(2560, 1440)
        };

        private void Start()
        {
            currentResolutionIndex = PlayerPrefs.GetInt(PREF_RES_INDEX, 1);
            currentFullscreen = PlayerPrefs.GetInt(PREF_FULLSCREEN, 1) == 1;
            int savedIndex = PlayerPrefs.GetInt(PREF_RES_INDEX, 1);
            bool savedFullscreen =
                PlayerPrefs.GetInt(PREF_FULLSCREEN, 1) == 1;

            resolutionsDropdown.value = savedIndex;
            fullscreenToggle.isOn = savedFullscreen;

            resolutionsDropdown.onValueChanged
                .AddListener(SetResolution);

            fullscreenToggle.onValueChanged
                .AddListener(SetFullscreen);
        }

        private void SetResolution(int index)
        {
            currentResolutionIndex = index;

            var res = allowedResolutions[index];

            Screen.SetResolution(
                res.x,
                res.y,
                currentFullscreen);

            Save(currentResolutionIndex, currentFullscreen);
        }

        private void SetFullscreen(bool fullscreen)
        {
            currentFullscreen = fullscreen;
            Screen.fullScreen = fullscreen;
            Save(currentResolutionIndex, currentFullscreen);
        }

        private void Save(int resolutionIndex, bool fullscreen)
        {
            PlayerPrefs.SetInt(PREF_RES_INDEX, resolutionIndex);

            PlayerPrefs.SetInt(
                PREF_FULLSCREEN,
                fullscreen ? 1 : 0);

            PlayerPrefs.Save();
        }
    }
}