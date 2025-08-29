using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class Item : MonoBehaviour
    {
        [field: SerializeField] public ItemSlot Slot { get; set; }

        [SerializeField] private Image image;
        [SerializeField] private ItemMovement itemMovement;
        [SerializeField] private ItemMouseInputController inputController;
        [SerializeField] private float timeToFry = 5f;
        private float fryingTimer;
        private Level level;
        private ItemData itemData;

        public void SetSlot(ItemSlot slot) => Slot = slot;
        public Image Image => image;
        public float FryingTimer => fryingTimer;
        public float TimeToFry => timeToFry;
        public float TimeToFryLeft => timeToFry - fryingTimer;

        public void Init(Level level, ItemData itemData)
        {
            this.level = level;
            itemMovement.Init(level, this);
            inputController.Init(itemMovement);

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
    }
}