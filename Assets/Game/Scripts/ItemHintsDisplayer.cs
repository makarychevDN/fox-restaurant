using UnityEngine;

namespace foxRestaurant
{
    public class ItemHintsDisplayer : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public void ShowHint(ItemSlot slot)
        {
            animator.SetBool("isDisplaying", true);
            transform.position = slot.CenterForItem.position;
        }

        public void HideHint()
        {
            animator.SetBool("isDisplaying", false);
        }
    }
}