using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField] private Image orderImage;
        private Item requiredItem;

        public void Init(Level level, CustomerData customerData)
        {
            slotToPlaceFood = SpawnSlot(level);
            slotToPlaceFood.OnItemHasBeenPlaced.AddListener(TryToEat);
            slotToPlaceFood.OnHasBeenOccupied.AddListener(slotToPlaceFood.Clear);
            SetCustomerData(customerData);            
            orderImage.transform.rotation = Quaternion.identity;
        }

        private ItemSlot SpawnSlot(Level level)
        {
            var slot = Instantiate(itemSlotPrefab);
            slot.Init(level);
            slot.transform.parent = level.CustomerSlotsParent;
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