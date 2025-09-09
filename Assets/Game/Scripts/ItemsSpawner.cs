using System.Linq;
using UnityEngine;

namespace foxRestaurant
{
    public class ItemsSpawner : MonoBehaviour
    {
        [SerializeField] private Item itemPrefab;
        private Level level;

        public void Init(Level level)
        {
            this.level = level;
        }

        public void SpawnIngredient()
        {
            var cookerSlot = level.SlotsManager.CookerSlots.FirstOrDefault(slot => !slot.gameObject.activeSelf || slot.Empty);
            if (cookerSlot == null)
                return;

            var itemData = level.DecksManager.GetRandomIngredient();
            SpawnItem(level, itemData, cookerSlot, 1 + itemData.AdditionalSatiety);
        }

        public void SpawnItem(Level level, ItemData itemData, ItemSlot itemSlot, int satiety)
        {
            Item item = Instantiate(itemPrefab);

            itemSlot.SetItem(item);
            item.Slot = itemSlot;
            item.Init(level, itemData, satiety);

            item.transform.parent = itemSlot.transform;
            item.transform.position = itemSlot.CenterForItem.position;
            item.transform.localScale = Vector3.one;
        }
    }
}