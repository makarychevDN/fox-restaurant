using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler
{
    private bool isDragging;

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if(isDragging)
            transform.position = eventData.position;
    }
}
