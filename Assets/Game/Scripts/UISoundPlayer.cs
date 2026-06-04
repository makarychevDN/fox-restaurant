using UnityEngine;

namespace foxRestaurant
{
    public class UISoundPlayer : MonoBehaviour
    {
        public static UISoundPlayer Instance { get; private set; }

        [SerializeField] private AudioClip uiHoverSound;
        [SerializeField] private AudioClip uiClickSound;
        [SerializeField] private AudioSource audioSource;

        private void Awake()
        {
            Instance = this;
        }

        public void Play(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }

        public void PlayHoverSound()
        {
            Play(uiHoverSound);
        }

        public void PlayClickSound()
        {
            Play(uiClickSound);
        }
    }
}