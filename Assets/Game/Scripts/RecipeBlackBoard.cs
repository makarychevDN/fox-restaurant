using System.Collections.Generic;
using UnityEngine;

namespace foxRestaurant
{
    public class RecipeBlackBoard : MonoBehaviour
    {
        [SerializeField] private Transform parentForPanels;
        [SerializeField] private RecipePanel recipePanelPrefab;
        private List<RecipePanel> recipePanels = new List<RecipePanel>();

        public void Init(Level level)
        {
            level.OnDataBaseUpdated.AddListener(UpdatePanels);
        }

        public void UpdatePanels(DataBase dataBase)
        {
            int panelsCount = Mathf.Max(dataBase.Recipes.Count, recipePanels.Count);

            for (int i = 0; i < panelsCount; i++)
            {
                if (i < dataBase.Recipes.Count)
                {
                    var panel = GetRecipePanel(i);
                    panel.SetRecipe(dataBase.Recipes[i]);
                    panel.gameObject.SetActive(true);
                }
                else
                {
                    recipePanels[i].gameObject.SetActive(false);
                }
            }
        }

        private RecipePanel GetRecipePanel(int index)
        {
            return recipePanels.Count > index ? recipePanels[index] : SpawnRecipePanel();
        }

        private RecipePanel SpawnRecipePanel()
        {
            var recipePanel = Instantiate(recipePanelPrefab);
            recipePanel.transform.parent = parentForPanels;
            recipePanel.transform.localScale = Vector3.one;

            recipePanels.Add(recipePanel);

            return recipePanel;
        }
    }
}