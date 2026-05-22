using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace foxRestaurant
{
    public class ItemsSpawner : MonoBehaviour
    {
        [SerializeField] private Item foodItemPrefab;
        private RestaurantEncounter restaurantEncounter;
        private List<ItemSlot> foodItemSpawnSlots;

        public void Init(RestaurantEncounter restaurantEncounter)
        {
            this.restaurantEncounter = restaurantEncounter;
            foodItemSpawnSlots = restaurantEncounter.SlotsManager.SpawnerSlots;
        }

        public void SetFoodItemSpawnSlots(List<ItemSlot> itemSlots)
        {
            foodItemSpawnSlots = itemSlots;
        }

        public void SpawnIngredient()
        {
            var cookerSlot = foodItemSpawnSlots.FirstOrDefault(slot => slot.Empty);
            if (cookerSlot == null)
                return;

            var itemData = restaurantEncounter.DecksManager.GetRandomIngredient();
            SpawnFoodItem(restaurantEncounter, itemData, cookerSlot);
        }

        public Item SpawnFoodItem(RestaurantEncounter restaurantEncounter, ItemData itemData, ItemSlot itemSlot) =>
            SpawnItem(foodItemPrefab, restaurantEncounter, itemData, itemSlot);

        public Item SpawnItem(Item prefab, RestaurantEncounter restaurantEncounter, ItemData itemData, ItemSlot itemSlot, float spawnSoundVolume = 1)
        {
            Item item = Instantiate(prefab);

            item.Init(restaurantEncounter, itemData, spawnSoundVolume);
            itemSlot.SetItem(item);
            item.Slot = itemSlot;

            item.transform.parent = itemSlot.transform;
            item.transform.position = itemSlot.CenterForItem.position;
            item.transform.localScale = Vector3.one;

            return item;
        }
    }
}