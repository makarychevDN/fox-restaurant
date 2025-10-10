using TMPro;
using UnityEngine;

namespace foxRestaurant
{
    public class ItemUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text satienseIndicator;

        public void Init(FoodItemExtension foodItemExtension)
        {
            foodItemExtension.OnSatietyUpdated.AddListener(UpdateDisplayingValue);
            UpdateDisplayingValue(foodItemExtension.Satiety);
        }

        private void UpdateDisplayingValue(int value)
        {
            satienseIndicator.text = value.ToString();
        }
    }
}