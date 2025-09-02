using UnityEngine;

namespace foxRestaurant
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private ItemsSpawner itemsSpawner;
        [SerializeField] private PlayerInputController playerInputController;
        [SerializeField] private SlotsManager slotsManager;
        [SerializeField] private Transform parentForItemsMovement;
        [SerializeField] private CustomerSpawner customerSpawner;
        [SerializeField] private CookerSlotSpawner cookerSlotSpawner;
        [SerializeField] private CookPositionController cookPositionController;
        [SerializeField] private Ticker ticker;
        [SerializeField] private Transform customerSlotsParent;
        [SerializeField] private Scenario scenario;

        [Space]
        [SerializeField] private int cookerSlotsCount;

        public ItemsSpawner ItemsSpawner => itemsSpawner;
        public SlotsManager SlotsManager => slotsManager;
        public Ticker Ticker => ticker;
        public Transform ParentForItemsMovement => parentForItemsMovement;
        public Transform CustomerSlotsParent => customerSlotsParent;

        private void Awake()
        {
            playerInputController.Init(this);
            itemsSpawner.Init(this);
            customerSpawner.Init(this);
            cookerSlotSpawner.Init(this);
            scenario.Init(customerSpawner);

            for (int i = 0; i < cookerSlotsCount; i++)
            {
                var cookerSlot = cookerSlotSpawner.SpawnCookerSlot();
                cookerSlot.OnItemHovered.AddListener(() => cookPositionController.HoverSlot(cookerSlot));
            }
        }
    }
}