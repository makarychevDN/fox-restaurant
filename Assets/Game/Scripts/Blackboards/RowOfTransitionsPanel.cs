using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace foxRestaurant
{
    public class RowOfTransitionsPanel : MonoBehaviour
    {
        public void UpdatePanels(List<ItemData> itemsData)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ItemData item in itemsData)
            {
                sb.Append(item.name).Append(" ");
            }
            print(sb.ToString());
        }
    }
}