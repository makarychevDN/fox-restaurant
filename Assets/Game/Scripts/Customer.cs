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

        private Item requiredItem;
        private ItemSlot slotToPlaceFood;

        public void Init(Level level, CustomerData customerData)
        {
            slotToPlaceFood = SpawnSlot(level);
            slotToPlaceFood.OnItemHasBeenPlaced.AddListener(TryToEat);
            slotToPlaceFood.OnHasBeenOccupied.AddListener(slotToPlaceFood.Clear);

            SetCustomerData(customerData);   
            
            orderImage.transform.rotation = Quaternion.identity;
            uiStatsRoot.rotation = Quaternion.identity;

            level.Ticker.AddTickable(this);
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
            MakeOrder();
        }

        public void TryToEat(Item item)
        {
            animator.SetTrigger("onEat");
            eatingSound.Play();
        }

        public void MakeOrder()
        {

        }

        public void Tick(float deltaTime)
        {
            patience -= deltaTime;
            patienceIndicator.text = patience.ToString("0");
        }
    }
}