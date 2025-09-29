using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class DialogueDisplayer : MonoBehaviour
    {
        [SerializeField] private TMP_Text actualText;
        [SerializeField] private GameObject panel;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private List<RectTransform> contentSizeFitters;
        [SerializeField] private float pitchDelta = 0;
        [SerializeField] private float pitch = 1;

        public void SetSound(AudioClip sound) => audioSource.clip = sound;
        public void SetPitch(string inputValue) => pitch = inputValue.ParseFloatSafe();
        public void SetPitchDelta(string inputValue) => pitchDelta = inputValue.ParseFloatSafe();
        public void SetVolume(string inputValue) => audioSource.volume = inputValue.ParseFloatSafe();

        public void Print(int substringLength, bool playSound = true)
        {
            actualText.maxVisibleCharacters += substringLength;

            if (playSound)
            {
                audioSource.pitch = Random.Range(pitch - pitchDelta, pitch + pitchDelta);
                audioSource.Play();
            }
        }

        public void Show(string wholeString)
        {
            panel.SetActive(true);
            actualText.text = PreprocessText(wholeString);
            actualText.maxVisibleCharacters = 0;
            RebuildContentSizeFitters();
        }

        public void Hide()
        {
            panel.SetActive(false);
        }

        private void RebuildContentSizeFitters()
        {
            foreach(var fitter in contentSizeFitters)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(fitter);
            }
        }

        public string PreprocessText(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            string processed = Regex.Replace(input, @"<color:([^>]+)>", "<color=$1>");
            processed = Regex.Replace(processed, @"<(pause|delay|voice|chunk|emote|pitch|volume|pitch delta)(:[^>]*)?>", string.Empty);

            return processed;
        }
    }
}