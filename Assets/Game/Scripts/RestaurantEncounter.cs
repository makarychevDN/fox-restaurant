using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace foxRestaurant
{
    public class RestaurantEncounter : Encounter
    {
        [field: Header("Stats")]
        [SerializeField] private int cookerSlotsCount;
        [SerializeField] private int additionalSlotsCount;
        [SerializeField] private int spawnItemSlotsCount;
        [SerializeField] private int spawnCustomerSlotsCount;

        [field: Header("Controllers And Managers")]
        [field: SerializeField] public PlayerInputController PlayerInputController { get; private set; }
        [field: SerializeField] public SlotsManager SlotsManager { get; private set; }
        [field: SerializeField] public CookPositionController CookPositionController { get; private set; }
        [field: SerializeField] public DecksManager DecksManager { get; private set; }
        [field: SerializeField] public Ticker Ticker { get; private set; }
        [field: SerializeField] public RestaurantScenario Scenario { get; private set; }
        [field: SerializeField] public DynamicTextManager DynamicTextManager { get; private set; }

        [field: Header("Spawners")]
        [field: SerializeField] public ItemsSpawner ItemsSpawner { get; private set; }
        [field: SerializeField] public ItemSpawnTimer ItemSpawnTimer { get; private set; }
        [field: SerializeField] public CustomerSpawner CustomerSpawner { get; private set; }
        [field: SerializeField] public SlotSpawner CookerSlotSpawner { get; private set; }
        [field: SerializeField] public SlotSpawner SlotsToSpawnFoodSpawner { get; private set; }
        [field: SerializeField] public SlotSpawner CustomerSlotsToPlaceFoodSpawner { get; private set; }
        [field: SerializeField] public SlotSpawner AdditionalFoodSlotSpawner { get; private set; }
        [field: SerializeField] public SlotSpawner SlotsToSpawnCustomerItemsSpawner { get; private set; }
        [field: SerializeField] public SlotSpawner SlotsToPlaceCustomerItemsSpawner { get; private set; }
        [field: SerializeField] public ItemSlot GarbageCanSlot { get; private set; }

        [field: Header("UI")]
        [field: SerializeField] public RecipeBlackBoard RecipeBlackBoard { get; private set; }
        [field: SerializeField] public TransitionsBlackBoard TransitionsBlackBoard { get; private set; }
        [field: SerializeField] public ItemHintsDisplayer LMBItemHintsDisplayer { get; private set; }
        [field: SerializeField] public ItemHintsDisplayer RMBItemHintsDisplayer { get; private set; }
        [field: SerializeField] public SliceEffectDisplayer SliceEffectDisplayer { get; private set; }

        [field: Header("Parent Objects")]
        [field: SerializeField] public Transform ParentForItemsMovement { get; private set; }

        [field: Header("Data Base")]
        [field: SerializeField] public RecipesManager RecipesManager { get; private set; }
        [field: SerializeField] public ItemTransitionsManager ItemTransitionsManager { get; private set; }
        [field: SerializeField] public RestaurantEncounterData restaurantEncounterData { get; private set; }
        public DataBase DataBase { get; private set; }

        private List<ItemData> allPossibleItemData;
        private TaskCompletionSource<bool> completionSource = new TaskCompletionSource<bool>();

        public UnityEvent<DataBase> OnDataBaseUpdated;


        public override void Init()
        {
            allPossibleItemData = restaurantEncounterData.AllPossibleItemData.DataList;
            RecipeBlackBoard.Init(this);
            TransitionsBlackBoard.Init(this);
            UpdateDataBase(restaurantEncounterData.CsvFile);

            PlayerInputController.Init(this);
            CookerSlotSpawner.Init(this);
            SlotsToSpawnFoodSpawner.Init(this);
            AdditionalFoodSlotSpawner.Init(this);
            CustomerSlotsToPlaceFoodSpawner.Init(this);
            SlotsToSpawnCustomerItemsSpawner.Init(this);
            SlotsToPlaceCustomerItemsSpawner.Init(this);
            CustomerSpawner.Init(this);
            ItemsSpawner.Init(this);
            GarbageCanSlot.Init(this);
            DecksManager.Init();

            RecipesManager.Init(this);
            ItemTransitionsManager.Init(this);

            for (int i = 0; i < cookerSlotsCount; i++)
            {
                var cookerSlot = CookerSlotSpawner.SpawnSlot();

                cookerSlot.OnItemHovered.AddListener(() => CookPositionController.HoverSlot(cookerSlot));
                cookerSlot.OnItemUnhovered.AddListener(CookPositionController.Unhover);

                cookerSlot.OnItemHovered.AddListener(() => LMBItemHintsDisplayer.ShowHint(cookerSlot));
                cookerSlot.OnItemHovered.AddListener(() => RMBItemHintsDisplayer.ShowHint(cookerSlot));

                cookerSlot.OnItemUnhovered.AddListener(LMBItemHintsDisplayer.HideHint);
                cookerSlot.OnItemUnhovered.AddListener(RMBItemHintsDisplayer.HideHint);

                cookerSlot.OnItemSliced.AddListener(() => SliceEffectDisplayer.Play(cookerSlot));
                cookerSlot.OnItemSliced.AddListener(CookPositionController.Slice);

            }

            SpawnSlots(AdditionalFoodSlotSpawner, additionalSlotsCount);
            SpawnSlots(SlotsToSpawnFoodSpawner, spawnItemSlotsCount);
            SpawnSlots(SlotsToSpawnCustomerItemsSpawner, spawnCustomerSlotsCount);

            if(ItemSpawnTimer != null) 
                ItemSpawnTimer.Init(this);
            if(Scenario != null)
                Scenario.Init(this);
        }

        private void SpawnSlots(SlotSpawner slotSpawner, int slotsAmount)
        {
            for (int i = 0; i < slotsAmount; i++)
            {
                var spawnedSlot = slotSpawner.SpawnSlot();

                spawnedSlot.OnItemHovered.AddListener(() => LMBItemHintsDisplayer.ShowHint(spawnedSlot));
                spawnedSlot.OnItemUnhovered.AddListener(LMBItemHintsDisplayer.HideHint);
            }
        }

        public void UpdateDataBase(TextAsset csvFile)
        {
            DataBase = new DataBase(csvFile.ToStringsArray(), allPossibleItemData);
            OnDataBaseUpdated.Invoke(DataBase);
        }

        public override async Task StartEncounter()
        {
            await completionSource.Task;
        }

        public void Complete()
        {
            if (completionSource != null && !completionSource.Task.IsCompleted)
            {
                completionSource.SetResult(true);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Complete();
            }
        }
    }
}