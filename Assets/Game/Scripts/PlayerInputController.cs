using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private Button spawnIngredientButton;
        [SerializeField] private RecipeBook recipeBook;

        public void Init(RestaurantEncounter restaurantEncounter)
        {
            spawnIngredientButton.onClick.AddListener(restaurantEncounter.ItemsSpawner.SpawnIngredient);
            recipeBook.Init(restaurantEncounter);
        }
    }
}