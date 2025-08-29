using UnityEngine;

namespace foxRestaurant
{
    public class Cooker : MonoBehaviour, ITickable
    {
        private ItemSlot slot;
        private Item item;

        public void Init(ItemSlot slot)
        {
            this.slot = slot;
        }

        public void Tick(float deltaTime)
        {
            if (slot.Item == null)
                return;

            slot.Item.Fry(deltaTime);
        }

        private void Update()
        {
            Tick(Time.deltaTime);
        }
    }
}