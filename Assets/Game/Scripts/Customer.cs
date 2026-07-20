using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class Customer : MonoBehaviour, ITickable
    {
        [Header("stats")]
        [field: SerializeField] public int HungerPoints { get; private set; }
        [field: SerializeField] public int MaxHungerPoints { get; private set; }
        [field: SerializeField] public float Patience { get; private set; }

        [Header("assets links")]
        [SerializeField] private ItemSlot itemSlotPrefab;

        [Header("links")]
        [SerializeField] private Character character;
        [SerializeField] private AudioSource timeIsUpSound;
        [SerializeField] private AudioSource eatSound;
        [SerializeField] private Animator animator;
        [SerializeField] private Animator orderBoxAnimator;
        [SerializeField] private Transform slotPositionPoint;
        [SerializeField] private Transform uiStatsRoot;
        [SerializeField] private Transform uiEffectsRoot;
        [SerializeField] private ItemData pretzel;

        [Header("ui links")]
        [SerializeField] private GameObject orderBox;
        [SerializeField] private Image orderImage;
        [SerializeField] private CustomersUI customersUI;
        [SerializeField] private CustomersBuffsDisplayer customersBuffsDisplayer;

        private ItemData requiredItemData;
        private ItemSlot slotToPlaceFood;
        private SeatPlace seatPlace;
        private Func<ItemData> getItemDataToOrderFunc;
        private Func<ItemData, ItemData> pretzelOrderHandler;
        private RestaurantEncounter restaurantEncounter;
        private List<ITickable> activeEffects = new();
        private float leavingTimer = 2;
        private bool isLeaving = false;

        public UnityEvent<float> OnPatienceChanged;
        public UnityEvent<int, int> OnHungerPointsChanged;
        public UnityEvent OnHungerPointsIncreased;
        public UnityEvent OnStartLeavingProcess;
        public UnityEvent OnLeft;
        public UnityEvent<bool> OnLeftSatisfied;
        public UnityEvent<Customer> OnCustomerLeft;
        public UnityEvent<Customer, bool> OnCustomerLeftSatisfied;
        public UnityEvent OnAte;
        public UnityEvent<ItemData> OnAteCertainFood;

        public Character Character => character;
        public bool IsLeaving => isLeaving;
        public bool IsSatisfied => HungerPoints <= 0;
        public SeatPlace SeatPlace => seatPlace;

        public void Init(RestaurantEncounter restaurantEncounter, SeatPlace seatPlace, CustomerData customerData, Func<ItemData> getItemDataToOrderFunc)
        {
            slotToPlaceFood = restaurantEncounter.CustomerSlotsToPlaceFoodSpawner.SpawnSlot();
            slotToPlaceFood.transform.position = slotPositionPoint.position;
            slotToPlaceFood.OnItemHasBeenPlaced.AddListener(TryToEat);
            slotToPlaceFood.OnHasBeenOccupied.AddListener(slotToPlaceFood.Clear);

            this.seatPlace = seatPlace;
            this.getItemDataToOrderFunc = getItemDataToOrderFunc;
            restaurantEncounter.Ticker.AddTickable(this);
            restaurantEncounter.CustomersManager.AddCustomer(this);
            this.restaurantEncounter = restaurantEncounter;

            SetCustomerData(customerData, restaurantEncounter);
            MakeOrder();
            customersUI.Init(this);
            customersBuffsDisplayer.Init(this);

            orderImage.transform.rotation = Quaternion.identity;
            uiStatsRoot.rotation = Quaternion.identity;
        }

        public void SetCustomerData(CustomerData customerData, RestaurantEncounter restaurantEncounter)
        {
            MaxHungerPoints = customerData.HungerPoints;
            HungerPoints = customerData.HungerPoints;
            Patience = customerData.Patience;
            character.SetSprite(customerData.Sprite);

            foreach (ICustomerEffect effect in customerData.Effects)
            {
                var instance = effect.CreateInstance();

                if (effect is IAbleToReturnViewPrefab ableToReturnViewPrefab)
                {
                    var prefab = ableToReturnViewPrefab.GetViewPrefab();
                    var viewGO = Instantiate(prefab, uiEffectsRoot);

                    if (viewGO.TryGetComponent<ICustomerEffectView>(out var view))
                    {
                        view.InitBase(instance, restaurantEncounter);
                    }
                }

                if (instance is ITickable) activeEffects.Add(instance as ITickable);
                instance.Apply(this, restaurantEncounter);
            }
        }

        public void SetOrderData(ItemData itemData)
        {
            orderImage.sprite = itemData.Sprite;
            requiredItemData = itemData;
            orderImage.rectTransform.sizeDelta = itemData.Sprite.GetSpriteSizeInPixels() * 0.5f;
            orderBoxAnimator.SetTrigger("order changed");
        }

        public void SetPretzelOrderHandler(Func<ItemData, ItemData> handler)
        {
            pretzelOrderHandler = handler;
        }

        public void TryToEat(Item item)
        {
            var food = item as FoodItem;
            EatVisualEffect();

            if (item.ItemData == requiredItemData)
            {
                OnAte.Invoke();
                OnAteCertainFood.Invoke(item.ItemData);
                MakeOrder();
                GetSatiety(food.Satiety);
            }

            if(item.ItemData == pretzel)
                PretzelEatenHandler();
        }

        public void AutoSatisfy()
        {
            GetSatiety(HungerPoints);
            EatVisualEffect();
        }

        public void AddHunger(int hunger)
        {
            GetSatiety(-hunger);
            OnHungerPointsIncreased.Invoke();
        }

        public void GetSatiety(int satiety)
        {
            if (isLeaving)
                return;

            HungerPoints -= satiety;
            HungerPoints = Math.Clamp(HungerPoints, 0, 100);
            OnHungerPointsChanged.Invoke(HungerPoints, MaxHungerPoints);

            if (IsSatisfied)
            {
                Uninit();
                OnStartLeavingProcess.Invoke();
                isLeaving = true;
            }
        }

        public void MakeOrder()
        {
            SetOrderData(getItemDataToOrderFunc());
            slotToPlaceFood.SetRequiredItemData(requiredItemData);
        }

        public void PretzelEatenHandler()
        {
            print(pretzelOrderHandler);
            print(requiredItemData);
            var data = pretzelOrderHandler(requiredItemData);
            SetOrderData(data);
            slotToPlaceFood.SetRequiredItemData(data);
        }

        public void Tick(float deltaTime)
        {
            if (isLeaving)
            {
                TickLeavingTimer(deltaTime);
            }
            else
            {
                TickPatience(deltaTime);
            }
        }

        private void TickPatience(float deltaTime)
        {
            Patience -= deltaTime;
            Patience = Math.Clamp(Patience, 0, 1000);
            OnPatienceChanged.Invoke(Patience);

            for (int i = 0; i < activeEffects.Count; i++)
            {
                activeEffects[i].Tick(deltaTime);
            }

            if (Patience <= 0)
            {
                Uninit();
                animator.SetTrigger("onEat");
                timeIsUpSound.Play();
                OnStartLeavingProcess.Invoke();
                isLeaving = true;
            }
        }

        private void TickLeavingTimer(float deltaTime)
        {
            leavingTimer -= deltaTime;


            if (leavingTimer <= 0)
            {
                LeaveSatisfied(IsSatisfied);
                restaurantEncounter.Ticker.RemoveTickable(this);
            }
        }

        public void Uninit()
        {
            restaurantEncounter.CustomersManager.RemoveCustomer(this);
            slotToPlaceFood.OnItemHasBeenPlaced.RemoveListener(TryToEat);
            slotToPlaceFood.OnHasBeenOccupied.RemoveListener(slotToPlaceFood.Clear);
            slotToPlaceFood.PreDestroy();

            orderBox.SetActive(false);
        }


        public void LeaveSatisfied(bool isSatisfied)
        {
            OnLeft.Invoke();
            OnCustomerLeft.Invoke(this);
            OnLeftSatisfied.Invoke(isSatisfied);
            OnCustomerLeftSatisfied.Invoke(this, isSatisfied);
            if (slotToPlaceFood != null)
                Destroy(slotToPlaceFood.gameObject);
            Destroy(gameObject);
        }

        public void CenterOnNewParent(Transform newParent)
        {
            transform.parent = newParent;
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
            transform.localRotation = Quaternion.identity;
        }

        public void SetBlockedByTauntOnAnotherCustomer(bool value)
        {
            slotToPlaceFood.SetBlockedValue(value);
        }

        public bool HasTauntEffect => activeEffects.Any(effect => effect is TauntEffectInstance);

        private void EatVisualEffect()
        {
            animator.SetTrigger("onEat");
            eatSound.pitch = 1 + UnityEngine.Random.Range(-0.25f, 0.25f);
            eatSound.Play();
        }
    }
}