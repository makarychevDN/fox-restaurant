using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private ItemMouseInputController inputController;

    private ItemSlot slot;
    private MovementState movementState = MovementState.placedInSlot;
    private Level level;

    public void SetSlot(ItemSlot slot) => this.slot = slot;
    public Level Level => level;
    public ItemSlot Slot => slot;
    public Image Image => image;

    public void Init(Level level)
    {
        this.level = level;
        inputController.Init(this);
    }

    public void OnSelect()
    {
        if (movementState == MovementState.goingBackToLastSlot)
            return;

        if (movementState == MovementState.placedInSlot)
            movementState = MovementState.preparedForGrabbing;
    }

    public void OnDrag()
    {
        if (movementState == MovementState.goingBackToLastSlot)
            return;

        transform.parent = level.ParentForItemsMovement;
        movementState = MovementState.dragged;
        slot.SetItem(null);
    }

    public async void OnRelease()
    {
        if (movementState == MovementState.preparedForGrabbing)
        {
            movementState = MovementState.grabbed;
            transform.parent = level.ParentForItemsMovement;
            slot.SetItem(null);
            return;
        }

        if (movementState == MovementState.grabbed || movementState == MovementState.dragged)
        {
            level.SlotsManager.UnhoverAllSlots();
            await GoToSlot(level.SlotsManager.GetSlotToPlaceItem(this));
        }
    }

    private async Task GoToSlot(ItemSlot slot)
    {
        movementState = MovementState.goingBackToLastSlot;

        await transform.DOMove(slot.transform.position, 0.1f).AsyncWaitForCompletion();

        this.slot = slot;
        slot.SetItem(this);
        movementState = MovementState.placedInSlot;
    }

    private void Update()
    {
        if(movementState == MovementState.grabbed || movementState == MovementState.dragged)
        {
            transform.position = Input.mousePosition;
            var slotToPlaceItem = level.SlotsManager.GetSlotToPlaceItem(this);
            level.SlotsManager.UnhoverAllSlotsExcept(slotToPlaceItem);
            slotToPlaceItem.Hover(this);
        }
    }

    private enum MovementState
    {
        placedInSlot,
        dragged,
        preparedForGrabbing,
        grabbed,
        goingBackToLastSlot
    }
}
