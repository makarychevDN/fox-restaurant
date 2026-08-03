using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class SelectLevelPanel : MonoBehaviour
    {
        [SerializeField] private Button level1Button;
        [SerializeField] private Button level2Button;
        [SerializeField] private Button backButton;
        [SerializeField] private List<Button> levelButtons;
        
        public Button Level1Button => level1Button;
        public Button Level2Button => level2Button;
        public Button BackButton => backButton;
        public List<Button> LevelButtons => levelButtons;
    }
}