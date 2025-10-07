using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private RecipeBook recipeBook;

        public void Init(RestaurantEncounter restaurantEncounter)
        {
            recipeBook.Init(restaurantEncounter);
        }
    }
}