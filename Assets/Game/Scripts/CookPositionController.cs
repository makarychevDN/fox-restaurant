using DG.Tweening;
using UnityEngine;

namespace foxRestaurant
{
    public class CookPositionController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public void HoverSlot(ItemSlot itemSlot)
        {
            animator.SetBool("readyToSlice", true);
            transform.DOMove(itemSlot.transform.position + Vector3.forward * 10, 0.1f);
            Squeeze();
        }

        public void Unhover()
        {
            animator.SetBool("readyToSlice", false);
        }

        public async void Slice()
        {
            animator.SetTrigger("slice");
            await transform.DOScale(new Vector3(1.2f, 0.8f), 0.1f).AsyncWaitForCompletion();
            await transform.DOScale(1f, 0.1f).AsyncWaitForCompletion();
        }

        private async void Squeeze()
        {
            await transform.DOScale(new Vector3(1.2f, 0.8f), 0.1f).AsyncWaitForCompletion();
            await transform.DOScale(1f, 0.1f).AsyncWaitForCompletion();
        }
    }
}