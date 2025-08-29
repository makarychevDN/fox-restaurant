using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class ItemSlot : MonoBehaviour
    {
        [SerializeField] private Image hoveredItemRenderer;
        [SerializeField] private GameObject boxOnHoverRenderer;
        [SerializeField] private Transform centerForItem;
        [SerializeField] private SlotType slotType;
        [SerializeField] private Cooker cooker;
        private Item item;
        private Level level;

        public UnityEvent OnHasBeenOccupied;
        public UnityEvent<Item> OnItemHasBeenPlaced;
        public UnityEvent<Item> OnItemHasBeenRemoved;
        public UnityEvent OnItemHovered;

        public bool Available => item == null;
        public SlotType SlotType => slotType;
        public Transform CenterForItem => centerForItem;
        public Item Item => item;

        public void Init(Level level)
        {
            this.level = level;
            level.SlotsManager.AddSlot(this);

            if (centerForItem != null)
                cooker.Init(this);

            if (centerForItem == null)
                centerForItem = transform;
        }

        public void SetItem(Item item)
        {
            var oldItem = this.item;
            this.item = item;

            if (item == null)
            {
                OnItemHasBeenRemoved.Invoke(oldItem);
                return;
            }

            item.transform.parent = centerForItem;
            item.transform.localPosition = Vector3.zero;
            OnHasBeenOccupied.Invoke();
            OnItemHasBeenPlaced.Invoke(item);
        }

        public void Clear()
        {
            SetItem(null);
            Destroy(item.gameObject);
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
}