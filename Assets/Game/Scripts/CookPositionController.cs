using DG.Tweening;
using UnityEngine;

namespace foxRestaurant
{
    public class CookPositionController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public async void HoverSlot(ItemSlot itemSlot)
        {
            animator.SetBool("readyToSlice", true);
            transform.DOMove(Camera.main.ScreenToWorldPoint(itemSlot.transform.position + Vector3.forward * 10), 0.1f);
            await transform.DOScale(new Vector3(1.2f, 0.8f), 0.1f).AsyncWaitForCompletion();
            await transform.DOScale(1f, 0.1f).AsyncWaitForCompletion();
        }

        public void Unhover()
        {
            animator.SetBool("readyToSlice", false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("slice");
            }
        }
    }
}