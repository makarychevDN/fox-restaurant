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
            if (slot.Item == null)
                return;

            slot.Item.Fry(deltaTime);
            DisplayInfo(slot.Item);
        }

        private void DisplayInfo(Item item)
        {
            if (!item.FoodItemExtension.CanBeFried())
            {
                timeLeftTextIndicator.text = "<mspace=1em>done</mspace>";
                timeLeftTextIndicator.color = Extensions.HexToColor("#c19a47");
                return;
            }

            timeLeftTextIndicator.color = Extensions.HexToColor("#848f2e");
            timeLeftTextIndicator.text = slot.Item.FoodItemExtension.
                TimeToFryLeft.ToString("<mspace=1em>0.0s</mspace>").Replace(',', ':');
            timeLeftSliderIndicator.value = slot.Item.FoodItemExtension.FryingTimer;
        }

        private void ItemSettedInSlotHandler(Item item)
        {
            timeLeftSliderIndicator.maxValue = item.FoodItemExtension.TimeToFry;
            timeLeftTextIndicator.text = item.FoodItemExtension.TimeToFry.ToString();
            timeLeftTextIndicator.color = Extensions.HexToColor(item.FoodItemExtension.CanBeFried() ? "#848f2e" : "#c19a47");
        }

        private void ClearIndicators(Item item)
        {
            timeLeftTextIndicator.text = "<mspace=1em>0:0s</mspace>";
            timeLeftSliderIndicator.value = 0;
            timeLeftTextIndicator.color = Extensions.HexToColor("#4e5615");
        }
    }
}