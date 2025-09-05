using System.Collections.Generic;
using UnityEngine;

namespace foxRestaurant
{
    [CreateAssetMenu(fileName = "RecipesList", menuName = "Scriptable Objects/RecipesList")]
    public class RecipesList : ScriptableObject
    {
        [field: SerializeField] public List<ItemData> Recipes { get; private set; }
    }
}