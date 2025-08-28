using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image hoveredItemRenderer;
    [SerializeField] private GameObject boxOnHoverRenderer;
    [SerializeField] private Transform centerForItem;
    [SerializeField] private SlotType slotType;
    private Item item;
    private Level level;

    public UnityEvent OnHasBeenOccupied;
    public UnityEvent<Item> OnItemHasBeenPlaced;

    public bool Available => item == null;
    public SlotType SlotType => slotType;
    public Transform CenterForItem => centerForItem;
    //public void Activate() => level.SlotsManager.AddSlot(this);
    //public void Deactivate() => level.SlotsManager.RemoveSlot(this);

    public void Init(Level level)
    {
        this.level = level;
        level.SlotsManager.AddSlot(this);

        if (centerForItem == null)
            centerForItem = transform;
    }

    public void SetItem(Item item)
    {
        this.item = item;

        if (item == null)
            return;

        item.transform.parent = centerForItem;
        item.transform.localPosition = Vector3.zero;
        OnHasBeenOccupied.Invoke();
        OnItemHasBeenPlaced.Invoke(item);
    }

    public void Clear()
    {
        Destroy(item.gameObject);
        SetItem(null);
    }

    public void Hover(Item item)
    {
        hoveredItemRenderer.enabled = true;
        if (!boxOnHoverRenderer.activeInHierarchy)
            boxOnHoverRenderer.SetActive(true);
        hoveredItemRenderer.sprite = item.Image.sprite;
        hoveredItemRenderer.rectTransform.sizeDelta = item.Image.rectTransform.sizeDelta;
    }

    public void Unhover()
    {
        hoveredItemRenderer.enabled = false;
        boxOnHoverRenderer.SetActive(false);
    }
}

public enum SlotType
{
    Customer = 10,
    Cooker = 20
}
