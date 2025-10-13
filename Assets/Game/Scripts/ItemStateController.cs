using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace foxRestaurant
{
    public class ItemStateController : MonoBehaviour
    {
        [SerializeField] private MovementState movementState = MovementState.placedInSlot;
        [SerializeField] private AudioSource pickItUpSound;
        private RestaurantEncounter restaurantEncounter;
        private Item item;

        public UnityEvent<ItemSlot> OnItemWasHoveredByThisItem;

        public void Init(RestaurantEncounter restaurantEncounter, Item item)
        {
            this.restaurantEncounter = restaurantEncounter;
            this.item = item;
            transform.localPosition = Vector2.zero;
        }

        public void OnSelect()
        {
            if (movementState == MovementState.goingBackToLastSlot)
                return;

            if (movementState == MovementState.placedInSlot)
                movementState = MovementState.preparedForGrabbing;
        }

        public void OnBeginDrag()
        {
            if (movementState == MovementState.goingBackToLastSlot)
                return;

            BeginMovementHandler(MovementState.dragged);
        }

        public async void OnRelease()
        {
            if (movementState == MovementState.preparedForGrabbing)
            {
                BeginMovementHandler(MovementState.grabbed);
                return;
            }

            if (movementState == MovementState.grabbed || movementState == MovementState.dragged)
            {
                restaurantEncounter.SlotsManager.UnhoverAllSlots();
                await GoToSlot(restaurantEncounter.SlotsManager.GetSlotToPlaceItem(item));
            }
        }

        private void BeginMovementHandler(MovementState actualState)
        {
            pickItUpSound.Play();
            movementState = actualState;
            transform.parent = restaurantEncounter.ParentForItemsMovement;
            transform.localScale = Vector3.one;
            item.Slot.SetItem(null);
        }

        public void OnHover()
        {
            item.Slot.OnItemHovered.Invoke();
            transform.DOScale(new Vector3(1.15f, 1.15f, 1), 0.05f);
        }

        public void OnUnhover()
        {
            item.Slot.OnItemUnhovered.Invoke();
            transform.DOScale(Vector3.one, 0.05f);
        }

        private async Task GoToSlot(ItemSlot slot)
        {
            movementState = MovementState.goingBackToLastSlot;
            slot.SetSelectedForItemMovement(true);

            transform.DOScale(slot.transform.localScale, 0.1f);
            await transform.DOMove(slot.CenterForItem.position, 0.1f).AsyncWaitForCompletion();

            slot.SetSelectedForItemMovement(false);
            item.Slot = slot;
            slot.SetItem(item);
            movementState = MovementState.placedInSlot;
        }

        public void TryToSlice()
        {
            if (movementState != MovementState.placedInSlot)
                return;

            var foodItem = item as FoodItem;
            if (foodItem == null)
                return;

            foodItem.Slice();
        }

        private void Update()
        {
            if (movementState == MovementState.grabbed || movementState == MovementState.dragged)
            {
                transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10);
                var slotToPlaceItem = restaurantEncounter.SlotsManager.GetSlotToPlaceItem(item);
                restaurantEncounter.SlotsManager.UnhoverAllSlotsExcept(slotToPlaceItem);
                OnItemWasHoveredByThisItem.Invoke(slotToPlaceItem);

                if (slotToPlaceItem.Empty)
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