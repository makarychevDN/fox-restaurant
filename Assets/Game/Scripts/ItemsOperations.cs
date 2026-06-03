using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class ItemsOperations : MonoBehaviour
    {
        private RestaurantEncounter encounter;

        public void Init(RestaurantEncounter encounter)
        {
            this.encounter = encounter;
        }

        public async UniTask SpawnStartItems(List<ItemData> itemsToSpawnData)
        {
            encounter.SlotsManager.BottomRowSlots.ForEach(slot => slot.Clear());
            encounter.SlotsManager.SpawnerSlots.ForEach(slot => slot.Clear());
            FindObjectsOfType<Item>().ToList().ForEach(item => Destroy(item.gameObject));

            for (int i = 0; i < itemsToSpawnData.Count; i++)
            {
                encounter.ItemsSpawner.SpawnFoodItem(encounter, itemsToSpawnData[i], encounter.SlotsManager.BottomRowSlots[i]);
                await UniTask.Delay(500);
            }
        }

        public async UniTask SpawnStartItems()
        {
            List<ItemData> itemsToSpawn = new List<ItemData>();
            int counter = 0;
            while(counter < encounter.SlotsManager.BottomRowSlots.Count)
            {
                itemsToSpawn.Add(encounter.DecksManager.GetRandomIngredient());
                counter++;
            }

            await SpawnStartItems(itemsToSpawn);
        }
    }
}