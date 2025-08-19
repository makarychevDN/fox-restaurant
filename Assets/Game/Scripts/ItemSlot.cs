using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Item item;
    private Level level;

    public void Activate() => level.SlotsManager.AddSlot(this);
    public void Deactivate() => level.SlotsManager.RemoveSlot(this);

    public void SetItem(Item item)
    {
        this.item = item;

        if (item == null)
            return;

        item.transform.parent = transform;
        item.transform.localPosition = Vector3.zero;
    }

    public void Init(Level level)
    {
        this.level = level;
    }
}
