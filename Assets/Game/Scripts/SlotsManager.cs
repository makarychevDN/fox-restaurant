using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace foxRestaurant
{
    public class SlotsManager : MonoBehaviour
    {
        [SerializeField] private List<ItemSlot> slots;

        public void AddSlot(ItemSlot slot) => slots.Add(slot);
        public void RemoveSlot(ItemSlot slot) => slots.Remove(slot);
        public List<ItemSlot> CookerSlots => slots.Where(slot => slot.SlotType == SlotType.Cooker).ToList();

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

            if (abailableSlots.Count == 1)
                return abailableSlots[0];

            var distanceToClosestSlot = Vector3.Distance(abailableSlots[0].transform.position, item.transform.position);
            var distanceToSecondClosestSlot = Vector3.Distance(abailableSlots[1].transform.position, item.transform.position);
            if (distanceToSecondClosestSlot / distanceToClosestSlot > 2)
            {
                return abailableSlots[0];
            }

            return null;
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