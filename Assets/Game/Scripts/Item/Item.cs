using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class Item : MonoBehaviour
    {
        [field: Header("Dynamic Links")]
        [field: SerializeField] public ItemSlot Slot { get; set; }
        [field: SerializeField] public ItemData ItemData { get; private set; }

        [field: Header("Setup")]
        [field: SerializeField] public Image Image { get; private set; }
        [SerializeField] private ItemStateController itemStateController;
        [SerializeField] private ItemMouseInputController inputController;
        [SerializeField] private ParticleSystem poofParticles;
        [SerializeField] private AudioSource appearSound;
        [SerializeField] private ItemType itemType;

        protected RestaurantEncounter restaurantEncounter;

        public ItemType ItemType => itemType;
        public ItemStateController ItemStateController => itemStateController;

        public virtual void Init(RestaurantEncounter restaurantEncounter, ItemData itemData, int satiety)
        {
            this.restaurantEncounter = restaurantEncounter;
            itemStateController.Init(restaurantEncounter, this);
            inputController.Init(itemStateController, this);
            SetItemData(itemData);
            appearSound.pitch = Random.Range(0.7f, 1.3f);
        }

        public void SetItemData(ItemData itemData)
        {
            ItemData = itemData;
            Image.sprite = itemData.Sprite;
            Image.rectTransform.sizeDelta = itemData.Sprite.GetSpriteSizeInPixels();
        }

        public void PlayPoofParticles()
        {
            poofParticles.Play();
        }
    }

    public enum ItemType
    {
        Food = 5,
        Customer = 9
    }
}