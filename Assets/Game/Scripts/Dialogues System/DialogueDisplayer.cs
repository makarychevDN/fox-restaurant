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

        public void Print(string substring)
        {
            label.text += substring;
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