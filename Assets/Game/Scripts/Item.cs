using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class Item : MonoBehaviour
    {
        [field: SerializeField] public ItemSlot Slot { get; set; }

        [SerializeField] private Image image;
        [SerializeField] private ItemMovement itemMovement;
        [SerializeField] private ItemMouseInputController inputController;
        [SerializeField] private float timeToFry = 5f;
        private float fryingTimer;
        private Level level;

        public void SetSlot(ItemSlot slot) => Slot = slot;
        public Image Image => image;
        public float FryingTimer => fryingTimer;

        public void Init(Level level)
        {
            this.level = level;
            itemMovement.Init(level, this);
            inputController.Init(itemMovement);
        }

        public void Fry(float time)
        {
            fryingTimer += time;

            if(fryingTimer >= timeToFry)
            {
                fryingTimer = 0;
                print("fried!");
            }
        }
    }
}