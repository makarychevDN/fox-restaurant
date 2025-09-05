using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class ItemsFusionDisplayer : MonoBehaviour
    {
        [SerializeField] private GameObject plusIcon;
        [SerializeField] private GameObject equalIcon;
        [SerializeField] private Image resultIcon;
        [SerializeField] private Animator animator;
        private Item hashedTargetItem;

        public void DisplayPlus(Vector3 startPosition, Vector3 endPosition)
        {
            Vector3 center = (endPosition + startPosition) * 0.5f;
            plusIcon.transform.position = center;
        }

        public void DisplayPlus(ItemSlot itemSlot)
        {
            if (itemSlot.Item != hashedTargetItem)
                animator.SetTrigger("appear");
            hashedTargetItem = itemSlot.Item;

            Vector3 resultIconPosition = (transform.position - itemSlot.transform.position) + transform.position;
            Vector3 plusIconPosition = (itemSlot.transform.position + transform.position) * 0.5f;
            Vector3 equalIconPosition = (resultIconPosition + transform.position) * 0.5f;

            plusIcon.transform.position = plusIconPosition;
            equalIcon.transform.position = equalIconPosition;
            resultIcon.transform.position = resultIconPosition;
        }
    }
}