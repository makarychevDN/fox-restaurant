using UnityEngine;

namespace foxRestaurant
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "Scriptable Objects/Recipe")]
    public class Recipe : ScriptableObject
    {
        public ItemData ingredientA;
        public ItemData ingredientB;
        public ItemData result;

        public bool Matches(ItemData a, ItemData b)
        {
            return (ingredientA == a && ingredientB == b) || (ingredientA == b && ingredientB == a);
        }
    }
}