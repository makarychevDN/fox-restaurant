using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class ItemSlot : MonoBehaviour
    {
        [SerializeField] private Image hoveredItemRenderer;
        [SerializeField] private GameObject boxOnHoverRenderer;
        [SerializeField] private GameObject redCross;
        [SerializeField] private Transform centerForItem;
        [SerializeField] private SlotType slotType;
        [SerializeField] private ItemType requiredItemsType;
        [SerializeField] private Cooker cooker;
        [SerializeField] private AudioSource onItemPlacedSound;
        private Item item;
        private ItemData requiredItemData;
        private RestaurantEncounter restaurantEncounter;
        private bool selectedForItemMovement;
        private bool preDestroyed;
        private bool blocked;

        public UnityEvent OnHasBeenOccupied;
        public UnityEvent<Item> OnItemHasBeenPlaced;
        public UnityEvent<Item> OnItemHasBeenRemoved;
        public UnityEvent OnItemHovered;
        public UnityEvent OnItemUnhovered;
        public UnityEvent OnItemSliced;

        public SlotType SlotType => slotType;
        public ItemType RequiredItemsType => requiredItemsType;
        public Transform CenterForItem => centerForItem;
        public Item Item => item;
        public bool Empty => item == null;

        public void Init(RestaurantEncounter restaurantEncounter)
        {
            this.restaurantEncounter = restaurantEncounter;
            restaurantEncounter.SlotsManager.AddSlot(this);

            if (cooker != null)
                cooker.Init(this, restaurantEncounter);

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
                TryToFuseItems((FoodItem)oldItem, (FoodItem)item);
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
            onItemPlacedSound.Play();
        }

        public void SetSelectedForItemMovement(bool value) => selectedForItemMovement = value;

        private void TryToFuseItems(FoodItem item1, FoodItem item2)
        {
            var fusion = restaurantEncounter.ItemsSpawner.SpawnFoodItem(
                restaurantEncounter, 
                restaurantEncounter.RecipesManager.Fuse(item1.ItemData, item2.ItemData), 
                this
            );

            ((FoodItem)fusion).SetSatiety(item1.Satiety + item2.Satiety);

            Destroy(item1.gameObject);
            Destroy(item2.gameObject);
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
            if (blocked || selectedForItemMovement || preDestroyed)
                return false;

            return placingItem.ItemType == requiredItemsType && gameObject.activeInHierarchy;
        }

        public void Unhover()
        {
            hoveredItemRenderer.enabled = false;
            boxOnHoverRenderer.SetActive(false);
        }

        public void PreDestroy()
        {
            preDestroyed = true;
        }

        public void SetBlockedValue(bool blockedValue)
        {
            blocked = blockedValue;
            redCross.SetActive(blockedValue);
        }

        private void OnDestroy()
        {
            restaurantEncounter.SlotsManager.RemoveSlot(this);
        }
    }

    public enum SlotType
    {
        Generic = 10,
        Spawner = 20
    }
}