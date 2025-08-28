using UnityEngine;

namespace foxRestaurant
{
    public class CookerSlotSpawner : MonoBehaviour
    {
        [SerializeField] private ItemSlot cookerSlotPrefab;
        [SerializeField] private Transform slotsParent;
        private Level level;

        public void Init(Level level)
        {
            this.level = level;
        }

        public ItemSlot SpawnCookerSlot()
        {
            var slot = Instantiate(cookerSlotPrefab);

            slot.Init(level);

            slot.transform.parent = slotsParent;
            slot.transform.localScale = Vector3.one;
            slot.gameObject.SetActive(true);

            return slot;
        }

    }
}