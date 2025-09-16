namespace foxRestaurant
{
    public class Recipe
    {
        private ItemData ingredientA;
        private ItemData ingredientB;
        private ItemData result;

        public ItemData Result => result;

        public Recipe(ItemData result, ItemData ingredientA, ItemData ingredientB)
        {
            this.result = result;
            this.ingredientA = ingredientA;
            this.ingredientB = ingredientB;
        }

        public bool Matches(ItemData a, ItemData b)
        {
            return (ingredientA == a && ingredientB == b) || (ingredientA == b && ingredientB == a);
        }
    }
}