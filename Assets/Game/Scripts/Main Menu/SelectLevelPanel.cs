using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class SelectLevelPanel : MonoBehaviour
    {
        [SerializeField] private Button level1Button;
        [SerializeField] private Button level2Button;
        [SerializeField] private Button backButton;

        public Button Level1Button => level1Button;
        public Button Level2Button => level2Button;
        public Button BackButton => backButton;
    }
}