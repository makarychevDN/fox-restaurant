using System.Collections.Generic;
using UnityEngine;

namespace foxRestaurant
{
    public class MainMenuButtonsPositionsSetter : MonoBehaviour
    {
        [SerializeField] private List<Transform> elements;
        [SerializeField] private PositionOnCurveCalculator positionOnCurveCalculator;

        private void Update()
        {
            positionOnCurveCalculator.SetCount(elements.Count);

            for(int i = 0; i < elements.Count; i++)
            {
                elements[i].transform.localPosition = positionOnCurveCalculator.GetPosition(i);
            }
        }
    }
}