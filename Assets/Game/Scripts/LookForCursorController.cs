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

        private void WatchAtPosition(Vector3 targetPosition)
        {
            Transform parent = transform.parent;

            // Переводим мировую позицию цели в локальное пространство родителя
            Vector3 localTarget = parent.InverseTransformPoint(targetPosition);

            // Теперь работаем полностью в локальных координатах
            Vector3 delta = localTarget;

            float xDistance = Mathf.Lerp(0, maxXPositionDistance, Mathf.Clamp01(Mathf.Abs(delta.x) / maxXCursorDistance));
            float yDistance = Mathf.Lerp(0, maxYPositionDistance, Mathf.Clamp01(Mathf.Abs(delta.y) / maxYCursorDistance));

            Vector3 targetLocalPosition = new Vector3(
                Mathf.Sign(delta.x) * xDistance,
                Mathf.Sign(delta.y) * yDistance,
                0f
            );

            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                targetLocalPosition,
                0.25f
            );
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