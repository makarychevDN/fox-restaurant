using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class Item : MonoBehaviour
    {
        [field: Header("Stats")]
        [field: SerializeField] public int Satiety { get; private set; }
        [field: SerializeField] public float TimeToFry { get; private set; }

        [field: Header("Dynamic Links")]
        [field: SerializeField] public ItemData ItemData { get; private set; }
        [field: SerializeField] public ItemSlot Slot { get; set; }

        [field: Header("Setup")]
        [field: SerializeField] public Image Image { get; private set; }
        [SerializeField] private ItemStateController itemStateController;
        [SerializeField] private ItemMouseInputController inputController;
        [SerializeField] private ItemsFusionDisplayer fusionDisplayer;

        private float fryingTimer;
        private Level level;

        public float FryingTimer => fryingTimer;
        public float TimeToFryLeft => TimeToFry - fryingTimer;

        public void Init(Level level, ItemData itemData, int satiety)
        {
            this.level = level;
            Satiety = satiety;
            itemStateController.Init(level, this, fusionDisplayer);
            fusionDisplayer.Init(level, this);
            inputController.Init(itemStateController, this);

            SetItemData(itemData);
        }

        public void SetItemData(ItemData itemData)
        {
            ItemData = itemData;
            Image.sprite = itemData.Sprite;

            Vector3 itemSpriteSize = itemData.Sprite.bounds.size;
            float pixelsPerUnit = itemData.Sprite.pixelsPerUnit;
            itemSpriteSize.y *= pixelsPerUnit;
            itemSpriteSize.x *= pixelsPerUnit;
            itemSpriteSize.z = 1;
            Image.rectTransform.sizeDelta = itemSpriteSize;
        }

        public void Fry(float time)
        {
            fryingTimer += time;

            if(fryingTimer >= TimeToFry)
            {
                fryingTimer = 0;
                Satiety++;

                if (ItemData.FryingResult != null)
                    SetItemData(ItemData.FryingResult);
            }
        }

        public void Slice()
        {
            if (ItemData.SlicingResult != null)
                SetItemData(ItemData.SlicingResult);

            level.Ticker.TickOnSlice();
            Satiety++;
        }
    }
}