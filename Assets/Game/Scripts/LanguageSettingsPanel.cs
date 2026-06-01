using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace foxRestaurant
{
    public class LanguageSettingsPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown languageDropdown;

        private const string PREF_LANGUAGE = "Game_Language";

        private string languageNamesTable = "UI texts";
        private string languageNameKey = "Language Name";

        private bool isChanging;

        private async void Start()
        {
            await LocalizationSettings.InitializationOperation.Task;

            await SetupDropdown();

            int savedIndex = PlayerPrefs.GetInt(PREF_LANGUAGE, 0);

            languageDropdown.value = savedIndex;
            languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
        }

        private async System.Threading.Tasks.Task SetupDropdown()
        {
            languageDropdown.ClearOptions();

            var locales = LocalizationSettings.AvailableLocales.Locales;
            var options = new List<string>();

            foreach (var locale in locales)
            {
                string localizedName;

                try
                {
                    localizedName =
                        await LocalizationSettings.StringDatabase
                            .GetLocalizedStringAsync(
                                languageNamesTable,
                                languageNameKey,
                                locale).Task;
                }
                catch
                {
                    localizedName = locale.LocaleName;
                }

                options.Add(localizedName);
            }

            languageDropdown.AddOptions(options);
        }

        private void OnLanguageChanged(int index)
        {
            if (!isChanging)
                ApplyLanguage(index);
        }

        private async void ApplyLanguage(int index)
        {
            if (index < 0 ||
                index >= LocalizationSettings.AvailableLocales.Locales.Count)
                return;

            isChanging = true;

            languageDropdown.interactable = false;

            await LocalizationSettings.InitializationOperation.Task;

            LocalizationSettings.SelectedLocale =
                LocalizationSettings.AvailableLocales.Locales[index];

            PlayerPrefs.SetInt(PREF_LANGUAGE, index);
            PlayerPrefs.Save();

            languageDropdown.interactable = true;

            isChanging = false;
        }
    }
}
