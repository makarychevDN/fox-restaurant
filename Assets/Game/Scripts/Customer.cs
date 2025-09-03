using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class Customer : MonoBehaviour, ITickable
    {
        [Header("stats")]
        [SerializeField] private int hungerPoints;
        [SerializeField] private float patience;
        [Header("assets links")]
        [SerializeField] private ItemSlot itemSlotPrefab;
        [Header("prefab links")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private AudioSource eatingSound;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform slotPositionPoint;
        [SerializeField] private Transform uiStatsRoot;
        [Header("ui links")]
        [SerializeField] private Image orderImage;
        [SerializeField] private TMP_Text patienceIndicator;

        private ItemData requiredItemData;
        private ItemSlot slotToPlaceFood;
        private Func<ItemData> getItemDataToOrderFunc;

        public void Init(Level level, CustomerData customerData, Func<ItemData> getItemDataToOrderFunc)
        {
            this.getItemDataToOrderFunc = getItemDataToOrderFunc;
            level.Ticker.AddTickable(this);

            slotToPlaceFood = SpawnSlot(level);
            slotToPlaceFood.OnItemHasBeenPlaced.AddListener(TryToEat);
            slotToPlaceFood.OnHasBeenOccupied.AddListener(slotToPlaceFood.Clear);

            SetCustomerData(customerData);
            MakeOrder();

            orderImage.transform.rotation = Quaternion.identity;
            uiStatsRoot.rotation = Quaternion.identity;
        }

        private ItemSlot SpawnSlot(Level level)
        {
            var slot = Instantiate(itemSlotPrefab);
            slot.Init(level);
            slot.transform.parent = level.CustomerSlotsParent;
            slot.transform.position = Camera.main.WorldToScreenPoint(slotPositionPoint.position);
            return slot;
        }

        public void SetCustomerData(CustomerData customerData)
        {
            hungerPoints = customerData.HungerPoints;
            patience = customerData.Patience;
            spriteRenderer.sprite = customerData.Sprite;
        }

        private void SetOrderData(ItemData itemData)
        {
            orderImage.sprite = itemData.Sprite;
            requiredItemData = itemData;
        }

        public void TryToEat(Item item)
        {
            animator.SetTrigger("onEat");
            eatingSound.Play();
        }

        public void MakeOrder()
        {
            SetOrderData(getItemDataToOrderFunc());
            slotToPlaceFood.SetRequiredItemData(requiredItemData);
        }

        public void Tick(float deltaTime)
        {
            patience -= deltaTime;
            patienceIndicator.text = patience.ToString("0");
        }
    }
}