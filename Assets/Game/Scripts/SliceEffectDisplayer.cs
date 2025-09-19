using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class SliceEffectDisplayer : MonoBehaviour
    {
        [SerializeField] private Image sliceEffect;

        public async void Play(ItemSlot itemSlot)
        {
            sliceEffect.transform.position = itemSlot.CenterForItem.position;
            sliceEffect.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(-25, 25));
            sliceEffect.transform.localScale = new Vector3(Random.Range(0.8f, 1.2f), Random.Range(0.8f, 1.2f), 1f);

            sliceEffect.fillClockwise = true;
            await sliceEffect.DOFillAmount(1, 0.125f).AsyncWaitForCompletion();

            sliceEffect.fillClockwise = false;
            await sliceEffect.DOFillAmount(0, 0.125f).AsyncWaitForCompletion();
        }
    }
}