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
        [SerializeField] private ItemType requiredItemsType;
        [SerializeField] private Cooker cooker;
        [SerializeField] private AudioSource onItemPlacedSound;
        private FoodItemExtension foodItemExtension;
        private ItemData requiredItemData;
        private RestaurantEncounter restaurantEncounter;
        private bool selectedForItemMovement;

        public UnityEvent OnHasBeenOccupied;
        public UnityEvent<FoodItemExtension> OnItemHasBeenPlaced;
        public UnityEvent<FoodItemExtension> OnItemHasBeenRemoved;
        public UnityEvent OnItemHovered;
        public UnityEvent OnItemUnhovered;
        public UnityEvent OnItemSliced;

        public SlotType SlotType => slotType;
        public ItemType RequiredItemsType => requiredItemsType;
        public Transform CenterForItem => centerForItem;
        public FoodItemExtension FoodItemExtension => foodItemExtension;
        public bool Empty => foodItemExtension == null;

        public void Init(RestaurantEncounter restaurantEncounter)
        {
            this.restaurantEncounter = restaurantEncounter;
            restaurantEncounter.SlotsManager.AddSlot(this);

            if (cooker != null)
                cooker.Init(this, restaurantEncounter);

            if (centerForItem == null)
                centerForItem = transform;
        }

        public void SetItem(FoodItemExtension foodItemExtension)
        {
            var oldItem = this.foodItemExtension;
            this.foodItemExtension = foodItemExtension;

            if (oldItem != null && foodItemExtension != null)
            {
                this.foodItemExtension = null;
                TryToFuseItems(oldItem, foodItemExtension);
                return;
            }

            if (foodItemExtension == null)
            {
                OnItemHasBeenRemoved.Invoke(oldItem);
                return;
            }

            foodItemExtension.transform.parent = centerForItem;
            foodItemExtension.transform.localPosition = Vector3.zero;
            OnHasBeenOccupied.Invoke();
            OnItemHasBeenPlaced.Invoke(foodItemExtension);
            onItemPlacedSound.Play();
        }

        public void SetSelectedForItemMovement(bool value) => selectedForItemMovement = value;

        private void TryToFuseItems(FoodItemExtension food1, FoodItemExtension food2)
        {
            restaurantEncounter.ItemsSpawner.SpawnItem(
                restaurantEncounter, 
                restaurantEncounter.RecipesManager.Fuse(food1.ItemData, food2.ItemData), 
                this,
                food1.Satiety + food2.Satiety
            );

            Destroy(food1.gameObject);
            Destroy(food2.gameObject);
        }

        public void SetRequiredItemData(ItemData requiredItemData)
        {
            this.requiredItemData = requiredItemData;
        }

        public void Clear()
        {
            var oldItem = foodItemExtension;
            SetItem(null);
            Destroy(oldItem.gameObject);
        }

        public void Hover(FoodItemExtension foodItemExtension)
        {
            hoveredItemRenderer.enabled = true;
            if (!boxOnHoverRenderer.activeInHierarchy)
                boxOnHoverRenderer.SetActive(true);
            hoveredItemRenderer.sprite = foodItemExtension.Image.sprite;
            hoveredItemRenderer.rectTransform.sizeDelta = foodItemExtension.Image.rectTransform.sizeDelta;
        }

        public bool AvailableToPlaceItem(FoodItemExtension placingFood)
        {
            if (selectedForItemMovement)
                return false;

            return placingFood.ItemType == requiredItemsType;
        }

        public void Unhover()
        {
            hoveredItemRenderer.enabled = false;
            boxOnHoverRenderer.SetActive(false);
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