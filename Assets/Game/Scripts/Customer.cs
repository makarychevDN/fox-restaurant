using System;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private Animator orderBoxAnimator;
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
        private List<ITickable> activeEffects = new();

        public UnityEvent<float> OnPatienceChanged;
        public UnityEvent<int> OnHungerPointsChanged;
        /// <summary>
        /// It is called when the customer leaves.
        /// if satisfied, argument = true, else argument = false.
        /// </summary>

        public UnityEvent OnStartLivingProcess;
        public UnityEvent<bool> OnLeftSatisfied;
        public UnityEvent OnLeft;
        public UnityEvent OnAte;
        public UnityEvent<ItemData> OnAteCertainFood;

        public Character Character => character;

        public void Init(RestaurantEncounter restaurantEncounter, CustomerData customerData, Func<ItemData> getItemDataToOrderFunc)
        {
            slotToPlaceFood = restaurantEncounter.CustomerSlotsToPlaceFoodSpawner.SpawnSlot();
            slotToPlaceFood.transform.position = slotPositionPoint.position;
            slotToPlaceFood.OnItemHasBeenPlaced.AddListener(TryToEat);
            slotToPlaceFood.OnHasBeenOccupied.AddListener(slotToPlaceFood.Clear);

            this.getItemDataToOrderFunc = getItemDataToOrderFunc;
            restaurantEncounter.Ticker.AddTickable(this);
            restaurantEncounter.CustomersManager.AddCustomer(this);
            this.restaurantEncounter = restaurantEncounter;

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
                if(instance is ITickable) activeEffects.Add(instance as ITickable);
                instance.Apply(this, restaurantEncounter);

                if (effect is IAbleToReturnViewPrefab ableToReturnViewPrefab)
                {
                    var prefab = ableToReturnViewPrefab.GetViewPrefab();
                    var viewGO = Instantiate(prefab, uiStatsRoot);

                    if (viewGO.TryGetComponent<ICustomerEffectView>(out var view))
                    {
                        view.InitBase(instance);
                    }
                }
            }
        }

        public void SetOrderData(ItemData itemData)
        {
            orderImage.sprite = itemData.Sprite;
            requiredItemData = itemData;
            orderImage.rectTransform.sizeDelta = itemData.Sprite.GetSpriteSizeInPixels() * 0.5f;
            orderBoxAnimator.SetTrigger("order changed");
        }

        public void TryToEat(Item item)
        {
            var food = item as FoodItem;
            OnAte.Invoke();
            OnAteCertainFood.Invoke(item.ItemData);

            if (item.ItemData == requiredItemData)
            {
                HungerPoints -= food.Satiety;
                HungerPoints = Math.Clamp(HungerPoints, 0, 100);
                OnHungerPointsChanged.Invoke(HungerPoints);
                MakeOrder();
            }

            animator.SetTrigger("onEat");

            if (IsSatisfied)
            {
                Uninit();
                OnStartLivingProcess.Invoke();
                Invoke(nameof(LeaveSatisfied), 2f);
            }
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

            for(int i = 0; i < activeEffects.Count; i++)
            {
                activeEffects[i].Tick(deltaTime);
            }

            if(Patience <= 0)
            {
                timeIsUpSound.Play();
                animator.SetTrigger("onEat");
                OnStartLivingProcess.Invoke();
                Uninit();
                Invoke(nameof(LeaveUnsatisfied), 2f);
            }
        }

        private bool IsSatisfied => HungerPoints <= 0;

        public void Uninit()
        {
            restaurantEncounter.Ticker.RemoveTickable(this);
            restaurantEncounter.CustomersManager.RemoveCustomer(this);
            slotToPlaceFood.OnItemHasBeenPlaced.RemoveListener(TryToEat);
            slotToPlaceFood.OnHasBeenOccupied.RemoveListener(slotToPlaceFood.Clear);
            slotToPlaceFood.PreDestroy();

            orderBox.SetActive(false);
        }

        public void LeaveSatisfied()
        {
            OnLeft.Invoke();
            OnLeftSatisfied.Invoke(true);
            if (slotToPlaceFood != null)
                Destroy(slotToPlaceFood.gameObject);
            Destroy(gameObject);
        }

        public void LeaveUnsatisfied() //todo improve this thing
        {
            OnLeft.Invoke();
            OnLeftSatisfied.Invoke(false);
            if(slotToPlaceFood != null)
                Destroy(slotToPlaceFood.gameObject);
            Destroy(gameObject);
        }

        public void SetBlockedByTauntOnAnotherCustomer(bool value)
        {
            slotToPlaceFood.SetBlockedValue(value);
        }

        public bool HasTauntEffect => activeEffects.Any(effect => effect is TauntEffectInstance);
    }
}