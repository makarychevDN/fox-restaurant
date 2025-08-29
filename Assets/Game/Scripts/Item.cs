using DG.Tweening;
using System.Threading.Tasks;
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
        private Level level;

        public void SetSlot(ItemSlot slot) => Slot = slot;
        public Image Image => image;

        public void Init(Level level)
        {
            this.level = level;
            itemMovement.Init(level, this);
            inputController.Init(itemMovement);
        }
    }
}