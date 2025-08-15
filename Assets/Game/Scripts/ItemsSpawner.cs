using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsSpawner : MonoBehaviour
{
    [SerializeField] private ItemSlot deskSlotPrefab;
    [SerializeField] private Transform slotsParent;
    [SerializeField] private List<ItemSlot> slots;

    public void ActivateNewSlot()
    {
        ItemSlot slotToPlaceItem = slots.FirstOrDefault(slot => !slot.gameObject.activeSelf);

        if (slotToPlaceItem == null)
        {
            slotToPlaceItem = Instantiate(deskSlotPrefab);
            slots.Add(slotToPlaceItem);
        }

        slotToPlaceItem.transform.parent = slotsParent;
        slotToPlaceItem.gameObject.SetActive(true);

    }
}
