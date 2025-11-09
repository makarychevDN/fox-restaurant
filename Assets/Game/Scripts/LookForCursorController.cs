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
            WatchAtPosition(targetPosition);
        }

        private void WatchAtPosition(Vector3 targetPositon)
        {
            Vector3 delta = targetPositon - transform.parent.position;

            float xDistance = Mathf.Lerp(0, maxXPositionDistance, delta.magnitude / maxXCursorDistance);
            float yDistance = Mathf.Lerp(0, maxYPositionDistance, delta.magnitude / maxYCursorDistance);

            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(delta.normalized.x * xDistance, delta.normalized.y * yDistance), 0.25f);
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