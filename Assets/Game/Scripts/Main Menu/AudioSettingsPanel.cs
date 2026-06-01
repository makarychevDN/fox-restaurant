using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class AudioSettingsPanel : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private List<AudioSetting> audioSettings;

        private void Start()
        {
            foreach (var setting in audioSettings)
            {
                string key = $"Audio_{setting.parameterName}";
                setting.slider.value = PlayerPrefs.GetFloat(key);

                setting.slider.onValueChanged.AddListener(value =>
                {
                    audioMixer.ApplyVolume(setting.parameterName, value);
                    PlayerPrefs.SetFloat($"Audio_{setting.parameterName}", value);
                    PlayerPrefs.Save();
                });
            }
        }

        [Serializable]
        public struct AudioSetting
        {
            public string parameterName; // имя параметра в AudioMixer
            public Slider slider;        // слайдер на сцене
            [HideInInspector] public float volume; // внутреннее значение (0–1)
        }
    }
}