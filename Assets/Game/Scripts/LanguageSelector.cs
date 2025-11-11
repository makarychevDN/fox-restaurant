using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace foxRestaurant
{
    public class LanguageSelector : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown languageDropdown;
        private string languageNamesTable = "UI texts"; // имя таблицы
        private string languageNameKey = "Language Name";     // ключ строки

        private const string PREF_LANGUAGE = "Game_Language";
        private bool isChanging = false;

        private async void Start()
        {
            await LocalizationSettings.InitializationOperation.Task;
            await SetupDropdown();

            int savedLangIndex = PlayerPrefs.GetInt(PREF_LANGUAGE, 0);

            if (PlayerPrefs.HasKey(PREF_LANGUAGE))
            {
                savedLangIndex = PlayerPrefs.GetInt(PREF_LANGUAGE, 0);
            }
            else
            {
                // Если нет сохранённого — ставим английский
                savedLangIndex = GetLocaleIndexByCode("en");
                if (savedLangIndex == -1)
                    savedLangIndex = 0; // fallback, если английского вдруг нет
                PlayerPrefs.SetInt(PREF_LANGUAGE, savedLangIndex);
                PlayerPrefs.Save();
            }

            savedLangIndex = Mathf.Clamp(savedLangIndex, 0, LocalizationSettings.AvailableLocales.Locales.Count - 1);

            languageDropdown.value = savedLangIndex;
            ApplyLanguage(savedLangIndex);

            languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
        }

        private async Task SetupDropdown()
        {
            languageDropdown.ClearOptions();

            var locales = LocalizationSettings.AvailableLocales.Locales;
            var options = new List<string>();

            // Загружаем таблицу строк (чтобы убедиться, что она доступна)
            AsyncOperationHandle<StringTable> handle = LocalizationSettings.StringDatabase.GetTableAsync(languageNamesTable);
            await handle.Task;

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogWarning($"Не удалось загрузить таблицу '{languageNamesTable}'");
                foreach (var locale in locales)
                    options.Add(locale.LocaleName);

                languageDropdown.AddOptions(options);
                return;
            }

            // Для каждой локали достаём значение строки из нужной локали
            foreach (var locale in locales)
            {
                string localizedName;

                try
                {
                    localizedName = await LocalizationSettings.StringDatabase.GetLocalizedStringAsync(
                        languageNamesTable,
                        languageNameKey,
                        locale
                    ).Task;
                }
                catch
                {
                    localizedName = locale.LocaleName; // fallback
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

        private int GetLocaleIndexByCode(string code)
        {
            var locales = LocalizationSettings.AvailableLocales.Locales;
            for (int i = 0; i < locales.Count; i++)
            {
                if (locales[i].Identifier.Code.Equals(code, System.StringComparison.OrdinalIgnoreCase))
                    return i;
            }
            return -1;
        }

        [ContextMenu("DELETE ALL SAVES")]
        public void ResetSaves()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}