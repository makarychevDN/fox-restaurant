using System.Linq;
using UnityEngine;

namespace foxRestaurant
{
    public class ItemsSpawner : MonoBehaviour
    {
        [SerializeField] private Item customerItemPrefab;
        [SerializeField] private Item foodItemPrefab;
        [SerializeField] private CustomerData wolf;
        private RestaurantEncounter restaurantEncounter;

        public void Init(RestaurantEncounter restaurantEncounter)
        {
            this.restaurantEncounter = restaurantEncounter;
        }

        public void SpawnIngredient()
        {
            var cookerSlot = restaurantEncounter.SlotsManager.FoodSpawningSlots.FirstOrDefault(slot => slot.Empty);
            if (cookerSlot == null)
                return;

            var itemData = restaurantEncounter.DecksManager.GetRandomIngredient();
            SpawnFoodItem(restaurantEncounter, itemData, cookerSlot);
        }

        public void SpawnCustomerItem()
        {
            var customerSpawnSlot = restaurantEncounter.SlotsManager.CustomerSpawnerSlots.FirstOrDefault(slot => slot.Empty);
            if (customerSpawnSlot == null)
                return;

            var itemData = wolf;
            SpawnItem(customerItemPrefab, restaurantEncounter, itemData, customerSpawnSlot);
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