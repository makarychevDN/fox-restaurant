using UnityEngine;

namespace foxRestaurant
{
    public class UISoundPlayer : MonoBehaviour
    {
        public static UISoundPlayer Instance { get; private set; }

        [SerializeField] private AudioSource audioSource;

        private void Awake()
        {
            Instance = this;
        }

        public void Play(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}