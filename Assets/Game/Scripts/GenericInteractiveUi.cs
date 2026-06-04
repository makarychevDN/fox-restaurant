using UnityEngine;

namespace foxRestaurant
{
    public class GenericInteractiveUi : MonoBehaviour
    {
        public void PlayClickSound()
        {
            UISoundPlayer.Instance.PlayClickSound();
        }

        public void PlayHoverSound()
        {
            UISoundPlayer.Instance.PlayHoverSound();
        }
    }
}