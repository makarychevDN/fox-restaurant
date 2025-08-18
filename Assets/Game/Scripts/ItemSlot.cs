using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Item item;

    public void SetItem(Item item)
    {
        this.item = item;
        item.transform.parent = transform;
        item.transform.localPosition = Vector3.zero;
    }
}
