using Unity.VisualScripting;
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
        private ItemData requiredItemData;
        private Level level;

        public UnityEvent OnHasBeenOccupied;
        public UnityEvent<Item> OnItemHasBeenPlaced;
        public UnityEvent<Item> OnItemHasBeenRemoved;
        public UnityEvent OnItemHovered;
        public SlotType SlotType => slotType;
        public Transform CenterForItem => centerForItem;
        public Item Item => item;
        public bool Empty => item == null;

        public void Init(Level level)
        {
            this.level = level;
            level.SlotsManager.AddSlot(this);

            if (cooker != null)
                cooker.Init(this, level);

            if (centerForItem == null)
                centerForItem = transform;
        }

        public void SetItem(Item item)
        {
            var oldItem = this.item;
            this.item = item;

            if (oldItem != null && item != null)
            {
                this.item = null;
                TryToFuseItems(oldItem, item);
                return;
            }

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

        private void TryToFuseItems(Item item1, Item item2)
        {
            Destroy(item1.gameObject);
            Destroy(item2.gameObject);

            level.ItemsSpawner.SpawnItem(
                level, 
                level.RecipesManager.Fuse(item1.ItemData, item2.ItemData), 
                this
            );
        }

        public void SetRequiredItemData(ItemData requiredItemData)
        {
            this.requiredItemData = requiredItemData;
        }

        public void Clear()
        {
            var oldItem = item;
            SetItem(null);
            Destroy(oldItem.gameObject);
        }

        public void Hover(Item item)
        {
            hoveredItemRenderer.enabled = true;
            if (!boxOnHoverRenderer.activeInHierarchy)
                boxOnHoverRenderer.SetActive(true);
            hoveredItemRenderer.sprite = item.Image.sprite;
            hoveredItemRenderer.rectTransform.sizeDelta = item.Image.rectTransform.sizeDelta;
        }

        public bool AvailableToPlaceItem(Item placingItem)
        {
            //return Empty && (requiredItemData == null || placingItem.ItemData == requiredItemData);
            return true;
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
        Cooker = 20,
        Additional = 30
    }
}