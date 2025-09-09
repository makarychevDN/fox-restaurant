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

            SpawnItem(level, level.DecksManager.GetRandomIngredient(), cookerSlot);
        }

        public void SpawnItem(Level level, ItemData itemData, ItemSlot itemSlot)
        {
            Item item = Instantiate(itemPrefab);

            itemSlot.SetItem(item);
            item.SetSlot(itemSlot);
            item.Init(level, itemData);

            item.transform.parent = itemSlot.transform;
            item.transform.position = itemSlot.CenterForItem.position;
            item.transform.localScale = Vector3.one;
        }
    }
}