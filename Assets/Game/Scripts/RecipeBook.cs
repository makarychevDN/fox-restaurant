using UnityEngine;
using UnityEngine.EventSystems;

namespace foxRestaurant
{
    public class RecipeBook : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private RecipeBlackBoard recipeBlackBoard;
        [SerializeField] private TransitionsBlackBoard transitionsBlackBoard;
        private Level level;

        public void Init(Level level)
        {
            this.level = level;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            recipeBlackBoard.Appear();
            transitionsBlackBoard.Appear();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            recipeBlackBoard.Disappear();
            transitionsBlackBoard.Disappear();
        }
    }
}