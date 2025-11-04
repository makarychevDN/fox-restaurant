using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class ScreenManager : MonoBehaviour
    {
        [Header("Target Aspect Ratio")]
        public Vector2 targetAspect = new Vector2(16, 9);

        [Header("Allowed Resolutions")]
        public Vector2Int[] allowedResolutions =
        {
            new Vector2Int(1280, 720),   // HD
            new Vector2Int(1920, 1080),  // Full HD
            new Vector2Int(2560, 1440)   // 2K
        };

        [SerializeField] private TMP_Dropdown resolutonsSelector;
        [SerializeField] private Toggle fullScreenSelector;

        private Camera mainCamera;
        private int prevWidth, prevHeight;
        private bool prevFullScreen;

        private const string PREF_RES_INDEX = "Screen_ResIndex";
        private const string PREF_FULLSCREEN = "Screen_Fullscreen";

        void Awake()
        {
            int savedIndex = PlayerPrefs.GetInt(PREF_RES_INDEX, 1);
            bool savedFullscreen = PlayerPrefs.GetInt(PREF_FULLSCREEN, 1) == 1;

            savedIndex = Mathf.Clamp(savedIndex, 0, allowedResolutions.Length - 1);
            var res = allowedResolutions[savedIndex];
            Screen.SetResolution(res.x, res.y, savedFullscreen);

            mainCamera = Camera.main;
            ApplyAspect();

            resolutonsSelector.value = savedIndex;
            fullScreenSelector.isOn = savedFullscreen;  
        }

        void OnEnable()
        {
            SceneManager.activeSceneChanged += OnSceneChanged;
        }

        void OnDisable()
        {
            SceneManager.activeSceneChanged -= OnSceneChanged;
        }

        private void OnSceneChanged(Scene oldScene, Scene newScene)
        {
            mainCamera = Camera.main;
            ApplyAspect();
        }

        void Update()
        {
            if (Screen.width != prevWidth || Screen.height != prevHeight || Screen.fullScreen != prevFullScreen)
                ApplyAspect();
        }

        public void ToggleFullScreen()
        {
            SetFullScreen(!Screen.fullScreen);
        }

        public void SetFullScreen(bool fullScreen)
        {
            Screen.fullScreen = fullScreen;
            SaveSettings();
            ApplyAspect();
        }

        public void SetResolution(int index)
        {
            if (index < 0 || index >= allowedResolutions.Length) return;
            var res = allowedResolutions[index];
            Screen.SetResolution(res.x, res.y, Screen.fullScreen);
            SaveSettings(index);
            ApplyAspect();
        }

        private void ApplyAspect()
        {
            if (mainCamera == null) mainCamera = Camera.main;
            if (mainCamera == null) return;

            float targetRatio = targetAspect.x / targetAspect.y;
            float windowRatio = (float)Screen.width / Screen.height;
            float scaleHeight = windowRatio / targetRatio;

            Rect rect = mainCamera.rect;

            if (scaleHeight < 1f)
            {
                rect.width = 1f;
                rect.height = scaleHeight;
                rect.x = 0;
                rect.y = (1f - scaleHeight) / 2f;
            }
            else
            {
                float scaleWidth = 1f / scaleHeight;
                rect.width = scaleWidth;
                rect.height = 1f;
                rect.x = (1f - scaleWidth) / 2f;
                rect.y = 0;
            }

            mainCamera.rect = rect;
            prevWidth = Screen.width;
            prevHeight = Screen.height;
            prevFullScreen = Screen.fullScreen;
        }

        private void SaveSettings(int resolutionIndex = -1)
        {
            if (resolutionIndex == -1)
            {
                for (int i = 0; i < allowedResolutions.Length; i++)
                {
                    if (allowedResolutions[i].x == Screen.width && allowedResolutions[i].y == Screen.height)
                    {
                        resolutionIndex = i;
                        break;
                    }
                }
            }

            if (resolutionIndex == -1) resolutionIndex = 1;

            PlayerPrefs.SetInt(PREF_RES_INDEX, resolutionIndex);
            PlayerPrefs.SetInt(PREF_FULLSCREEN, Screen.fullScreen ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
}