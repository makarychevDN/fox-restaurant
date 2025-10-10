using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class FoodItemExtension : MonoBehaviour
    {
        [field: Header("Stats")]
        [field: SerializeField] public int Satiety { get; private set; }
        [field: SerializeField] public float TimeToFry { get; private set; }

        [field: Header("Dynamic Links")]
        [field: SerializeField] public ItemData ItemData { get; private set; }
        [field: SerializeField] public ItemSlot Slot { get; set; }

        [field: Header("Setup")]
        [field: SerializeField] public Image Image { get; private set; }
        [SerializeField] private Item itemStateController;
        [SerializeField] private ItemMouseInputController inputController;
        [SerializeField] private ItemsFusionDisplayer fusionDisplayer;
        [SerializeField] private ItemUI itemUI;
        [SerializeField] private ParticleSystem poofParticles;
        [SerializeField] private List<ParticleSystem> fryingParticles;
        [SerializeField] private AudioSource friedSound;
        [SerializeField] private AudioSource appearSound;
        [SerializeField] private ItemType itemType;

        private float fryingTimer;
        private RestaurantEncounter restaurantEncounter;
        private bool isReady;

        public UnityEvent<int> OnSatietyUpdated;

        public float FryingTimer => fryingTimer;
        public float TimeToFryLeft => TimeToFry - fryingTimer;
        public ItemType ItemType => itemType;

        public bool CanBeFried()
        {
            ItemData fryingResult = restaurantEncounter.ItemTransitionsManager.GetFryingResult(ItemData);
            return fryingResult != null;
        }

        public void Init(RestaurantEncounter restaurantEncounter, ItemData itemData, int satiety)
        {
            this.restaurantEncounter = restaurantEncounter;
            Satiety = satiety;
            itemStateController.Init(restaurantEncounter, this, fusionDisplayer);
            fusionDisplayer.Init(restaurantEncounter, this);
            inputController.Init(itemStateController);
            SetItemData(itemData);
            OnSatietyUpdated.Invoke(Satiety);
            itemUI.Init(this);
            appearSound.pitch = Random.Range(0.7f, 1.3f);
        }

        public void SetItemData(ItemData itemData)
        {
            ItemData = itemData;
            Image.sprite = itemData.Sprite;
            Image.rectTransform.sizeDelta = itemData.Sprite.GetSpriteSizeInPixels();
        }

        public void Fry(float time)
        {
            ItemData fryingResult = restaurantEncounter.ItemTransitionsManager.GetFryingResult(ItemData);
            if (fryingResult == null)
                return;

            fryingTimer += time;

            if(fryingTimer >= TimeToFry)
            {
                isReady = true;
                fryingTimer = 0;
                Satiety++;
                OnSatietyUpdated.Invoke(Satiety);
                SetItemData(fryingResult);
                friedSound.pitch = Random.Range(0.7f, 1.3f);
                friedSound.Play();
                fryingParticles.ForEach(p => p.Play());
            }
        }

        public void Slice()
        {
            ItemData slicingResult = restaurantEncounter.ItemTransitionsManager.GetSlicingResult(ItemData);
            if (slicingResult != null)
                SetItemData(slicingResult);

            restaurantEncounter.Ticker.TickOnSlice();
            Satiety++;
            OnSatietyUpdated.Invoke(Satiety);
            Slot.OnItemSliced.Invoke();
            poofParticles.Play();
        }
    }

    public enum ItemType
    {
        Food = 5,
        Customer = 9
    }
}