using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class ItemArrow : MonoBehaviour
    {
        [SerializeField] private Image imageOfRecepie;
        [SerializeField] private GameObject plusIcon;

        public void PointOnSlot(Item item, ItemSlot itemSlot) => PointArrow(item.transform.position, itemSlot.transform.position);

        public void PointArrow(Vector3 startPosition, Vector3 endPosition)
        {
            Vector3 direction = endPosition - startPosition;
            float magnitude = Vector3.Magnitude(endPosition - startPosition);
            float angle = Vector2.SignedAngle(Vector2.right, direction);

            transform.rotation = Quaternion.Euler(0, 0, angle);
            var arrowsRectTransform = (transform as RectTransform);
            arrowsRectTransform.sizeDelta = new Vector2(magnitude, arrowsRectTransform.sizeDelta.y);
        }
    }
}