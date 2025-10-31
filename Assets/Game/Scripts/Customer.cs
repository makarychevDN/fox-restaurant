using System;
using System.Collections.Generic;
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
        [SerializeField] private Character character;
        [SerializeField] private AudioSource timeIsUpSound;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform slotPositionPoint;
        [SerializeField] private Transform uiStatsRoot;

        [Header("ui links")]
        [SerializeField] private GameObject orderBox;
        [SerializeField] private Image orderImage;
        [SerializeField] private CustomersUI customersUI;

        private ItemData requiredItemData;
        private ItemSlot slotToPlaceFood;
        private Func<ItemData> getItemDataToOrderFunc;
        private RestaurantEncounter restaurantEncounter;
        private List<ICustomerEffectInstance> effects = new();

        public UnityEvent<float> OnPatienceChanged;
        public UnityEvent<int> OnHungerPointsChanged;
        /// <summary>
        /// It is called when the customer leaves.
        /// if satisfied, argument = true, else argument = false.
        /// </summary>
        public UnityEvent<bool> OnLeft;
        public UnityEvent OnAte;

        public Character Character => character;

        public void Init(RestaurantEncounter restaurantEncounter, CustomerData customerData, Func<ItemData> getItemDataToOrderFunc)
        {
            this.getItemDataToOrderFunc = getItemDataToOrderFunc;
            restaurantEncounter.Ticker.AddTickable(this);
            this.restaurantEncounter = restaurantEncounter;

            slotToPlaceFood = restaurantEncounter.CustomerSlotsToPlaceFoodSpawner.SpawnSlot();
            slotToPlaceFood.transform.position = slotPositionPoint.position;
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
            character.SetSprite(customerData.Sprite);

            foreach (ICustomerEffect effect in customerData.Effects)
            {
                var instance = effect.CreateInstance();
                effects.Add(instance);
                instance.Apply(this);                
            }
        }

        private void SetOrderData(ItemData itemData)
        {
            orderImage.sprite = itemData.Sprite;
            requiredItemData = itemData;
            orderImage.rectTransform.sizeDelta = itemData.Sprite.GetSpriteSizeInPixels() * 0.5f;
        }

        public void TryToEat(Item item)
        {
            var food = item as FoodItem;
            OnAte.Invoke();

            if (item.ItemData == requiredItemData)
            {
                HungerPoints -= food.Satiety;
                HungerPoints = Math.Clamp(HungerPoints, 0, 100);
                OnHungerPointsChanged.Invoke(HungerPoints);
            }
            else
            {
                Patience += food.Satiety;
                OnPatienceChanged.Invoke(Patience);
            }

            animator.SetTrigger("onEat");

            if (IsSatisfied)
            {
                Uninit();
                Invoke(nameof(LeaveSatisfied), 2f);
            }
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
            Patience = Math.Clamp(Patience, 0, 1000);
            OnPatienceChanged.Invoke(Patience);

            if(Patience <= 0)
            {
                timeIsUpSound.Play();
                animator.SetTrigger("onEat");
                Uninit();
                Invoke(nameof(LeaveUnsatisfied), 2f);
            }
        }

        private bool IsSatisfied => HungerPoints <= 0;

        public void Uninit()
        {
            restaurantEncounter.Ticker.RemoveTickable(this);
            slotToPlaceFood.OnItemHasBeenPlaced.RemoveListener(TryToEat);
            slotToPlaceFood.OnHasBeenOccupied.RemoveListener(slotToPlaceFood.Clear);
            slotToPlaceFood.PreDestroy();

            orderBox.SetActive(false);
        }

        public void LeaveSatisfied()
        {
            OnLeft.Invoke(true);
            Destroy(slotToPlaceFood.gameObject);
            Destroy(gameObject);
        }

        public void LeaveUnsatisfied()
        {
            OnLeft.Invoke(false);
            Destroy(slotToPlaceFood.gameObject);
            Destroy(gameObject);
        }
    }
}