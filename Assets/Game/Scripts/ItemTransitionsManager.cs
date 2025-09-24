using UnityEngine;

namespace foxRestaurant
{
    public class ItemTransitionsManager : MonoBehaviour
    {
        private RestaurantEncounter restaurantEncounter;

        public void Init(RestaurantEncounter restaurantEncounter)
        {
            this.restaurantEncounter = restaurantEncounter;
        }

        public ItemData GetFryingResult(ItemData itemData)
        {
            restaurantEncounter.DataBase.FryingResults.TryGetValue(itemData, out ItemData fryingResult);
            return fryingResult;
        }

        public ItemData GetSlicingResult(ItemData itemData)
        {
            restaurantEncounter.DataBase.SlicingResults.TryGetValue(itemData, out ItemData slicingResult);
            return slicingResult;
        }
    }
}