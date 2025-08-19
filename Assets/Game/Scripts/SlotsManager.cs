using System.Collections.Generic;
using UnityEngine;

public class SlotsManager : MonoBehaviour
{
    [SerializeField] private List<ItemSlot> slots;

    public void AddSlot(ItemSlot slot) => slots.Add(slot);
    public void RemoveSlot(ItemSlot slot) => slots.Remove(slot);

    public ItemSlot GetClosestAvailableSlot(Item item)
    {
        slots.Sort((a, b) =>
            (a.transform.position - item.transform.position).sqrMagnitude.CompareTo(
            (b.transform.position - item.transform.position).sqrMagnitude));

        if (slots.Count == 0)
            return null;

        if (slots.Count == 1)
            return slots[0];

        var distanceToClosestSlot = Vector3.Distance(slots[0].transform.position, item.transform.position);
        var distanceToSecondClosestSlot = Vector3.Distance(slots[1].transform.position, item.transform.position);
        if (distanceToSecondClosestSlot / distanceToClosestSlot > 2)
        {
            return slots[0];
        }

        return null;
    }
}
