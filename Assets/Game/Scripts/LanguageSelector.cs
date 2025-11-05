using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace foxRestaurant
{
    public class LanguageSelector : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown languageDropdown;

        private const string PREF_LANGUAGE = "Game_Language";

        private bool isChanging = false;

        private void Start()
        {
            // Заполняем дропдаун именами доступных локалей
            SetupDropdown();

            // Загружаем сохранённый язык
            int savedLangIndex = PlayerPrefs.GetInt(PREF_LANGUAGE, 0);
            savedLangIndex = Mathf.Clamp(savedLangIndex, 0, LocalizationSettings.AvailableLocales.Locales.Count - 1);

            languageDropdown.value = savedLangIndex;
            ApplyLanguage(savedLangIndex);

            // Подписываемся на изменение дропдауна
            languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
        }

        private void SetupDropdown()
        {
            languageDropdown.ClearOptions();

            var options = new System.Collections.Generic.List<string>();
            foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
            {
                // Показываем язык пользователю по названию (например, "English" или "Русский")
                options.Add(locale.LocaleName);
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
            if (index < 0 || index >= LocalizationSettings.AvailableLocales.Locales.Count)
                return;

            isChanging = true;
            languageDropdown.interactable = false;

            var selectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
            await LocalizationSettings.InitializationOperation.Task;
            LocalizationSettings.SelectedLocale = selectedLocale;

            PlayerPrefs.SetInt(PREF_LANGUAGE, index);
            PlayerPrefs.Save();

            languageDropdown.interactable = true;
            isChanging = false;
        }
    }
}