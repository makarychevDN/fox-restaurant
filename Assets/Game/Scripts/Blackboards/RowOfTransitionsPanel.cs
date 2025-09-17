using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class RowOfTransitionsPanel : UIRepeater<Image, ItemData>
    {
        public void SetItems(List<ItemData> itemsData)
        {
            UpdatePanels(itemsData);
        }

        protected override void Bind(Image panel, ItemData data)
        {
            panel.sprite = data.Sprite;
            panel.rectTransform.sizeDelta = data.Sprite.GetSpriteSizeInPixels() * 0.5f;
        }
    }
}