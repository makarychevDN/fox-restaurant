using TMPro;
using UnityEngine;

namespace foxRestaurant
{
    public class DialogueDisplayer : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;
        [SerializeField] private GameObject panel;
        [SerializeField] private AudioSource audioSource;

        private float pitchDelta = 0;
        private float pitch = 1;

        public void Clear() => label.text = "";
        public void SetSound(AudioClip sound) => audioSource.clip = sound;
        public void SetPitch(string inputValue) => pitch = inputValue.ParseFloatSafe();
        public void SetPitchDelta(string inputValue) => pitchDelta = inputValue.ParseFloatSafe();
        public void SetVolume(string inputValue) => audioSource.volume = inputValue.ParseFloatSafe();

        public void Print(string substring, bool playSound = true)
        {
            label.text += substring;

            if (playSound)
            {
                audioSource.pitch = Random.Range(pitch - pitchDelta, pitch + pitchDelta);
                audioSource.Play();
            }
        }

        public void Show()
        {
            panel.SetActive(true);
        }

        public void Hide()
        {
            panel.SetActive(false);
        }
    }
}