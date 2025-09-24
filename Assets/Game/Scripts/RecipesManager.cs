using System.Linq;
using UnityEngine;

namespace foxRestaurant
{
    public class RecipesManager : MonoBehaviour
    {
        [SerializeField] private ItemData coal;
        private RestaurantEncounter level;

        public void Init(RestaurantEncounter level)
        {
            this.level = level;
        }

        public ItemData Fuse(ItemData itemData1, ItemData itemData2)
        {
            var matchesResipe = level.DataBase.Recipes.FirstOrDefault(r => r.Matches(itemData1, itemData2));
            var fusionResult = matchesResipe == null ? coal : matchesResipe.Result;
            return fusionResult;
        }
    }
}