using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsSpawner : MonoBehaviour
{
    [SerializeField] private ItemSlot deskSlotPrefab;
    [SerializeField] private Item itemPrefab;
    [SerializeField] private Transform slotsParent;
    [SerializeField] private List<ItemSlot> slots;
    private Level level;

    public void Init(Level level)
    {
        this.level = level;
    }

    public ItemSlot ActivateItemSlot()
    {
        ItemSlot slot = slots.FirstOrDefault(slot => !slot.gameObject.activeSelf || slot.Available);

        if (slot == null)
        {
            slot = Instantiate(deskSlotPrefab);
            slots.Add(slot);
            slot.Init(level);
        }

        slot.transform.parent = slotsParent;
        slot.transform.localScale = Vector3.one;
        slot.gameObject.SetActive(true);
        return slot;
    }

    public void SpawnIngredient()
    {
        Item item = Instantiate(itemPrefab);
        item.Init(level);
        var slot = ActivateItemSlot();
        item.transform.parent = slot.transform;
        item.transform.localPosition = Vector3.zero;
        item.transform.localScale = Vector3.one;
        slot.SetItem(item);
        item.SetSlot(slot);
    }
}
