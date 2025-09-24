using UnityEngine;

namespace foxRestaurant
{
    public class SlotSpawner : MonoBehaviour
    {
        [SerializeField] private ItemSlot cookerSlotPrefab;
        [SerializeField] private Transform slotsParent;
        private RestaurantEncounter restaurantEncounter;

        public void Init(RestaurantEncounter restaurantEncounter)
        {
            this.restaurantEncounter = restaurantEncounter;
        }

        public ItemSlot SpawnSlot()
        {
            var slot = Instantiate(cookerSlotPrefab);

            slot.Init(restaurantEncounter);

            slot.transform.parent = slotsParent;
            slot.transform.localScale = Vector3.one;
            slot.gameObject.SetActive(true);

            return slot;
        }

    }
}