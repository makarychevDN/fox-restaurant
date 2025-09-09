using TMPro;
using UnityEngine;

namespace foxRestaurant
{
    public class ItemUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text valueDisplayer;

        public void Init(Item item)
        {
            item.OnSatietyUpdated.AddListener(UpdateDisplayingValue);
            UpdateDisplayingValue(item.Satiety);
        }

        private void UpdateDisplayingValue(int value)
        {
            valueDisplayer.text = value.ToString();
        }
    }
}