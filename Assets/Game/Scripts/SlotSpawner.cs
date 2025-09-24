using UnityEngine;

namespace foxRestaurant
{
    public class SlotSpawner : MonoBehaviour
    {
        [SerializeField] private ItemSlot cookerSlotPrefab;
        [SerializeField] private Transform slotsParent;
        private RestaurantEncounter level;

        public void Init(RestaurantEncounter level)
        {
            this.level = level;
        }

        public ItemSlot SpawnSlot()
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