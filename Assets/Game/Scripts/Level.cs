using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private ItemsSpawner itemsSpawner;
    [SerializeField] private PlayerInputController playerInputController;
    [SerializeField] private List<ItemSlot> slots;

    public ItemsSpawner ItemsSpawner => itemsSpawner;
    public void AddSlot(ItemSlot slot) => slots.Add(slot);
    public void RemoveSlot(ItemSlot slot) => slots.Remove(slot);
    public List<ItemSlot> Slots => slots;

    private void Awake()
    {
        playerInputController.Init(this);
        itemsSpawner.Init(this);
    }

    public ItemSlot GetClosestAvailableSlot(Item item)
    {
        slots.Sort((a, b) =>
            (a.transform.position - item.transform.position).sqrMagnitude.CompareTo(
            (b.transform.position - item.transform.position).sqrMagnitude));

        if (slots.Count == 0)
            return null;

        if (slots.Count == 1)
            return item.Level.Slots[0];

        var distanceToClosestSlot = Vector3.Distance(slots[0].transform.position, item.transform.position);
        var distanceToSecondClosestSlot = Vector3.Distance(slots[1].transform.position, item.transform.position);
        if (distanceToSecondClosestSlot / distanceToClosestSlot > 2)
        {
            return slots[0];
        }

        return null;
    }
}
