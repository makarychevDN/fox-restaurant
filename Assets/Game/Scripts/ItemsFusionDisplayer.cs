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
        private Item hashedTargetItem;
        private RestaurantEncounter level;
        private Item item;

        public void Init(RestaurantEncounter level, Item item)
        {
            this.level = level;
            this.item = item;
        }

        public void DisplayPlus(Vector3 startPosition, Vector3 endPosition)
        {
            Vector3 center = (endPosition + startPosition) * 0.5f;
            plusIcon.transform.position = center;
        }

        public void DisplayPlus(ItemSlot targetSlot)
        {
            if (targetSlot.Item != hashedTargetItem)
                animator.SetTrigger("appear");
            hashedTargetItem = targetSlot.Item;

            Vector3 resultIconPosition = (transform.position - targetSlot.transform.position) + transform.position;
            Vector3 plusIconPosition = (targetSlot.transform.position + transform.position) * 0.5f;
            Vector3 equalIconPosition = (resultIconPosition + transform.position) * 0.5f;

            plusIcon.transform.position = plusIconPosition;
            equalIcon.transform.position = equalIconPosition;
            resultIcon.transform.position = resultIconPosition;

            resultIcon.sprite = level.RecipesManager.Fuse(item.ItemData, targetSlot.Item.ItemData).Sprite;
            resultIcon.rectTransform.sizeDelta = resultIcon.sprite.GetSpriteSizeInPixels();
        }
    }
}