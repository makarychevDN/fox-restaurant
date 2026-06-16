using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class CustomersUI : MonoBehaviour
    {
        [Header("hunger points")]
        [SerializeField] private Transform hungerPointsParent;
        [SerializeField] private Transform hungerPointsIcon;
        [SerializeField] private Image hungerPointsFillArea;
        [SerializeField] private Image extraHungerPointsIcon;
        [SerializeField] private TMP_Text hungerPointsIndicator;

        [Header("patience")]
        [SerializeField] private TMP_Text patienceIndicator;

        public void Init(Customer customer)
        {
            customer.OnHungerPointsChanged.AddListener(UpdateDisplayingHungerValue);
            customer.OnPatienceChanged.AddListener(UpdateDisplayingPatienceValue);
            UpdateDisplayingHungerValue(customer.HungerPoints, customer.MaxHungerPoints);
            UpdateDisplayingPatienceValue(customer.Patience);
        }

        private void UpdateDisplayingHungerValue(int value, int maxValue)
        {
            bool extraHungerPoints = value > maxValue;
            extraHungerPointsIcon.gameObject.SetActive(extraHungerPoints);
            hungerPointsIcon.gameObject.SetActive(!extraHungerPoints);
            hungerPointsFillArea.fillAmount = (float)value / maxValue;
            hungerPointsIndicator.text = value.ToString();
            PlayAnimation(hungerPointsParent);
        }

        private void UpdateDisplayingPatienceValue(float value)
        {
            patienceIndicator.text = value.ToString("0");
        }

        private async void PlayAnimation(Transform icon, float animationTime = 0.3f)
        {
            icon.transform.DORotate(new Vector3(0, 0, -30), animationTime * 0.5f);
            await icon.DOScale(3f, animationTime * 0.5f).ToUniTask();
            icon.DORotate(new Vector3(0, 0, 0), animationTime * 0.5f);
            icon.DOScale(1f, animationTime * 0.5f);
        }
    }
}