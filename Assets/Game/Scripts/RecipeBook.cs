using UnityEngine;
using UnityEngine.EventSystems;

namespace foxRestaurant
{
    public class RecipeBook : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private RecipeBlackBoard recipeBlackBoard;
        [SerializeField] private GameObject fringBlackBoard;
        private Level level;

        public void Init(Level level)
        {
            this.level = level;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            recipeBlackBoard.Appear();
            fringBlackBoard.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            recipeBlackBoard.Disappear();
            fringBlackBoard.SetActive(false);
        }
    }
}