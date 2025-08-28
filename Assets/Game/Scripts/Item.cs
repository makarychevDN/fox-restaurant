using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;
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
        transform.localPosition = new Vector2(0, -100);
        transform.DOLocalMove(Vector3.zero, 0.1f);
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

        movementState = MovementState.dragged;
        transform.parent = level.ParentForItemsMovement;
        transform.localScale = Vector3.one;
        slot.SetItem(null);
    }

    public async void OnRelease()
    {
        if (movementState == MovementState.preparedForGrabbing)
        {
            movementState = MovementState.grabbed;
            transform.parent = level.ParentForItemsMovement;
            transform.localScale = Vector3.one;
            slot.SetItem(null);
            return;
        }

        if (movementState == MovementState.grabbed || movementState == MovementState.dragged)
        {
            level.SlotsManager.UnhoverAllSlots();
            await GoToSlot(level.SlotsManager.GetSlotToPlaceItem(this));
        }
    }

    public void OnHover()
    {
        slot.OnItemHovered.Invoke();
        transform.DOScale(new Vector3(1.15f, 1.15f, 1), 0.05f);
    }

    public void OnUnhover()
    {
        transform.DOScale(Vector3.one, 0.05f);
    }

    private async Task GoToSlot(ItemSlot slot)
    {
        movementState = MovementState.goingBackToLastSlot;

        transform.DOScale(slot.transform.localScale, 0.1f);
        await transform.DOMove(slot.CenterForItem.position, 0.1f).AsyncWaitForCompletion();

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
