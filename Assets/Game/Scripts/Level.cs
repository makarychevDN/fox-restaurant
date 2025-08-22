using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private ItemsSpawner itemsSpawner;
    [SerializeField] private PlayerInputController playerInputController;
    [SerializeField] private SlotsManager slotsManager;
    [SerializeField] private Transform parentForItemsMovement;
    [SerializeField] private CustomerSpawner customerSpawner;

    public ItemsSpawner ItemsSpawner => itemsSpawner;
    public SlotsManager SlotsManager => slotsManager;
    public Transform ParentForItemsMovement => parentForItemsMovement;

    private void Awake()
    {
        playerInputController.Init(this);
        itemsSpawner.Init(this);
        customerSpawner.Init(this);
    }
}
