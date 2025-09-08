using foxRestaurant;
using System.Linq;
using UnityEngine;

public class RecipesManager : MonoBehaviour
{
    [SerializeField] private RecipesList recipesList;
    [SerializeField] private ItemData coal;

    public ItemData Fuse(ItemData itemData1, ItemData itemData2)
    {
        var matchesResipe = recipesList.Recipes.FirstOrDefault(r => r.Matches(itemData1, itemData2));
        var fusionResult = matchesResipe == null ? coal : matchesResipe.Result;
        return fusionResult;
    }
}
