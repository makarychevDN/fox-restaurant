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
            eyes.ForEach(eye => { eye.SetTarget(null); eye.SetDistances(10, 10, 100, 100); });
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            eyes.ForEach(eye => { eye.SetTarget(fish); eye.SetDistances(1, 1, 7, 7); });
        }
    }
}