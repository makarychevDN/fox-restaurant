using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class RiversideScenarioPart4 : BaseScenario<RestaurantEncounter>
    {
        [SerializeField] private Character red;
        [SerializeField] private CustomerData wolf;
        [SerializeField] private CustomerData hog;
        [SerializeField] private CustomerData cow;
        [SerializeField] private CustomerData bull;
        [SerializeField] private CustomerData toad;
        [SerializeField] private ItemData iceCream;
        [SerializeField] private List<ItemSlot> slots;
        private RestaurantEncounter encounter;

        protected override void InitTyped(RestaurantEncounter encounter) 
        {
            this.encounter = encounter;
        }

        protected override async UniTask StartScenarioTyped(RestaurantEncounter encounter)
        {
            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<UniTask>[] { () => red.Say("До"), () => SpawnIceCream(new List<ItemData>() { iceCream, iceCream, iceCream, iceCream }) },
                AfterInitSpawn = new Func<UniTask>[] { () => red.Say("После") },
                OnFail = new Func<UniTask>[] { () => red.Say("Попробуем снова") },

                Customers = new List<(CustomerData, Func<ItemData>)>
                {
                    (hog, encounter.DecksManager.GetRandomDish),
                    (toad, encounter.DecksManager.GetRandomDish),
                    (cow, encounter.DecksManager.GetRandomDish),
                    (hog, encounter.DecksManager.GetRandomDish),
                    (wolf, encounter.DecksManager.GetRandomDish),
                    (toad, encounter.DecksManager.GetRandomDish),
                    (hog, encounter.DecksManager.GetRandomDish),
                    (cow, encounter.DecksManager.GetRandomDish),
                    (bull, encounter.DecksManager.GetRandomDish),
                    (wolf, encounter.DecksManager.GetRandomDish)
                }
            });
        }

        private async UniTask SpawnIceCream(List<ItemData> itemsToSpawnData)
        {
            for (int i = 0; i < encounter.SlotsManager.BottomRowSlots.Count; i++)
            {
                await UniTask.Delay(500);
                encounter.ItemsSpawner.SpawnFoodItem(encounter, itemsToSpawnData[i], encounter.SlotsManager.BottomRowSlots[i]);
            }
        }
    }
}