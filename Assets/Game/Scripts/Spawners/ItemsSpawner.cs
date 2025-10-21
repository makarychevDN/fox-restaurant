using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace foxRestaurant
{
    public class ItemsSpawner : MonoBehaviour
    {
        [SerializeField] private Item customerItemPrefab;
        [SerializeField] private Item foodItemPrefab;
        private RestaurantEncounter restaurantEncounter;
        private List<ItemSlot> customerItemSpawnSlots;
        private List<ItemSlot> foodItemSpawnSlots;

        public void Init(RestaurantEncounter restaurantEncounter)
        {
            this.restaurantEncounter = restaurantEncounter;
            foodItemSpawnSlots = restaurantEncounter.SlotsManager.FoodSpawningSlots;
            customerItemSpawnSlots = restaurantEncounter.SlotsManager.CustomerSpawnerSlots;
        }

        public void SetFoodItemSpawnSlots(List<ItemSlot> itemSlots)
        {
            foodItemSpawnSlots = itemSlots;
        }

        public void SetCustomerItemSpawnSlots(List<ItemSlot> itemSlots)
        {
            customerItemSpawnSlots = itemSlots;
        }

        public void SpawnIngredient()
        {
            var cookerSlot = foodItemSpawnSlots.FirstOrDefault(slot => slot.Empty);
            if (cookerSlot == null)
                return;

            var itemData = restaurantEncounter.DecksManager.GetRandomIngredient();
            SpawnFoodItem(restaurantEncounter, itemData, cookerSlot);
        }

        public Item SpawnCustomerItem()
        {
            var customerSpawnSlot = customerItemSpawnSlots.FirstOrDefault(slot => slot.Empty);
            if (customerSpawnSlot == null)
                return null;

            var itemData = restaurantEncounter.DecksManager.GetRandomCustomer();
            return SpawnItem(customerItemPrefab, restaurantEncounter, itemData, customerSpawnSlot);
        }

        public Item SpawnFoodItem(RestaurantEncounter restaurantEncounter, ItemData itemData, ItemSlot itemSlot) =>
            SpawnItem(foodItemPrefab, restaurantEncounter, itemData, itemSlot);

        public Item SpawnItem(Item prefab, RestaurantEncounter restaurantEncounter, ItemData itemData, ItemSlot itemSlot)
        {
            Item item = Instantiate(prefab);

            item.Init(restaurantEncounter, itemData);
            itemSlot.SetItem(item);
            item.Slot = itemSlot;

            item.transform.parent = itemSlot.transform;
            item.transform.position = itemSlot.CenterForItem.position;
            item.transform.localScale = Vector3.one;

            return item;
        }
    }
}