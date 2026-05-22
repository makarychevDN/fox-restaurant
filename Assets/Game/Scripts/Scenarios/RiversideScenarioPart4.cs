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
        [SerializeField] private CustomerData kitty;
        [SerializeField] private CustomerData doggo;
        [SerializeField] private CustomerData seal;
        [SerializeField] private CustomerData wolf;
        [SerializeField] private ItemData iceCream;
        [SerializeField] private List<ItemSlot> slots;
        private RestaurantEncounter encounter;

        protected override void InitTyped(RestaurantEncounter encounter) 
        {
            this.encounter = encounter;
        }

        protected override async Task StartScenarioTyped(RestaurantEncounter encounter)
        {
            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<Task>[] { () => red.Say("До"), () => SpawnIceCream(new List<ItemData>() { iceCream, iceCream, iceCream, iceCream }) },
                AfterInitSpawn = new Func<Task>[] { () => red.Say("После") },
                OnFail = new Func<Task>[] { () => red.Say("Попробуем снова") },

                Customers = new List<(CustomerData, Func<ItemData>)>
                {
                    (doggo, encounter.DecksManager.GetRandomDish),
                    (kitty, encounter.DecksManager.GetRandomDish),
                    (kitty, encounter.DecksManager.GetRandomDish),
                    (kitty, encounter.DecksManager.GetRandomDish),
                    (kitty, encounter.DecksManager.GetRandomDish),
                    (kitty, encounter.DecksManager.GetRandomDish),
                    (doggo, encounter.DecksManager.GetRandomDish),
                    (doggo, encounter.DecksManager.GetRandomDish),
                    (kitty, encounter.DecksManager.GetRandomDish)
                }
            });
        }

        private async Task SpawnIceCream(List<ItemData> itemsToSpawnData)
        {
            for (int i = 0; i < encounter.SlotsManager.BottomRowSlots.Count; i++)
            {
                await Task.Delay(500);
                encounter.ItemsSpawner.SpawnFoodItem(encounter, itemsToSpawnData[i], encounter.SlotsManager.BottomRowSlots[i]);
            }
        }
    }
}