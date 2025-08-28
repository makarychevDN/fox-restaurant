using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemMouseInputController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
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
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            item.OnDrag();
    }

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
}
