using UnityEngine;

namespace foxRestaurant
{
    public class Level : MonoBehaviour
    {
        [field: SerializeField] public ItemsSpawner ItemsSpawner { get; private set; }
        [field: SerializeField] public PlayerInputController PlayerInputController { get; private set; }
        [field: SerializeField] public SlotsManager SlotsManager { get; private set; }
        [field: SerializeField] public Transform ParentForItemsMovement { get; private set; }
        [field: SerializeField] public CustomerSpawner CustomerSpawner { get; private set; }
        [field: SerializeField] public SlotSpawner CookerSlotSpawner { get; private set; }
        [field: SerializeField] public SlotSpawner AdditionalSlotSpawner { get; private set; }
        [field: SerializeField] public CookPositionController CookPositionController { get; private set; }
        [field: SerializeField] public Ticker Ticker { get; private set; }
        [field: SerializeField] public Transform CustomerSlotsParent { get; private set; }
        [field: SerializeField] public Scenario Scenario { get; private set; }
        [field: SerializeField] public DecksManager DecksManager { get; private set; }
        [Space]
        [SerializeField] private int cookerSlotsCount;

        private void Awake()
        {
            PlayerInputController.Init(this);
            ItemsSpawner.Init(this);
            CustomerSpawner.Init(this);
            CookerSlotSpawner.Init(this);
            AdditionalSlotSpawner.Init(this);
            DecksManager.Init();
            Scenario.Init(CustomerSpawner, this);

            for (int i = 0; i < cookerSlotsCount; i++)
            {
                var cookerSlot = CookerSlotSpawner.SpawnSlot();
                cookerSlot.OnItemHovered.AddListener(() => CookPositionController.HoverSlot(cookerSlot));
            }

            for (int i = 0; i < cookerSlotsCount; i++)
            {
                var additionalSlot = AdditionalSlotSpawner.SpawnSlot();
            }
        }
    }
}