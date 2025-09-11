using UnityEngine;
using UnityEngine.EventSystems;

namespace foxRestaurant
{
    public class RecipeBook : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject combinationsBlackBoard;
        [SerializeField] private GameObject fringBlackBoard;
        private Level level;

        public void Init(Level level)
        {
            this.level = level;
            //level.RecipesManager.RecipeList.Recipes.ForEach(recipe =>)
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            combinationsBlackBoard.SetActive(true);
            fringBlackBoard.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            combinationsBlackBoard.SetActive(false);
            fringBlackBoard.SetActive(false);
        }
    }
}