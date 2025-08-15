using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private ItemsSpawner itemsSpawner;
    [SerializeField] private PlayerInputController playerInputController;

    public ItemsSpawner ItemsSpawner => itemsSpawner;

    private void Awake()
    {
        playerInputController.Init(this);
    }
}
