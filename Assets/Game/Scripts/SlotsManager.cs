using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace foxRestaurant
{
    public class SlotsManager : MonoBehaviour
    {
        [SerializeField] private List<ItemSlot> slots;

        private List<ItemSlot> foodSpawnerSlots = new();
        private List<ItemSlot> customerSpawnerSlots = new();
        public List<ItemSlot> FoodSpawningSlots => foodSpawnerSlots;

        public void AddSlot(ItemSlot slot)
        {
            slots.Add(slot);

            if(slot.SlotType == SlotType.Spawner)
            {
                if(slot.RequiredItemsType == ItemType.Food)
                    foodSpawnerSlots.Add(slot);
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
                    foodSpawnerSlots.Remove(slot);
                else
                    customerSpawnerSlots.Remove(slot);
            }
        }

        public ItemSlot GetSlotToPlaceItem(Item item)
        {
            ItemSlot slot = GetClosestAvailableSlot(item);

            if (slot == null)
                slot = item.Slot;

            return slot;
        }

        private ItemSlot GetClosestAvailableSlot(Item item)
        {
            var abailableSlots = slots.Where(slot => slot.AvailableToPlaceItem(item)).ToList();
            abailableSlots.Sort((a, b) =>
                (a.transform.position - item.transform.position).sqrMagnitude.CompareTo(
                (b.transform.position - item.transform.position).sqrMagnitude));

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