using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Item item;
    private Level level;

    public void Activate() => level.AddSlot(this);
    public void Deactivate() => level.RemoveSlot(this);

    public void SetItem(Item item)
    {
        this.item = item;
        item.transform.parent = transform;
        item.transform.localPosition = Vector3.zero;
    }

    public void Init(Level level)
    {
        this.level = level;
    }
}
