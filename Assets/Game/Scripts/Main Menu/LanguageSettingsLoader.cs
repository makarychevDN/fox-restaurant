using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace foxRestaurant
{
    public class LanguageSettingsLoader : MonoBehaviour
    {
        private const string PREF_LANGUAGE = "Game_Language";

        private async void Start()
        {
            await LocalizationSettings.InitializationOperation.ToUniTask();

            int savedLangIndex;

            if (PlayerPrefs.HasKey(PREF_LANGUAGE))
            {
                savedLangIndex = PlayerPrefs.GetInt(PREF_LANGUAGE, 0);
            }
            else
            {
                savedLangIndex = GetLocaleIndexByCode("en");

                if (savedLangIndex == -1)
                    savedLangIndex = 0;

                PlayerPrefs.SetInt(PREF_LANGUAGE, savedLangIndex);
                PlayerPrefs.Save();
            }

            savedLangIndex = Mathf.Clamp(
                savedLangIndex,
                0,
                LocalizationSettings.AvailableLocales.Locales.Count - 1);

            LocalizationSettings.SelectedLocale =
                LocalizationSettings.AvailableLocales.Locales[savedLangIndex];
        }

        private int GetLocaleIndexByCode(string code)
        {
            var locales = LocalizationSettings.AvailableLocales.Locales;

            for (int i = 0; i < locales.Count; i++)
            {
                if (locales[i].Identifier.Code.Equals(
                    code,
                    System.StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}