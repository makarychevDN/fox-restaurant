using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace foxRestaurant
{
    public class ItemMouseInputController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private ItemMovement itemMovement;

        public void Init(ItemMovement item)
        {
            this.itemMovement = item;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                itemMovement.OnSelect();
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