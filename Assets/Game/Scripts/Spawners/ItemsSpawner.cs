using System.Linq;
using UnityEngine;

namespace foxRestaurant
{
    public class ItemsSpawner : MonoBehaviour
    {
        [SerializeField] private Item itemPrefab;
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
            SpawnItem(restaurantEncounter, itemData, cookerSlot);
        }

        public Item SpawnItem(RestaurantEncounter restaurantEncounter, ItemData itemData, ItemSlot itemSlot)
        {
            Item item = Instantiate(itemPrefab);

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