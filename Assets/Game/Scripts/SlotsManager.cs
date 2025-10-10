using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace foxRestaurant
{
    public class SlotsManager : MonoBehaviour
    {
        [SerializeField] private List<ItemSlot> slots;

        private List<ItemSlot> itemSpawnerSlots = new();
        private List<ItemSlot> customerSpawnerSlots = new();
        public List<ItemSlot> SpawningSlots => itemSpawnerSlots;

        public void AddSlot(ItemSlot slot)
        {
            slots.Add(slot);

            if(slot.SlotType == SlotType.Spawner)
            {
                if(slot.RequiredItemsType == ItemType.Food)
                    itemSpawnerSlots.Add(slot);
                else
                    customerSpawnerSlots.Add(slot);
            }
        }

        public void RemoveSlot(ItemSlot slot)
        {
            slots.Remove(slot);

            if (slot.SlotType == SlotType.Spawner)
            {
                if (slot.RequiredItemsType == ItemType.Food)
                    itemSpawnerSlots.Remove(slot);
                else
                    customerSpawnerSlots.Remove(slot);
            }
        }

        public ItemSlot GetSlotToPlaceItem(FoodItemExtension foodItemExtension)
        {
            ItemSlot slot = GetClosestAvailableSlot(foodItemExtension);

            if (slot == null)
                slot = foodItemExtension.Slot;

            return slot;
        }

        private ItemSlot GetClosestAvailableSlot(FoodItemExtension foodItemExtension)
        {
            var abailableSlots = slots.Where(slot => slot.AvailableToPlaceItem(foodItemExtension)).ToList();
            abailableSlots.Sort((a, b) =>
                (a.transform.position - foodItemExtension.transform.position).sqrMagnitude.CompareTo(
                (b.transform.position - foodItemExtension.transform.position).sqrMagnitude));

            if (abailableSlots.Count == 0)
                return null;

            return abailableSlots[0];
        }

        public void UnhoverAllSlots()
        {
            slots.ForEach(slot => slot.Unhover());
        }

        public void UnhoverAllSlotsExcept(ItemSlot exceptionSlot)
        {
            slots.ForEach(slot => { if (exceptionSlot != slot) slot.Unhover(); });
        }
    }
}