using System.Linq;

namespace foxRestaurant
{
    public class PirateRestaurantScenarioPart1 : RestaurantScenario
    {
        public override void Init(RestaurantEncounter restaurantEncounter)
        {
            restaurantEncounter.ItemsSpawner.SetFoodItemSpawnSlots(
                restaurantEncounter.SlotsManager.Slots.Where(slot => slot.RequiredItemsType == ItemType.Food && slot.gameObject.activeSelf).ToList());
            print(restaurantEncounter.SlotsManager.Slots.Where(slot => slot.RequiredItemsType == ItemType.Food).ToList().Count);
            restaurantEncounter.ItemsSpawner.SpawnIngredient();
            restaurantEncounter.ItemsSpawner.SpawnIngredient();
            print("pirate restaurant scenario part 1 inited");
        }
    }
}