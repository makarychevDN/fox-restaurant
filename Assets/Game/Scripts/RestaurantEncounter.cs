using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace foxRestaurant
{
    public class RestaurantEncounter : MonoBehaviour
    {
        [field: Header("Stats")]
        [SerializeField] private int cookerSlotsCount;
        [SerializeField] private int additionalSlotsCount;

        [field: Header("Controllers And Managers")]
        [field: SerializeField] public PlayerInputController PlayerInputController { get; private set; }
        [field: SerializeField] public SlotsManager SlotsManager { get; private set; }
        [field: SerializeField] public CookPositionController CookPositionController { get; private set; }
        [field: SerializeField] public DecksManager DecksManager { get; private set; }
        [field: SerializeField] public Ticker Ticker { get; private set; }
        [field: SerializeField] public Scenario Scenario { get; private set; }
        [field: SerializeField] public DynamicTextManager DynamicTextManager { get; private set; }

        [field: Header("Spawners")]
        [field: SerializeField] public ItemsSpawner ItemsSpawner { get; private set; }
        [field: SerializeField] public CustomerSpawner CustomerSpawner { get; private set; }
        [field: SerializeField] public SlotSpawner CookerSlotSpawner { get; private set; }
        [field: SerializeField] public SlotSpawner AdditionalSlotSpawner { get; private set; }
        [field: SerializeField] public SlotSpawner CustomerlSlotSpawner { get; private set; }

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

        public UnityEvent<DataBase> OnDataBaseUpdated;


        public void Init(RestaurantEncounterData restaurantEncounterData)
        {
            allPossibleItemData = restaurantEncounterData.AllPossibleItemData.DataList;
            RecipeBlackBoard.Init(this);
            TransitionsBlackBoard.Init(this);
            UpdateDataBase(restaurantEncounterData.CsvFile);

            PlayerInputController.Init(this);
            ItemsSpawner.Init(this);
            CustomerSpawner.Init(this);
            CookerSlotSpawner.Init(this);
            AdditionalSlotSpawner.Init(this);
            CustomerlSlotSpawner.Init(this);
            DecksManager.Init();
            Scenario.Init(CustomerSpawner, this);

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

            for (int i = 0; i < additionalSlotsCount; i++)
            {
                var additionalSlot = AdditionalSlotSpawner.SpawnSlot();

                additionalSlot.OnItemHovered.AddListener(() => LMBItemHintsDisplayer.ShowHint(additionalSlot));
                additionalSlot.OnItemUnhovered.AddListener(LMBItemHintsDisplayer.HideHint);
            }
        }

        public void UpdateDataBase(TextAsset csvFile)
        {
            DataBase = new DataBase(csvFile.ToStringsArray(), allPossibleItemData);
            OnDataBaseUpdated.Invoke(DataBase);
        }

        private void Awake()
        {
            Init(restaurantEncounterData);
        }
    }
}