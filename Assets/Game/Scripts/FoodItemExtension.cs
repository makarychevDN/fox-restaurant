using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace foxRestaurant
{
    public class FoodItemExtension : MonoBehaviour
    {
        [field: Header("Stats")]
        [field: SerializeField] public int Satiety { get; private set; }
        [field: SerializeField] public float TimeToFry { get; private set; }

        [SerializeField] private ItemsFusionDisplayer fusionDisplayer;
        [SerializeField] private List<ParticleSystem> fryingParticles;
        [SerializeField] private AudioSource friedSound;
        [SerializeField] private ItemUI itemUI;

        public float FryingTimer => fryingTimer;
        public float TimeToFryLeft => TimeToFry - fryingTimer;
        public ItemsFusionDisplayer FusionDisplayer => fusionDisplayer;

        private Item item;
        private float fryingTimer;
        private RestaurantEncounter restaurantEncounter;

        public UnityEvent<int> OnSatietyUpdated;

        public void Init(Item item, RestaurantEncounter restaurantEncounter)
        {
            this.item = item;
            this.restaurantEncounter = restaurantEncounter;
            itemUI.Init(this);
            fusionDisplayer.Init(restaurantEncounter, item);
        }

        public void SetSatiety(int satiety)
        {
            Satiety = satiety;
            OnSatietyUpdated.Invoke(satiety);
        }

        public void Fry(float time)
        {
            ItemData fryingResult = restaurantEncounter.ItemTransitionsManager.GetFryingResult(item.ItemData);
            if (fryingResult == null)
                return;

            fryingTimer += time;

            if (fryingTimer >= TimeToFry)
            {
                fryingTimer = 0;
                Satiety++;
                OnSatietyUpdated.Invoke(Satiety);
                item.SetItemData(fryingResult);
                friedSound.pitch = Random.Range(0.7f, 1.3f);
                friedSound.Play();
                fryingParticles.ForEach(p => p.Play());
            }
        }

        public void Slice()
        {
            ItemData slicingResult = restaurantEncounter.ItemTransitionsManager.GetSlicingResult(item.ItemData);
            if (slicingResult != null)
                item.SetItemData(slicingResult);

            restaurantEncounter.Ticker.TickOnSlice();
            Satiety++;
            OnSatietyUpdated.Invoke(Satiety);
            item.Slot.OnItemSliced.Invoke();
            item.PlayPoofParticles();
        }

        public bool CanBeFried()
        {
            ItemData fryingResult = restaurantEncounter.ItemTransitionsManager.GetFryingResult(item.ItemData);
            return fryingResult != null;
        }
    }
}