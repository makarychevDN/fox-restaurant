using UnityEngine;
using UnityEngine.Events;
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
        [SerializeField] private ItemUI itemUI;
        [SerializeField] private ParticleSystem poofParticles;

        private float fryingTimer;
        private Level level;

        public UnityEvent<int> OnSatietyUpdated;

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
            OnSatietyUpdated.Invoke(Satiety);
            itemUI.Init(this);
        }

        public void SetItemData(ItemData itemData)
        {
            ItemData = itemData;
            Image.sprite = itemData.Sprite;
            Image.rectTransform.sizeDelta = itemData.Sprite.GetSpriteSizeInPixels();
        }

        public void Fry(float time)
        {
            fryingTimer += time;

            if(fryingTimer >= TimeToFry)
            {
                fryingTimer = 0;
                Satiety++;
                OnSatietyUpdated.Invoke(Satiety);

                if (ItemData.FryingResult != null)
                    SetItemData(ItemData.FryingResult);
            }
        }

        public void Slice()
        {
            if (ItemData.SlicingResult != null)
                SetItemData(ItemData.SlicingResult);

            level.Ticker.TickOnSlice();
            fryingTimer = 0;
            Satiety++;
            OnSatietyUpdated.Invoke(Satiety);
        }
    }
}