using TMPro;
using UnityEngine;

namespace foxRestaurant
{
    public class DialogueDisplayer : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;
        [SerializeField] private GameObject panel;
        [SerializeField] private AudioSource audioSource;

        public void Clear() => label.text = "";
        public void SetSound(AudioClip sound) => audioSource.clip = sound;

        public void Print(string substring, bool playSound = true)
        {
            label.text += substring;

            if(playSound)
                audioSource.Play();
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