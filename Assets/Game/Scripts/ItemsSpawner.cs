using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace foxRestaurant
{
    public class ItemsSpawner : MonoBehaviour
    {
        [SerializeField] private Item itemPrefab;
        [SerializeField] private List<ItemData> availvableItemData;
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

            Item item = Instantiate(itemPrefab);
            item.transform.parent = cookerSlot.transform;
            item.transform.localPosition = Vector3.zero;
            item.transform.localScale = Vector3.one;
            cookerSlot.SetItem(item);
            item.SetSlot(cookerSlot);
            item.Init(level, availvableItemData.GetRandomElement());
        }
    }
}