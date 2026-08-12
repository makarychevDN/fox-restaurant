using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class InitDifficultySetupPanel : MonoBehaviour
    {
        [SerializeField] private Button confirmButton;

        public Button ConfirmButton => confirmButton;
    }
}