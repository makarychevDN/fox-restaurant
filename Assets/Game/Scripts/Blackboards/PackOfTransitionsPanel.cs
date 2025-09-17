using System.Collections.Generic;
using UnityEngine;

namespace foxRestaurant
{
    public class PackOfTransitionsPanel : UIRepeater<RowOfTransitionsPanel, List<ItemData>>
    {
        [SerializeField] private RowOfTransitionsPanel rowOfTransitionsPanelPrefab;

        public void UpdatePanels(RootData rootData)
        {
            //UpdatePanels(rootData);
            print(rootData.RootItemData.name);
        }

        protected override void Bind(RowOfTransitionsPanel panel, List<ItemData> itemsData)
        {
            panel.SetListOfItemData(itemsData);
        }
    }
}