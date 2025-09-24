using UnityEngine;
using UnityEngine.EventSystems;

namespace foxRestaurant
{
    public class RecipeBook : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private RecipeBlackBoard recipeBlackBoard;
        [SerializeField] private TransitionsBlackBoard transitionsBlackBoard;
        [SerializeField] private AudioSource openBookSound;
        [SerializeField] private AudioSource closeBookSound;
        private RestaurantEncounter level;

        public void Init(RestaurantEncounter level)
        {
            this.level = level;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            recipeBlackBoard.Appear();
            transitionsBlackBoard.Appear();
            openBookSound.Play();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            recipeBlackBoard.Disappear();
            transitionsBlackBoard.Disappear();
            closeBookSound.Play();
        }
    }
}