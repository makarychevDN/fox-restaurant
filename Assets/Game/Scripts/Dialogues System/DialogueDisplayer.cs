using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class DialogueDisplayer : MonoBehaviour
    {
        [SerializeField] private TMP_Text actualText;
        [SerializeField] private TMP_Text transparentText;
        [SerializeField] private GameObject panel;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private List<RectTransform> contentSizeFitters;
        [SerializeField] private float pitchDelta = 0;
        [SerializeField] private float pitch = 1;

        public void SetSound(AudioClip sound) => audioSource.clip = sound;
        public void SetPitch(string inputValue) => pitch = inputValue.ParseFloatSafe();
        public void SetPitchDelta(string inputValue) => pitchDelta = inputValue.ParseFloatSafe();
        public void SetVolume(string inputValue) => audioSource.volume = inputValue.ParseFloatSafe();

        public void Print(string substring, bool playSound = true)
        {
            actualText.text += substring;

            if (playSound)
            {
                audioSource.pitch = Random.Range(pitch - pitchDelta, pitch + pitchDelta);
                audioSource.Play();
            }
        }

        public void Show(string wholeString)
        {
            panel.SetActive(true);
            actualText.text = "";
            transparentText.text = wholeString.RemoveTags();
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
    }
}