using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class ItemStateController : MonoBehaviour
    {
        private MovementState movementState = MovementState.placedInSlot;
        private Level level;
        private Item item;

        public void Init(Level level, Item item)
        {
            this.level = level;
            this.item = item;
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
            item.Slot.SetItem(null);
        }

        public async void OnRelease()
        {
            if (movementState == MovementState.preparedForGrabbing)
            {
                movementState = MovementState.grabbed;
                transform.parent = level.ParentForItemsMovement;
                transform.localScale = Vector3.one;
                item.Slot.SetItem(null);
                return;
            }

            if (movementState == MovementState.grabbed || movementState == MovementState.dragged)
            {
                level.SlotsManager.UnhoverAllSlots();
                await GoToSlot(level.SlotsManager.GetSlotToPlaceItem(item));
            }
        }

        public void OnHover()
        {
            item.Slot.OnItemHovered.Invoke();
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

            item.Slot = slot;
            slot.SetItem(item);
            movementState = MovementState.placedInSlot;
        }

        public void TryToSlice()
        {
            if (movementState != MovementState.placedInSlot)
                return;

            item.Slice();
        }

        private void Update()
        {
            if (movementState == MovementState.grabbed || movementState == MovementState.dragged)
            {
                transform.position = Input.mousePosition;
                var slotToPlaceItem = level.SlotsManager.GetSlotToPlaceItem(item);
                level.SlotsManager.UnhoverAllSlotsExcept(slotToPlaceItem);
                slotToPlaceItem.Hover(item);
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
}