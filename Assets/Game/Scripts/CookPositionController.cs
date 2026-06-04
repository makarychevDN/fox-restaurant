using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace foxRestaurant
{
    public class CookPositionController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private AudioSource changePositionSound;

        public void HoverSlot(ItemSlot itemSlot)
        {
            changePositionSound.pitch = Random.Range(0.25f, 1.75f);
            changePositionSound.Play();
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
            await transform.DOScale(new Vector3(1.2f, 0.8f), 0.1f).ToUniTask();
            await transform.DOScale(1f, 0.1f).ToUniTask();
        }

        private async void Squeeze()
        {
            await transform.DOScale(new Vector3(1.2f, 0.8f), 0.1f).ToUniTask();
            await transform.DOScale(1f, 0.1f).ToUniTask();
        }
    }
}