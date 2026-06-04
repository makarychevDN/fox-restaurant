using UnityEngine;

namespace foxRestaurant
{
    public class LookForCursorController : MonoBehaviour
    {
        [SerializeField] private float maxXCursorDistance;
        [SerializeField] private float maxYCursorDistance;
        [SerializeField] private float maxXPositionDistance;
        [SerializeField] private float maxYPositionDistance;
        [SerializeField] private Transform target;

        void FixedUpdate()
        {
            Vector3 targetPosition = target != null ? target.position : Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10);
            LookAtPosition(targetPosition);
        }

        private void LookAtPosition(Vector3 targetPosition)
        {
            Vector3 startPosition = transform.parent.position;

            float deltaX = (targetPosition.x - startPosition.x) / maxXCursorDistance;            
            deltaX = Mathf.Clamp(deltaX, -1, 1);
            float deltaY = (targetPosition.y - startPosition.y) / maxYCursorDistance;
            deltaY = Mathf.Clamp(deltaY, -1, 1);

            Vector3 watchingObjectPosition = new Vector3
            (
                startPosition.x + deltaX * maxXPositionDistance,
                startPosition.y + deltaY * maxYPositionDistance,
                transform.position.z
            );

            transform.position = Vector3.Lerp(transform.position, watchingObjectPosition, 0.25f);
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        public void SetDistances(float maxXCursorDistance, float maxYCursorDistance, float maxXPositionDistance, float maxYPositionDistance)
        {
            this.maxXCursorDistance = maxXCursorDistance;
            this.maxYCursorDistance = maxYCursorDistance;
            this.maxXPositionDistance = maxXPositionDistance;
            this.maxYPositionDistance = maxYPositionDistance;
        }
    }
}