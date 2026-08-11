using System.Collections.Generic;
using UnityEngine;

namespace foxRestaurant
{
    public class DifficultySetupPanel : MonoBehaviour
    {
        [SerializeField] private ContentSizeFittersRebilder contentSizeFittersRebilder;

        private void OnEnable()
        {
            contentSizeFittersRebilder.Rebuild();
        }
    }
}