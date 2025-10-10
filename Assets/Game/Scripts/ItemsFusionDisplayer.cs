using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class ItemsFusionDisplayer : MonoBehaviour
    {
        [SerializeField] private GameObject plusIcon;
        [SerializeField] private GameObject equalIcon;
        [SerializeField] private Image resultIcon;
        [SerializeField] private Sprite redCross;
        [SerializeField] private Animator animator;
        private FoodItemExtension hashedTarget;
        private RestaurantEncounter restaurantEncounter;
        private FoodItemExtension foodItemExtension;

        public void Init(RestaurantEncounter restaurantEncounter, FoodItemExtension foodItemExtension)
        {
            this.restaurantEncounter = restaurantEncounter;
            this.foodItemExtension = foodItemExtension;
        }

        public void DisplayPlus(Vector3 startPosition, Vector3 endPosition)
        {
            Vector3 center = (endPosition + startPosition) * 0.5f;
            plusIcon.transform.position = center;
        }

        public void DisplayPlus(ItemSlot targetSlot)
        {
            if (targetSlot.FoodItemExtension != hashedTarget)
                animator.SetTrigger("appear");
            hashedTarget = targetSlot.FoodItemExtension;

            Vector3 resultIconPosition = (transform.position - targetSlot.transform.position) + transform.position;
            Vector3 plusIconPosition = (targetSlot.transform.position + transform.position) * 0.5f;
            Vector3 equalIconPosition = (resultIconPosition + transform.position) * 0.5f;

            plusIcon.transform.position = plusIconPosition;
            equalIcon.transform.position = equalIconPosition;
            resultIcon.transform.position = resultIconPosition;

            resultIcon.sprite = restaurantEncounter.RecipesManager.Fuse(foodItemExtension.ItemData, targetSlot.FoodItemExtension.ItemData).Sprite;
            resultIcon.rectTransform.sizeDelta = resultIcon.sprite.GetSpriteSizeInPixels();
        }
    }
}