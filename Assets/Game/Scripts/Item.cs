using DG.Tweening;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private ItemSlot slot;
    [SerializeField] private MovementState movementState = MovementState.placedInSlot;

    public void SetSlot(ItemSlot slot) => this.slot = slot;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) 
            TryToGrab(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            TryToDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            TryToReleaseDraggingCard();
            TryToReleaseGrabbedCard();
        }
    }

    private async void TryToGrab(PointerEventData eventData)
    {
        if (movementState == MovementState.placedInSlot)
            movementState = MovementState.preparedForGrabbing;

        if (movementState == MovementState.grabbed)
        {
            await GoBackToTheLastSlot();
        }
    }

    private async void TryToReleaseDraggingCard()
    {
        if (movementState == MovementState.dragged)
            await GoBackToTheLastSlot();
    }

    private async void TryToReleaseGrabbedCard()
    {
        if (movementState == MovementState.grabbed)
        {
            await GoBackToTheLastSlot();
            return;
        }

        if (movementState == MovementState.preparedForGrabbing)
        {
            movementState = MovementState.grabbed;
        }
    }

    private void TryToDrag(PointerEventData eventData)
    {
        if (movementState == MovementState.goingBackToLastSlot)
            return;

        movementState = MovementState.dragged;
        transform.position = eventData.position;
    }

    private async Task GoBackToTheLastSlot()
    {
        movementState = MovementState.goingBackToLastSlot;
        await transform.DOMove(slot.transform.position, 0.1f).AsyncWaitForCompletion();
        slot.SetItem(this);
        movementState = MovementState.placedInSlot;
    }

    private void Update()
    {
        if(movementState == MovementState.grabbed)
        {
            //transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = Input.mousePosition;
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
