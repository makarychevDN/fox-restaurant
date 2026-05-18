using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

namespace foxRestaurant
{
    public class RiversideScenarioPart4 : BaseScenario<RestaurantEncounter>
    {
        [SerializeField] private Character red;
        [SerializeField] private CustomerData kitty;
        [SerializeField] private CustomerData doggo;
        [SerializeField] private CustomerData seal;
        [SerializeField] private ItemData iceCream;
        [SerializeField] private List<ItemSlot> slots;
        private RestaurantEncounter encounter;

        protected override void InitTyped(RestaurantEncounter encounter) 
        {
            this.encounter = encounter;
        }

        protected override async Task StartScenarioTyped(RestaurantEncounter encounter)
        {
            await encounter.CurrentWaveManager.ExecuteWave(

                new List<Func<Task>>() { 
                    () => red.Say("до"), 
                    () => SpawnIceCream(new List<ItemData>() { iceCream, iceCream, iceCream, iceCream} )
                },

                () => red.Say("после"),

                (kitty, () => iceCream),
                (kitty, () => iceCream));
        }

        private async Task SpawnIceCream(List<ItemData> itemsToSpawnData)
        {
            var itemSlots = encounter.SlotsManager.Slots.Where(slot => slot.RequiredItemsType == ItemType.Food && slot.gameObject.activeSelf).ToList();
            for (int i = 1; i < itemsToSpawnData.Count; i++)
            {
                await Task.Delay(500);
                encounter.ItemsSpawner.SpawnFoodItem(encounter, itemsToSpawnData[i], itemSlots[i]);
            }
        }
    }
}