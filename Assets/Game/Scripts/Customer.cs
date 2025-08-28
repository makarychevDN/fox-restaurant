using UnityEngine;

namespace foxRestaurant
{
    public class Customer : MonoBehaviour
    {
        [SerializeField] private ItemSlot slotToPlaceFood;
        [SerializeField] private ItemSlot itemSlotPrefab;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private AudioSource eatingSound;
        [SerializeField] private int hungerPoints;
        [SerializeField] private int patience;
        private Transform customerSlotsParent;
        private Item requiredItem;
        private Level level;

        public void Init(Level level, Transform customerSlotsParent)
        {
            this.level = level;
            this.customerSlotsParent = customerSlotsParent;
            slotToPlaceFood = SpawnSlot();
            slotToPlaceFood.OnItemHasBeenPlaced.AddListener(TryToEat);
            slotToPlaceFood.OnHasBeenOccupied.AddListener(slotToPlaceFood.Clear);
        }

        private ItemSlot SpawnSlot()
        {
            var slot = Instantiate(itemSlotPrefab);
            slot.Init(level);
            slot.transform.parent = customerSlotsParent;
            slot.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            return slot;
        }

        public void SetCustomerData(CustomerData customerData)
        {
            hungerPoints = customerData.HungerPoints;
            patience = customerData.Patience;
            spriteRenderer.sprite = customerData.Sprite;
            MakeOrder();
        }

        public void TryToEat(Item item)
        {
            eatingSound.Play();
        }

        public void MakeOrder()
        {

        }
    }
}