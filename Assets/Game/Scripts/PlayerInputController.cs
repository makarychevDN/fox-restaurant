using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private Button spawnIngredientButton;
        [SerializeField] private RecipeBook recipeBook;

        public void Init(RestaurantEncounter level)
        {
            spawnIngredientButton.onClick.AddListener(level.ItemsSpawner.SpawnIngredient);
            recipeBook.Init(level);
        }
    }
}