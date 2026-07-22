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
        [SerializeField] private Animator hungerPointsAnimator;

        [Header("patience")]
        [SerializeField] private TMP_Text patienceIndicator;

        public void Init(Customer customer)
        {
            customer.OnHungerPointsChanged.AddListener(UpdateDisplayingHungerValueWithAnimation);
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
        }

        private void UpdateDisplayingHungerValueWithAnimation(int value, int maxValue)
        {
            UpdateDisplayingHungerValue(value, maxValue);
            hungerPointsAnimator.SetTrigger("triggered");
        }

        private void UpdateDisplayingPatienceValue(float value)
        {
            patienceIndicator.text = value.ToString("0");
        }
    }
}