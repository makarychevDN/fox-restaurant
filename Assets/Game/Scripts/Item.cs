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
        [SerializeField] private FoodItemExtension foodItemExtension;
        private RestaurantEncounter restaurantEncounter;

        public ItemType ItemType => itemType;
        public FoodItemExtension FoodItemExtension => foodItemExtension;

        public void Init(RestaurantEncounter restaurantEncounter, ItemData itemData, int satiety)
        {
            this.restaurantEncounter = restaurantEncounter;
            itemStateController.Init(restaurantEncounter, this, foodItemExtension == null ? null : foodItemExtension.FusionDisplayer);
            inputController.Init(itemStateController, this);
            SetItemData(itemData);
            appearSound.pitch = Random.Range(0.7f, 1.3f);

            if (FoodItemExtension != null)
            {
                foodItemExtension.Init(this, restaurantEncounter);
                foodItemExtension.SetSatiety(satiety);
            }
        }

        public void SetItemData(ItemData itemData)
        {
            ItemData = itemData;
            Image.sprite = itemData.Sprite;
            Image.rectTransform.sizeDelta = itemData.Sprite.GetSpriteSizeInPixels();
        }

        public void Fry(float time)
        {
            if(foodItemExtension != null)
                foodItemExtension.Fry(time);
        }

        public void Slice()
        {
            if (foodItemExtension != null)
                foodItemExtension.Slice();
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