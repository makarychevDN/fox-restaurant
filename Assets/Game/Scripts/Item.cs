using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private ItemSlot slot;
    private MovementState movementState = MovementState.placedInSlot;
    private Level level;

    public void SetSlot(ItemSlot slot) => this.slot = slot;
    public Level Level => level;
    public ItemSlot Slot => slot;

    public void Init(Level level)
    {
        this.level = level;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) 
            SingleLMBDownHandler(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            DragByLMBClickHandler(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            SingleLMBUpHandler();
        }
    }

    private async void SingleLMBDownHandler(PointerEventData eventData)
    {
        if (movementState == MovementState.goingBackToLastSlot)
            return;

        if (movementState == MovementState.placedInSlot)
            movementState = MovementState.preparedForGrabbing;
    }

    private async void SingleLMBUpHandler()
    {
        if (movementState == MovementState.preparedForGrabbing)
        {
            movementState = MovementState.grabbed;
            slot.SetItem(null);
            return;
        }

        if (movementState == MovementState.grabbed || movementState == MovementState.dragged)
        {
            var targetSlot = level.SlotsManager.GetSlotToPlaceItem(this);
            if (targetSlot != null)
            {
                await GoToSlot(targetSlot);
            }
        }
    }

    private void DragByLMBClickHandler(PointerEventData eventData)
    {
        if (movementState == MovementState.goingBackToLastSlot)
            return;

        movementState = MovementState.dragged;
        slot.SetItem(null);
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

            var test = level.SlotsManager.GetSlotToPlaceItem(this);

            if(test != null)
            {
                print(test.name);
            }
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
