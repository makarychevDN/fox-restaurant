using TMPro;
using UnityEngine;

namespace foxRestaurant
{
    public class CustomersUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text hungerPointsIndicator;
        [SerializeField] private TMP_Text patienceIndicator;

        public void Init(Customer customer)
        {
            customer.OnHungerPointsChanged.AddListener(UpdateDisplayingHungerValue);
            customer.OnPatienceChanged.AddListener(UpdateDisplayingPatienceValue);
            UpdateDisplayingHungerValue(customer.HungerPoints);
            UpdateDisplayingPatienceValue(customer.Patience);
        }

        private void UpdateDisplayingHungerValue(int value)
        {
            hungerPointsIndicator.text = value.ToString();
        }

        private void UpdateDisplayingPatienceValue(float value)
        {
            patienceIndicator.text = value.ToString("0");
        }
    }
}