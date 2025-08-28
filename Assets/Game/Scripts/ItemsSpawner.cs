using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsSpawner : MonoBehaviour
{
    [SerializeField] private Item itemPrefab;
    private Level level;

    public void Init(Level level)
    {
        this.level = level;
    }

    public void SpawnIngredient()
    {
        var cookerSlot = level.SlotsManager.CookerSlots.FirstOrDefault(slot => !slot.gameObject.activeSelf || slot.Available);
        if (cookerSlot == null)
            return;

        Item item = Instantiate(itemPrefab);
        item.Init(level);
        item.transform.parent = cookerSlot.transform;
        item.transform.localPosition = Vector3.zero;
        item.transform.localScale = Vector3.one;
        cookerSlot.SetItem(item);
        item.SetSlot(cookerSlot);
    }
}
