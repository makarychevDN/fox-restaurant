using UnityEngine;
using UnityEngine.EventSystems;

namespace foxRestaurant
{
    public class ItemMouseInputController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler
    {
        private Item item;

        public void Init(Item item)
        {
            this.item = item;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                item.OnSelect();

            if (eventData.button == PointerEventData.InputButton.Right)
                item.TryToSlice();
        }

        public void OnDrag(PointerEventData eventData) { }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                item.OnRelease();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            item.OnHover();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            item.OnUnhover();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                item.OnBeginDrag();
        }
    }
}