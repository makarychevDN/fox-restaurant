using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private Image image;

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

    private void SingleLMBDownHandler(PointerEventData eventData)
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

    private void DragByLMBClickHandler(PointerEventData eventData)
    {
        if (movementState == MovementState.goingBackToLastSlot)
            return;

        transform.parent = level.ParentForItemsMovement;
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
