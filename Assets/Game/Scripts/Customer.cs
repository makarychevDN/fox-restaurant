using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class Customer : MonoBehaviour, ITickable
    {
        [Header("stats")]
        [field: SerializeField] public int HungerPoints { get; private set; }
        [field: SerializeField] public float Patience { get; private set; }

        [Header("assets links")]
        [SerializeField] private ItemSlot itemSlotPrefab;

        [Header("links")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private AudioSource eatingSound;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform slotPositionPoint;
        [SerializeField] private Transform uiStatsRoot;

        [Header("ui links")]
        [SerializeField] private Image orderImage;
        [SerializeField] private CustomersUI customersUI;

        private ItemData requiredItemData;
        private ItemSlot slotToPlaceFood;
        private Func<ItemData> getItemDataToOrderFunc;
        private Level level;

        public UnityEvent<float> OnPatienceChanged;
        public UnityEvent<int> OnHungerPointsChanged;
        public UnityEvent OnEaten;

        public void Init(Level level, CustomerData customerData, Func<ItemData> getItemDataToOrderFunc)
        {
            this.getItemDataToOrderFunc = getItemDataToOrderFunc;
            level.Ticker.AddTickable(this);
            this.level = level;

            slotToPlaceFood = level.CustomerlSlotSpawner.SpawnSlot();
            slotToPlaceFood.transform.position = Camera.main.WorldToScreenPoint(slotPositionPoint.position);
            slotToPlaceFood.OnItemHasBeenPlaced.AddListener(TryToEat);
            slotToPlaceFood.OnHasBeenOccupied.AddListener(slotToPlaceFood.Clear);

            SetCustomerData(customerData);
            MakeOrder();
            customersUI.Init(this);

            orderImage.transform.rotation = Quaternion.identity;
            uiStatsRoot.rotation = Quaternion.identity;
        }

        public void SetCustomerData(CustomerData customerData)
        {
            HungerPoints = customerData.HungerPoints;
            Patience = customerData.Patience;
            spriteRenderer.sprite = customerData.Sprite;
        }

        private void SetOrderData(ItemData itemData)
        {
            orderImage.sprite = itemData.Sprite;
            requiredItemData = itemData;
            orderImage.rectTransform.sizeDelta = itemData.Sprite.GetSpriteSizeInPixels() * 0.5f;
        }

        public void TryToEat(Item item)
        {
            OnEaten.Invoke();

            if (item.ItemData == requiredItemData)
            {
                HungerPoints -= item.Satiety;
                OnHungerPointsChanged.Invoke(HungerPoints);
            }
            else
            {
                Patience += item.Satiety;
                OnPatienceChanged.Invoke(Patience);
            }

            eatingSound.Play();
            animator.SetTrigger("onEat");

            if (IsSatisfied)
                DestroyCustomer();
            else
                MakeOrder();
        }

        public void MakeOrder()
        {
            SetOrderData(getItemDataToOrderFunc());
            slotToPlaceFood.SetRequiredItemData(requiredItemData);
        }

        public void Tick(float deltaTime)
        {
            Patience -= deltaTime;
            OnPatienceChanged.Invoke(Patience);
        }

        private bool IsSatisfied => HungerPoints <= 0;

        public void DestroyCustomer()
        {
            level.Ticker.RemoveTickable(this);

            slotToPlaceFood.OnItemHasBeenPlaced.RemoveListener(TryToEat);
            slotToPlaceFood.OnHasBeenOccupied.RemoveListener(slotToPlaceFood.Clear);
            Destroy(slotToPlaceFood.gameObject);

            Destroy(gameObject);
        }
    }
}