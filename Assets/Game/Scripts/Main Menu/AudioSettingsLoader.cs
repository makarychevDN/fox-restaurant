using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace foxRestaurant
{
    public class AudioSettingsLoader : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private List<string> parameterNames;

        private const string MasterVolumeParam = "MasterVolume";
        private const float DefaultMasterVolume = 0.5f;
        private const float DefaultChildVolume = 1.0f;

        private void Start()
        {
            foreach (var parameterName in parameterNames)
            {
                string key = $"Audio_{parameterName}";
                float defaultValue = parameterName == MasterVolumeParam
                    ? DefaultMasterVolume
                    : DefaultChildVolume;

                if (!PlayerPrefs.HasKey(key))
                {
                    PlayerPrefs.SetFloat(key, defaultValue);
                }

                float value = PlayerPrefs.GetFloat(key);
                audioMixer.ApplyVolume(parameterName, value);
            }

            PlayerPrefs.Save();
        }
    }
}