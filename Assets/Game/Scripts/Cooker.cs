using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class Cooker : MonoBehaviour, ITickable
    {
        [SerializeField] private TMP_Text timeLeftTextIndicator;
        [SerializeField] private Slider timeLeftSliderIndicator;
        [SerializeField] private Image image;
        [SerializeField] private Sprite coldCookerSprite;
        [SerializeField] private Sprite hotCookerSprite;

        private ItemSlot slot;

        public void Init(ItemSlot slot, RestaurantEncounter restaurantEncounter)
        {
            this.slot = slot;
            slot.OnItemHasBeenPlaced.AddListener(ItemSettedInSlotHandler);
            slot.OnItemHasBeenRemoved.AddListener(ClearIndicators);
            restaurantEncounter.Ticker.AddTickable(this);
        }

        public void Tick(float deltaTime)
        {
            if (slot.FoodItemExtension == null)
                return;

            slot.FoodItemExtension.Fry(deltaTime);
            DisplayInfo(slot.FoodItemExtension);
        }

        private void DisplayInfo(FoodItemExtension foodItemExtension)
        {
            if (!foodItemExtension.CanBeFried())
            {
                timeLeftTextIndicator.text = "<mspace=1em>done</mspace>";
                timeLeftTextIndicator.color = Extensions.HexToColor("#c19a47");
                return;
            }

            timeLeftTextIndicator.color = Extensions.HexToColor("#848f2e");
            timeLeftTextIndicator.text = slot.FoodItemExtension.TimeToFryLeft.ToString("<mspace=1em>0.0s</mspace>").Replace(',', ':');
            timeLeftSliderIndicator.value = slot.FoodItemExtension.FryingTimer;
        }

        private void ItemSettedInSlotHandler(FoodItemExtension foodItemExtension)
        {
            timeLeftSliderIndicator.maxValue = foodItemExtension.TimeToFry;
            timeLeftTextIndicator.text = foodItemExtension.TimeToFry.ToString();
            timeLeftTextIndicator.color = Extensions.HexToColor(foodItemExtension.CanBeFried() ? "#848f2e" : "#c19a47");
        }

        private void ClearIndicators(FoodItemExtension foodItemExtension)
        {
            timeLeftTextIndicator.text = "<mspace=1em>0:0s</mspace>";
            timeLeftSliderIndicator.value = 0;
            timeLeftTextIndicator.color = Extensions.HexToColor("#4e5615");
        }
    }
}