using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class Cooker : MonoBehaviour, ITickable
    {
        [SerializeField] private TMP_Text timeLefTextIndicator;
        [SerializeField] private TMP_Text sLetter;
        [SerializeField] private Slider timeLeftSliderIndicator;

        private ItemSlot slot;

        public void Init(ItemSlot slot)
        {
            this.slot = slot;
            slot.OnItemHasBeenPlaced.AddListener(ItemSettedInSlotHandler);
            slot.OnItemHasBeenRemoved.AddListener(ClearIndicators);
        }

        public void Tick(float deltaTime)
        {
            if (slot.Item == null)
                return;

            timeLefTextIndicator.text = slot.Item.TimeToFryLeft.ToString("0.0");
            timeLeftSliderIndicator.value = slot.Item.FryingTimer;
            slot.Item.Fry(deltaTime);
        }

        private void ItemSettedInSlotHandler(Item item)
        {
            timeLeftSliderIndicator.maxValue = item.TimeToFry;
            sLetter.text = "s";
        }

        private void ClearIndicators(Item item)
        {
            timeLefTextIndicator.text = "-";
            timeLeftSliderIndicator.value = 0;
            sLetter.text = "";
        }

        private void Update()
        {
            Tick(Time.deltaTime);
        }
    }
}