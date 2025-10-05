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

            if (slot.Item.IsReady)
            {
                timeLeftTextIndicator.text = "<mspace=1em>done</mspace>";
                timeLeftTextIndicator.color = Extensions.HexToColor("#c19a47");
                return;
            }

            slot.Item.Fry(deltaTime); 
            timeLeftTextIndicator.text = slot.Item.TimeToFryLeft.ToString("<mspace=1em>0.0s</mspace>").Replace(',', ':');
            timeLeftSliderIndicator.value = slot.Item.FryingTimer;
        }

        private void ItemSettedInSlotHandler(Item item)
        {
            timeLeftSliderIndicator.maxValue = item.TimeToFry;
            timeLeftTextIndicator.text = item.TimeToFry.ToString();
            timeLeftTextIndicator.color = Extensions.HexToColor(item.IsReady ? "#c19a47" : "#848f2e");
        }

        private void ClearIndicators(Item item)
        {
            timeLeftTextIndicator.text = "<mspace=1em>0:0s</mspace>";
            timeLeftSliderIndicator.value = 0;
            timeLeftTextIndicator.color = Extensions.HexToColor("#4e5615");
        }
    }
}