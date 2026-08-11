using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class ContentSizeFittersRebilder : MonoBehaviour
    {
        [SerializeField] private List<RectTransform> contentSizeFitters;
        [SerializeField] private bool executeOnEnable;

        public void Rebuild()
        {
            contentSizeFitters.ForEach(fitter =>
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(fitter);
            });
        }

        private void OnEnable()
        {
            if (executeOnEnable)
                Rebuild();
        }
    }
}