using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace foxRestaurant
{
    public class PositionOnCurveCalculator : MonoBehaviour
    {
        [SerializeField] private AnimationCurve curve;
        [SerializeField] private float height;
        [SerializeField] private float width;
        [SerializeField] private int sampleResolution = 1000; // Чем выше, тем точнее равномерность

        private List<Vector2> positions = new();
        private int count;

        public UnityEvent OnPositionsRecalculated;

        public void CalculatePositions()
        {
            if (count < 2) return;

            // 1. Сэмплируем кривую
            List<float> cumulativeLengths = new();
            List<float> ts = new();
            cumulativeLengths.Add(0f);
            ts.Add(0f);

            float totalLength = 0f;
            Vector2 prev = GetPoint(0f);

            for (int i = 1; i <= sampleResolution; i++)
            {
                float t = (float)i / sampleResolution;
                Vector2 p = GetPoint(t);

                totalLength += Vector2.Distance(prev, p);
                cumulativeLengths.Add(totalLength);
                ts.Add(t);

                prev = p;
            }

            // 2. Целевые позиции
            positions.Clear();
            for (int i = 0; i < count; i++)
            {
                float targetLength = totalLength * i / (count - 1);

                // Находим ближайший сегмент
                int seg = cumulativeLengths.FindIndex(l => l >= targetLength);
                if (seg <= 0) seg = 1;

                float l1 = cumulativeLengths[seg - 1];
                float l2 = cumulativeLengths[seg];
                float t1 = ts[seg - 1];
                float t2 = ts[seg];

                // Линейная интерполяция по длине
                float lerp = (targetLength - l1) / (l2 - l1);
                float tFinal = Mathf.Lerp(t1, t2, lerp);

                positions.Add(GetPoint(tFinal));
            }

            OnPositionsRecalculated?.Invoke();
        }

        private Vector2 GetPoint(float t)
        {
            return new Vector2(
                t * width - width * 0.5f,
                curve.Evaluate(t) * height
            );
        }

        public void SetCount(int count)
        {
            this.count = count;
            CalculatePositions();
        }

        public void IncreaseCount(int delta)
        {
            count += delta;
            CalculatePositions();
        }

        public Vector2 GetPosition(int index) => positions[index];
    }
}