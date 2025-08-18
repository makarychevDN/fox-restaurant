using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsSpawner : MonoBehaviour
{
    [SerializeField] private ItemSlot deskSlotPrefab;
    [SerializeField] private Item itemPrefab;
    [SerializeField] private Transform slotsParent;
    [SerializeField] private List<ItemSlot> slots;

    public ItemSlot ActivateItemSlot()
    {
        ItemSlot slotToPlaceItem = slots.FirstOrDefault(slot => !slot.gameObject.activeSelf);

        if (slotToPlaceItem == null)
        {
            slotToPlaceItem = Instantiate(deskSlotPrefab);
            slots.Add(slotToPlaceItem);
        }

        slotToPlaceItem.transform.parent = slotsParent;
        slotToPlaceItem.gameObject.SetActive(true);
        return slotToPlaceItem;
    }

    public void SpawnIngredient()
    {
        Item item = Instantiate(itemPrefab);
        var slot = ActivateItemSlot();
        item.transform.parent = slot.transform;
        item.transform.localPosition = Vector3.zero;
        slot.SetItem(item);
        item.SetSlot(slot);
    }
}
