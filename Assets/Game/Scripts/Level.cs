using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private ItemsSpawner itemsSpawner;
    [SerializeField] private PlayerInputController playerInputController;
    [SerializeField] private SlotsManager slotsManager;
    [SerializeField] private Transform parentForItemsMovement;

    public ItemsSpawner ItemsSpawner => itemsSpawner;
    public SlotsManager SlotsManager => slotsManager;
    public Transform ParentForItemsMovement => parentForItemsMovement;

    private void Awake()
    {
        playerInputController.Init(this);
        itemsSpawner.Init(this);
    }
}
