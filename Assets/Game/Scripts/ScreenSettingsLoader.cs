using UnityEngine;

namespace foxRestaurant
{
    public class ScreenSettingsLoader : MonoBehaviour
    {
        private const string PREF_RES_INDEX = "Screen_ResIndex";
        private const string PREF_FULLSCREEN = "Screen_Fullscreen";
        private Vector2Int[] allowedResolutions =
{
            new Vector2Int(1280, 720),
            new Vector2Int(1920, 1080),
            new Vector2Int(2560, 1440)
        };

        private void Awake()
        {
            int savedIndex = PlayerPrefs.GetInt(PREF_RES_INDEX, 1);
            bool savedFullscreen =
                PlayerPrefs.GetInt(PREF_FULLSCREEN, 1) == 1;

            savedIndex = Mathf.Clamp(
                savedIndex,
                0,
                allowedResolutions.Length - 1);

            var res = allowedResolutions[savedIndex];

            Screen.SetResolution(
                res.x,
                res.y,
                savedFullscreen);
        }
    }
}