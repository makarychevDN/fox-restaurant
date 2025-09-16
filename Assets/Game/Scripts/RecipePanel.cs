using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class RecipePanel : MonoBehaviour
    {
        [SerializeField] private Image ingredientA;
        [SerializeField] private Image ingredientB;
        [SerializeField] private Image result;

        public void SetRecipe(Recipe recipe)
        {
            SetImage(ingredientA, recipe.IngredientA);
            SetImage(ingredientB, recipe.IngredientB);
            SetImage(result, recipe.Result);
        }

        private void SetImage(Image image, ItemData itemData)
        {
            image.sprite = itemData.Sprite;
            image.rectTransform.sizeDelta = itemData.Sprite.GetSpriteSizeInPixels() * 0.5f;
        }
    }
}