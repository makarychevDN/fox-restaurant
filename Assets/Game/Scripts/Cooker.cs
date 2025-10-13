using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

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
            if (slot.Item == null)
                return;

            var foodItem = slot.Item as FoodItem;
            if (foodItem == null)
                return;

            foodItem.Fry(deltaTime);
            DisplayInfo(slot.Item);
        }

        private void DisplayInfo(Item item)
        {
            var foodItem = item as FoodItem;
            if (foodItem == null)
                return;

            if (!foodItem.CanBeFried())
            {
                timeLeftTextIndicator.text = "<mspace=1em>done</mspace>";
                timeLeftTextIndicator.color = Extensions.HexToColor("#c19a47");
                return;
            }

            timeLeftTextIndicator.color = Extensions.HexToColor("#848f2e");
            timeLeftTextIndicator.text = foodItem.TimeToFryLeft.ToString("<mspace=1em>0.0s</mspace>").Replace(',', ':');
            timeLeftSliderIndicator.value = foodItem.FryingTimer;
        }

        private void ItemSettedInSlotHandler(Item item)
        {
            var foodItem = item as FoodItem;
            if (foodItem == null)
                return;

            timeLeftSliderIndicator.maxValue = foodItem.TimeToFry;
            timeLeftTextIndicator.text = foodItem.TimeToFry.ToString();
            timeLeftTextIndicator.color = Extensions.HexToColor(foodItem.CanBeFried() ? "#848f2e" : "#c19a47");
        }

        private void ClearIndicators(Item item)
        {
            timeLeftTextIndicator.text = "<mspace=1em>0:0s</mspace>";
            timeLeftSliderIndicator.value = 0;
            timeLeftTextIndicator.color = Extensions.HexToColor("#4e5615");
        }
    }
}