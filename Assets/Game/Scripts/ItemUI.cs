using TMPro;
using UnityEngine;

namespace foxRestaurant
{
    public class ItemUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text satienseIndicator;

        public void Init(Item item)
        {
            item.OnSatietyUpdated.AddListener(UpdateDisplayingValue);
            UpdateDisplayingValue(item.Satiety);
        }

        private void UpdateDisplayingValue(int value)
        {
            satienseIndicator.text = value.ToString();
        }
    }
}