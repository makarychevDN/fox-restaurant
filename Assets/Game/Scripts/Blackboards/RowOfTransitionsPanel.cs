using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class RowOfTransitionsPanel : UIRepeater<RectTransform, ItemData>
    {
        public void SetItems(List<ItemData> itemsData)
        {
            UpdatePanels(itemsData);
        }

        protected override void Bind(RectTransform panel, ItemData data)
        {
            var image = panel.GetComponentInChildren<Image>();
            image.sprite = data.Sprite;
            image.rectTransform.sizeDelta = data.Sprite.GetSpriteSizeInPixels() * 0.5f;
        }
    }
}