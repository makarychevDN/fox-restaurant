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

        if (movementState == MovementState.grabbed)
        {
            await GoBackToTheLastSlot();
        }
    }

    private async void SingleLMBUpHandler()
    {
        if (movementState == MovementState.preparedForGrabbing)
        {
            movementState = MovementState.grabbed;
            return;
        }

        if (movementState == MovementState.grabbed || movementState == MovementState.dragged)
        {
            var closestAvailableSlot = level.SlotsManager.GetClosestAvailableSlot(this);
            if (closestAvailableSlot != null)
            {
                slot.SetItem(null);
                PlaceItemInSlot(closestAvailableSlot);
            }
            else
                await GoBackToTheLastSlot();
        }
    }

    private void DragByLMBClickHandler(PointerEventData eventData)
    {
        if (movementState == MovementState.goingBackToLastSlot)
            return;

        movementState = MovementState.dragged;
    }

    private void PlaceItemInSlot(ItemSlot slot)
    {
        this.slot = slot;
        slot.SetItem(this);
        movementState = MovementState.placedInSlot;
    }

    private async Task GoBackToTheLastSlot()
    {
        movementState = MovementState.goingBackToLastSlot;
        await transform.DOMove(slot.transform.position, 0.1f).AsyncWaitForCompletion();
        PlaceItemInSlot(slot);
    }

    private void Update()
    {
        if(movementState == MovementState.grabbed || movementState == MovementState.dragged)
        {
            transform.position = Input.mousePosition;

            var test = level.SlotsManager.GetClosestAvailableSlot(this);

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
