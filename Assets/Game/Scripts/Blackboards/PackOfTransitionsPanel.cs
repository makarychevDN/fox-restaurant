using System.Collections.Generic;

namespace foxRestaurant
{
    public class PackOfTransitionsPanel : UIRepeater<RowOfTransitionsPanel, List<ItemData>>
    {
        public void UpdatePanels(RootData rootData)
        {
            List<List<ItemData>> rows = new List<List<ItemData>>();
            var slicingIterator = rootData.RootItemData;  
            
            while (slicingIterator != null)
            {
                List<ItemData> row = new List<ItemData>();
                ItemData fryingterator = slicingIterator;

                while (fryingterator != null)
                {
                    row.Add(fryingterator);
                    rootData.DataBase.FryingResults.TryGetValue(fryingterator, out fryingterator);
                }

                rows.Add(row);
                rootData.DataBase.SlicingResults.TryGetValue(slicingIterator, out slicingIterator);
            }

            UpdatePanels(rows);
        }

        protected override void Bind(RowOfTransitionsPanel panel, List<ItemData> itemsData)
        {
            panel.SetItems(itemsData);
        }
    }
}