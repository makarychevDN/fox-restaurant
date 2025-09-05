using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class Item : MonoBehaviour
    {
        [field: SerializeField] public ItemSlot Slot { get; set; }

        [SerializeField] private Image image;
        [SerializeField] private ItemStateController itemStateController;
        [SerializeField] private ItemMouseInputController inputController;
        [SerializeField] private ItemArrow itemArrow;
        [SerializeField] private float timeToFry = 5f;
        private float fryingTimer;
        private Level level;
        private ItemData itemData;

        public void SetSlot(ItemSlot slot) => Slot = slot;
        public Image Image => image;
        public float FryingTimer => fryingTimer;
        public float TimeToFry => timeToFry;
        public float TimeToFryLeft => timeToFry - fryingTimer;
        public ItemData ItemData => itemData;

        public void Init(Level level, ItemData itemData)
        {
            this.level = level;
            itemStateController.Init(level, this, itemArrow);
            inputController.Init(itemStateController, this);

            SetItemData(itemData);
        }

        public void SetItemData(ItemData itemData)
        {
            this.itemData = itemData;
            image.sprite = itemData.Sprite;

            Vector3 itemSpriteSize = itemData.Sprite.bounds.size;
            float pixelsPerUnit = itemData.Sprite.pixelsPerUnit;
            itemSpriteSize.y *= pixelsPerUnit;
            itemSpriteSize.x *= pixelsPerUnit;
            itemSpriteSize.z = 1;
            image.rectTransform.sizeDelta = itemSpriteSize;
        }

        public void Fry(float time)
        {
            fryingTimer += time;

            if(fryingTimer >= timeToFry)
            {
                fryingTimer = 0;

                if(itemData.FryingResult != null)
                    SetItemData(itemData.FryingResult);
            }
        }

        public void Slice()
        {
            if (itemData.SlicingResult != null)
                SetItemData(itemData.SlicingResult);

            level.Ticker.TickOnSlice();
        }
    }
}