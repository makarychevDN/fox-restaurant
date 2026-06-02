using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class SettingsMenuPanel : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        public Button BackButton => backButton;
    }
}