using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private ItemsSpawner itemsSpawner;
    [SerializeField] private PlayerInputController playerInputController;
    [SerializeField] private SlotsManager slotsManager;

    public ItemsSpawner ItemsSpawner => itemsSpawner;
    public SlotsManager SlotsManager => slotsManager;

    private void Awake()
    {
        playerInputController.Init(this);
        itemsSpawner.Init(this);
    }
}
