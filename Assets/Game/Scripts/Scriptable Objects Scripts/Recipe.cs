using UnityEngine;

namespace foxRestaurant
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "Scriptable Objects/Recipe")]
    public class Recipe : ScriptableObject
    {
        [field: SerializeField] public ItemData IngredientA { get; private set; }
        [field: SerializeField] public ItemData IngredientB { get; private set; }
        [field: SerializeField] public ItemData Result { get; private set; }

        public bool Matches(ItemData a, ItemData b)
        {
            return (IngredientA == a && IngredientB == b) || (IngredientA == b && IngredientB == a);
        }
    }
}