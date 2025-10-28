using System.Collections.Generic;

namespace foxRestaurant
{
    public class DataBase
    {
        private string[,] data;
        private Dictionary<string, ItemData> items;
        private Dictionary<ItemData, ItemData> slicingResults;
        private Dictionary<ItemData, ItemData> fryingResults;
        private List<Recipe> recipes;

        public List<Recipe> Recipes => recipes;
        public Dictionary<ItemData, ItemData> SlicingResults => slicingResults;
        public Dictionary<ItemData, ItemData> FryingResults => fryingResults;
        public Dictionary<string, ItemData> Items => items;

        public DataBase(string[,] data, List<ItemData> allPossibleItemData)
        {
            this.data = data;
            items = new();
            slicingResults = new();
            fryingResults = new();
            recipes = new();

            FillItemsDictionary(allPossibleItemData);
            FindIngredientTransitions();
            FindRecepies();
        }

        private void FillItemsDictionary(List<ItemData> allPossibleItemData)
        {
            foreach (ItemData itemData in allPossibleItemData)
            {
                items.Add(itemData.name, itemData);
            }
        }

        private void FindIngredientTransitions()
        {
            (int, int) startIndexes = data.IndexOf("ingredient transitions (start)");
            (int, int) endIndexes = data.IndexOf("ingredient transitions (end)");

            for (int i = startIndexes.Item1 + 1; i < endIndexes.Item1 - 1; i++)
            {
                if (data[i, 0] != "" && data[i + 1, 0] != "")
                    slicingResults.Add(items[data[i, 0]], items[data[i + 1, 0]]);

                for (int j = 0; j < data.GetLength(1) - 1; j++)
                {
                    if (data[i, j] != "" && data[i, j + 1] != "")
                    {
                        fryingResults.Add(items[data[i, j]], items[data[i, j + 1]]);
                    }
                }
            }
        }

        private void FindRecepies()
        {
            (int, int) startIndexes = data.IndexOf("recipes");

            for(int i = startIndexes.Item1 + 1; i < data.GetLength(0); i++)
            {
                recipes.Add(new Recipe
                (
                    items[data[i, 0]],
                    items[data[i, 1]],
                    items[data[i, 2]]
                ));
            }
        }
    }
}