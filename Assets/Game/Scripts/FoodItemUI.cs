using TMPro;
using UnityEngine;

namespace foxRestaurant
{
    public class FoodItemUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text satienseIndicator;

        public void Init(FoodItem foodItem)
        {
            foodItem.OnSatietyUpdated.AddListener(UpdateDisplayingValue);
            UpdateDisplayingValue(foodItem.Satiety);
        }

        private void UpdateDisplayingValue(int value)
        {
            satienseIndicator.text = value.ToString();
        }
    }
}