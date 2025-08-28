using UnityEngine;

namespace foxRestaurant
{
    public class LookForCursorController : MonoBehaviour
    {
        [SerializeField] private float maxXCursorDistance;
        [SerializeField] private float maxYCursorDistance;
        [SerializeField] private float maxXPositionDistance;
        [SerializeField] private float maxYPositionDistance;

        void Update()
        {
            Vector3 cursorWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10);
            Vector3 delta = cursorWorldPosition - transform.parent.position;

            float xDistance = Mathf.Lerp(0, maxXPositionDistance, delta.magnitude / maxXCursorDistance);
            float yDistance = Mathf.Lerp(0, maxYPositionDistance, delta.magnitude / maxYCursorDistance);

            transform.localPosition = new Vector3(delta.normalized.x * xDistance, delta.normalized.y * yDistance);
        }
    }
}