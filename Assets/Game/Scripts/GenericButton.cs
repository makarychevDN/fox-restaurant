using UnityEngine;

namespace foxRestaurant
{
    public class GenericButton : MonoBehaviour
    {
        [SerializeField] private AudioClip clickClip;
        [SerializeField] private AudioClip hoverSound;

        public void PlayClickSound()
        {
            UISoundPlayer.Instance.Play(clickClip);
        }

        public void PlayHoverSound()
        {
            UISoundPlayer.Instance.Play(hoverSound);
        }
    }
}