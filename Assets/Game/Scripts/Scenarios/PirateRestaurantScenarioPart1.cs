using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class PirateRestaurantScenarioPart1 : BaseScenario<RestaurantEncounter>
    {
        [SerializeField] private ItemData iceCreamConeData;
        [SerializeField] private ItemData iceCreamPlateData;
        [SerializeField] private ItemData cherrySyrupData;
        [SerializeField] private ItemData cherryIceCreamPlateData;

        protected override async Task StartScenarioTyped(RestaurantEncounter ecnounter)
        {
            ecnounter.ItemsSpawner.SetFoodItemSpawnSlots(
                ecnounter.SlotsManager.Slots.Where(slot => slot.RequiredItemsType == ItemType.Food && slot.gameObject.activeSelf).ToList());
            print(ecnounter.SlotsManager.Slots.Where(slot => slot.RequiredItemsType == ItemType.Food).ToList().Count);
            ecnounter.ItemsSpawner.SpawnIngredient();
            ecnounter.ItemsSpawner.SpawnIngredient();
            print("pirate restaurant scenario part 1 inited");

            for (int i = 0; i < 100; i++)
                ecnounter.CustomerSpawner.TryToSpawnCustomer(ecnounter.DecksManager.GetRandomCustomer(), () => iceCreamConeData);

            await Task.Delay(5000);
        }
    }
}