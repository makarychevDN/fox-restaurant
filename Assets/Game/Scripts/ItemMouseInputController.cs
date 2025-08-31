using UnityEngine;
using UnityEngine.EventSystems;

namespace foxRestaurant
{
    public class ItemMouseInputController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private ItemMovement itemMovement;
        private Item item;

        public void Init(ItemMovement itemMovement, Item item)
        {
            this.itemMovement = itemMovement;
            this.item = item;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                itemMovement.OnSelect();

            if (eventData.button == PointerEventData.InputButton.Right)
                item.Slice();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                itemMovement.OnDrag();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                itemMovement.OnRelease();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            itemMovement.OnHover();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            itemMovement.OnUnhover();
        }
    }
}