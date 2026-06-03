using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class ContentSizeFittersRebilder : MonoBehaviour
    {
        [SerializeField] private List<RectTransform> contentSizeFitters;

        public void Rebuild()
        {
            contentSizeFitters.ForEach(fitter =>
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(fitter);
            });
        }
    }
}