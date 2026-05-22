using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace foxRestaurant
{
    public class SlotsManager : MonoBehaviour
    {
        private List<ItemSlot> slots = new();
        private List<ItemSlot> bottomRowSlots = new();
        private List<ItemSlot> holderSlots = new();
        private List<ItemSlot> spawnerSlots = new();
        private List<ItemSlot> customerSlots = new();
        private List<ItemSlot> garbageCanSlots = new();
        private Dictionary<SlotType, List<ItemSlot>> slotLists;

        public List<ItemSlot> BottomRowSlots => bottomRowSlots;
        public List<ItemSlot> HolderSlots => holderSlots;
        public List<ItemSlot> SpawnerSlots => spawnerSlots;
        public Dictionary<SlotType, List<ItemSlot>> SlotLists => slotLists;

        public void Init()
        {
            slotLists = new Dictionary<SlotType, List<ItemSlot>>()
            {
                { SlotType.BottomRow, bottomRowSlots },
                { SlotType.Holder, holderSlots },
                { SlotType.Spawner, spawnerSlots },
                { SlotType.CustomerSlot, customerSlots },
                { SlotType.GarbageCan, garbageCanSlots }
            };
        }

        public void AddSlot(ItemSlot slot)
        {
            slots.Add(slot);
            slotLists[slot.SlotType].Add(slot);
        }

        public void RemoveSlot(ItemSlot slot)
        {
            slots.Remove(slot);
            slotLists[slot.SlotType].Remove(slot);
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