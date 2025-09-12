namespace foxRestaurant
{
    public class Recipe
    {
        private ItemData ingredientA;
        private ItemData ingredientB;
        private ItemData result;

        public ItemData Result => result;

        public Recipe(ItemData ingredientA, ItemData ingredientB, ItemData result)
        {
            this.ingredientA = ingredientA;
            this.ingredientB = ingredientB;
            this.result = result;
        }

        public bool Matches(ItemData a, ItemData b)
        {
            return (ingredientA == a && ingredientB == b) || (ingredientA == b && ingredientB == a);
        }
    }
}