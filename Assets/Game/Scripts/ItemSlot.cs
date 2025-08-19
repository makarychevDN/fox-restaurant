using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image hoveredItemRenderer;
    [SerializeField] private GameObject boxOnHoverRenderer;
    private Item item;
    private Level level;

    public void Activate() => level.SlotsManager.AddSlot(this);
    public void Deactivate() => level.SlotsManager.RemoveSlot(this);
    public bool Available => item == null;

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

    public void Hover(Item item)
    {
        hoveredItemRenderer.enabled = true;
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
