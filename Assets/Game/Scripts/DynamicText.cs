using DG.Tweening;
using TMPro;
using UnityEngine;

namespace foxRestaurant
{
    public class DynamicText : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;

        public async void Init(string text, Color startColor)
        {
            label.text = text;
            label.color = startColor;

            transform.localScale = Vector3.zero;
            await transform.DOScale(Vector3.one, 0.15f).AsyncWaitForCompletion();

            var endColor = new Color(startColor.r, startColor.g, startColor.b, 0);
            label.DOColor(endColor, 3);
            await transform.DOMove(transform.position + Vector3.up * 50, 3).AsyncWaitForCompletion();

            gameObject.SetActive(false);
        }
    }
}