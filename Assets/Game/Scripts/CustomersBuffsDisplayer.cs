using DG.Tweening;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class CustomersBuffsDisplayer : MonoBehaviour
    {
        [Header("hunger points increased buff")]
        [SerializeField] private GameObject hungerPointsIncreasedBuff;
        [SerializeField] private List<SpriteRenderer> hungerPointsIncreasedBackRenderers;
        [SerializeField] private SpriteRenderer hungerPointsIncreasedFrontRenderer;
        [SerializeField] private AudioSource hungerPointsIncreasedSound;
        [SerializeField] private float hungerPointsIncreasedHeight = 1f;
        [SerializeField] private float moveUpDuration = 1f;

        public void Init(Customer customer)
        {
            customer.OnHungerPointsIncreased.AddListener(DisplayHungerPointsIncreasedBuff);
        }

        public async void DisplayHungerPointsIncreasedBuff()
        {
            hungerPointsIncreasedBuff.SetActive(true);
            hungerPointsIncreasedBuff.transform.localPosition = Vector3.zero;
            hungerPointsIncreasedBuff.transform.localScale = Vector3.zero;
            hungerPointsIncreasedSound.Play();

            // set initial opacities: front = 1, backs = 0.5
            if (hungerPointsIncreasedFrontRenderer != null)
            {
                var c = hungerPointsIncreasedFrontRenderer.color;
                c.a = 1f;
                hungerPointsIncreasedFrontRenderer.color = c;
            }

            if (hungerPointsIncreasedBackRenderers != null)
            {
                foreach (var rend in hungerPointsIncreasedBackRenderers)
                {
                    if (rend == null) continue;
                    var c = rend.color;
                    c.a = 0.5f;
                    rend.color = c;
                }
            }

            // pulse effect: scale to 1.5 then back to 1
            await hungerPointsIncreasedBuff.transform.DOScale(Vector3.one * 1.5f, 0.1f).ToUniTask();
            await hungerPointsIncreasedBuff.transform.DOScale(Vector3.one, 0.1f).ToUniTask();

            // move parent up while fading icons to transparent
            var seq = DOTween.Sequence();
            seq.Append(hungerPointsIncreasedBuff.transform.DOLocalMoveY(hungerPointsIncreasedHeight, moveUpDuration).SetRelative(true));

            if (hungerPointsIncreasedFrontRenderer != null)
                seq.Join(hungerPointsIncreasedFrontRenderer.DOFade(0f, moveUpDuration));

            if (hungerPointsIncreasedBackRenderers != null)
            {
                foreach (var rend in hungerPointsIncreasedBackRenderers)
                {
                    if (rend == null) continue;
                    seq.Join(rend.DOFade(0f, moveUpDuration));
                }
            }

            await seq.Play().ToUniTask();

            // finished, hide parent
            hungerPointsIncreasedBuff.SetActive(false);
        }
    }
}