using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] private ItemSlot slot;

    public void Init()
    {
        slot.OnItemHasBeenPlaced.AddListener(TryToFeetCustomer);
        slot.OnHasBeenOccupied.AddListener(slot.Clear);
    }

    public void TryToFeetCustomer(Item item)
    {
        print(item.name);
    }
}
