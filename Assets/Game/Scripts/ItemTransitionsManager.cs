using UnityEngine;

namespace foxRestaurant
{
    public class ItemTransitionsManager : MonoBehaviour
    {
        private Level level;

        public void Init(Level level)
        {
            this.level = level;
        }

        public ItemData GetFryingResult(ItemData itemData)
        {
            level.DataBase.FryingResults.TryGetValue(itemData, out ItemData fryingResult);
            return fryingResult;
        }

        public ItemData GetSlicingResult(ItemData itemData)
        {
            level.DataBase.SlicingResults.TryGetValue(itemData, out ItemData slicingResult);
            return slicingResult;
        }
    }
}