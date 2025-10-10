using System.Linq;
using UnityEngine;

namespace foxRestaurant
{
    public class ItemsSpawner : MonoBehaviour
    {
        [SerializeField] private FoodItemExtension itemPrefab;
        private RestaurantEncounter restaurantEncounter;

        public void Init(RestaurantEncounter restaurantEncounter)
        {
            this.restaurantEncounter = restaurantEncounter;
        }

        public void SpawnIngredient()
        {
            var cookerSlot = restaurantEncounter.SlotsManager.SpawningSlots.FirstOrDefault(slot => slot.Empty);
            if (cookerSlot == null)
                return;

            var itemData = restaurantEncounter.DecksManager.GetRandomIngredient();
            SpawnItem(restaurantEncounter, itemData, cookerSlot, 1 + itemData.AdditionalSatiety);
        }

        public void SpawnItem(RestaurantEncounter restaurantEncounter, ItemData itemData, ItemSlot itemSlot, int satiety)
        {
            FoodItemExtension foodItemExtension = Instantiate(itemPrefab);

            foodItemExtension.Init(restaurantEncounter, itemData, satiety);
            itemSlot.SetItem(foodItemExtension);
            foodItemExtension.Slot = itemSlot;

            foodItemExtension.transform.parent = itemSlot.transform;
            foodItemExtension.transform.position = itemSlot.CenterForItem.position;
            foodItemExtension.transform.localScale = Vector3.one;
        }
    }
}