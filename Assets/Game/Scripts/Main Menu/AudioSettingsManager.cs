using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private List<AudioSetting> audioSettings;

    private const string MasterVolumeParam = "MasterVolume";
    private const float DefaultMasterVolume = 0.5f;
    private const float DefaultChildVolume = 1.0f;

    private void Awake()
    {
        InitializeVolumes();
        BindSliders();
    }

    private void InitializeVolumes()
    {
        foreach (var setting in audioSettings)
        {
            string key = $"Audio_{setting.parameterName}";
            float defaultValue = setting.parameterName == MasterVolumeParam ? DefaultMasterVolume : DefaultChildVolume;
            float savedValue = PlayerPrefs.GetFloat(key, defaultValue);

            ApplyVolume(setting.parameterName, savedValue);

            if (setting.slider != null)
                setting.slider.value = savedValue;
        }
    }

    private void BindSliders()
    {
        foreach (var setting in audioSettings)
        {
            if (setting.slider == null)
                continue;

            setting.slider.onValueChanged.AddListener(value =>
            {
                ApplyVolume(setting.parameterName, value);
                PlayerPrefs.SetFloat($"Audio_{setting.parameterName}", value);
                PlayerPrefs.Save();
            });
        }
    }

    private void ApplyVolume(string parameterName, float value)
    {
        // перевод 0Ц1 в дЅ по формуле log10(x) * 20, с защитой от 0
        float dB = Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f;
        audioMixer.SetFloat(parameterName, dB);
    }

    [Serializable]
    public struct AudioSetting
    {
        public string parameterName; // им€ параметра в AudioMixer
        public Slider slider;        // слайдер на сцене
        [HideInInspector] public float volume; // внутреннее значение (0Ц1)
    }
}