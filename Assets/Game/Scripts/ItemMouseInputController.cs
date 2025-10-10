using UnityEngine;
using UnityEngine.EventSystems;

namespace foxRestaurant
{
    public class ItemMouseInputController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler
    {
        private ItemStateController itemStateController;

        public void Init(ItemStateController itemStateController, Item item)
        {
            this.itemStateController = itemStateController;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                itemStateController.OnSelect();

            if (eventData.button == PointerEventData.InputButton.Right)
                itemStateController.TryToSlice();
        }

        public void OnDrag(PointerEventData eventData) { }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                itemStateController.OnRelease();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            itemStateController.OnHover();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            itemStateController.OnUnhover();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                itemStateController.OnBeginDrag();
        }
    }
}