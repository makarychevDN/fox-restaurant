using UnityEngine;

namespace foxRestaurant
{
    public class GarbageCan : MonoBehaviour
    {
        [SerializeField] private ItemSlot slot;
        [SerializeField] private ParticleSystem itemDisappearParticles;

        private void Awake()
        {
            slot.OnHasBeenOccupied.AddListener(ItemThrownHandler);
        }

        private void ItemThrownHandler()
        {
            itemDisappearParticles.Play();
            slot.Clear();
        }
    }
}