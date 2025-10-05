using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class Cooker : MonoBehaviour, ITickable
    {
        [SerializeField] private TMP_Text timeLefTextIndicator;
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

            timeLefTextIndicator.text = slot.Item.TimeToFryLeft.ToString("<mspace=1em>0.0s</mspace>");
            timeLeftSliderIndicator.value = slot.Item.FryingTimer;
            slot.Item.Fry(deltaTime);
        }

        private void ItemSettedInSlotHandler(Item item)
        {
            timeLeftSliderIndicator.maxValue = item.TimeToFry;
            timeLefTextIndicator.text = item.TimeToFry.ToString();
        }

        private void ClearIndicators(Item item)
        {
            timeLefTextIndicator.text = "";
            timeLeftSliderIndicator.value = 0;
        }
    }
}