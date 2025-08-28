using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private ItemsSpawner itemsSpawner;
    [SerializeField] private PlayerInputController playerInputController;
    [SerializeField] private SlotsManager slotsManager;
    [SerializeField] private Transform parentForItemsMovement;
    [SerializeField] private CustomerSpawner customerSpawner;
    [SerializeField] private CookerSlotSpawner cookerSlotSpawner;
    [SerializeField] private CookPositionController cookPositionController;

    [Space]
    [SerializeField] private int cookerSlotsCount;

    public ItemsSpawner ItemsSpawner => itemsSpawner;
    public SlotsManager SlotsManager => slotsManager;
    public Transform ParentForItemsMovement => parentForItemsMovement;

    private void Awake()
    {
        playerInputController.Init(this);
        itemsSpawner.Init(this);
        customerSpawner.Init(this);
        cookerSlotSpawner.Init(this);

        for(int i = 0; i < cookerSlotsCount; i++)
        {
            var cookerSlot = cookerSlotSpawner.SpawnCookerSlot();
            cookerSlot.OnItemHovered.AddListener(() => cookPositionController.HoverSlot(cookerSlot));
        }
    }
}
