using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace foxRestaurant
{
    public class FoodItem : Item
    {
        [field: Header("Stats")]
        [field: SerializeField] public int Satiety { get; private set; }
        [field: SerializeField] public float TimeToFry { get; private set; }

        [SerializeField] private ItemsFusionDisplayer fusionDisplayer;
        [SerializeField] private List<ParticleSystem> fryingParticles;
        [SerializeField] private AudioSource friedSound;
        [SerializeField] private FoodItemUI itemUI;

        private float fryingTimer;

        public float FryingTimer => fryingTimer;
        public float TimeToFryLeft => TimeToFry - fryingTimer;
        public ItemsFusionDisplayer FusionDisplayer => fusionDisplayer;

        public UnityEvent<int> OnSatietyUpdated;

        public override void Init(RestaurantEncounter restaurantEncounter, ItemData itemData)
        {
            base.Init(restaurantEncounter, itemData);
            itemUI.Init(this);
            fusionDisplayer.Init(restaurantEncounter, this);
        }

        public void SetSatiety(int satiety)
        {
            Satiety = satiety;
            OnSatietyUpdated.Invoke(satiety);
        }

        public void Fry(float time)
        {
            ItemData fryingResult = restaurantEncounter.ItemTransitionsManager.GetFryingResult(ItemData);
            if (fryingResult == null)
                return;

            fryingTimer += time;

            if (fryingTimer >= TimeToFry)
            {
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
            PlayPoofParticles();
        }

        public bool CanBeFried()
        {
            ItemData fryingResult = restaurantEncounter.ItemTransitionsManager.GetFryingResult(ItemData);
            return fryingResult != null;
        }
    }
}