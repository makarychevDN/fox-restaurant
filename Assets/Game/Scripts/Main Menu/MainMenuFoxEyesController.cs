using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace foxRestaurant
{
    public class MainMenuFoxEyesController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private List<LookForCursorController> eyes;
        [SerializeField] private Transform fish;

        public void OnPointerEnter(PointerEventData eventData)
        {
            eyes.ForEach(eye => { eye.SetTarget(null); /*eye.SetDistances(10, 10, 70, 70);*/ });
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            eyes.ForEach(eye => { eye.SetTarget(fish); /*eye.SetDistances(2, 1, 14, 7);*/ });
        }
    }
}