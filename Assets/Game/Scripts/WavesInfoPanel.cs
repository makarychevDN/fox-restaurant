using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class WavesInfoPanel : MonoBehaviour
    {
        [SerializeField] private Image nextCustomerImage;
        [SerializeField] private TMP_Text nextCustomersPatience;
        [SerializeField] private TMP_Text customersCount;
        [SerializeField] private GameObject patienceInfoParent;
        [SerializeField] private GameObject noMoreCustomersInWave;
        [SerializeField] private ParticleSystem poofParticles;
        private RestaurantEncounter restaurantEncounter;

        public void Init(RestaurantEncounter restaurantEncounter)
        {
            this.restaurantEncounter = restaurantEncounter;
            restaurantEncounter.CurrentWaveManager.OnNextCustomerUpdated.AddListener(UpdateNextCustomerImage);
            restaurantEncounter.CurrentWaveManager.OnNextCustomersPatienceUpdated.AddListener(UpdatePatience);
            restaurantEncounter.CurrentWaveManager.OnFedCustomersCountUpdated.AddListener(UpdateFedCustomersCount);
        }

        private void UpdateNextCustomerImage(CustomerData nextCustomerData)
        {
            poofParticles.Play();

            bool thereIsNextCustomer = nextCustomerData != null;
            nextCustomerImage.gameObject.SetActive(thereIsNextCustomer);
            patienceInfoParent.SetActive(thereIsNextCustomer);
            noMoreCustomersInWave.SetActive(!thereIsNextCustomer);

            if (!thereIsNextCustomer)
                return;

            nextCustomerImage.sprite = nextCustomerData.Sprite;
            nextCustomerImage.rectTransform.sizeDelta = nextCustomerData.Sprite.GetSpriteSizeInPixels() * 0.5f;
        }

        private void UpdatePatience(float time)
        {
            nextCustomersPatience.text = time.ToString("0");
        }

        private async void UpdateFedCustomersCount(int count, int toFeed)
        {
            customersCount.text = $"{count} / {toFeed}";
            customersCount.transform.localScale = Vector3.one;
            await customersCount.transform.DOScale(2, 0.1f).AsyncWaitForCompletion();
            customersCount.transform.DOScale(1, 0.1f);
        }
    }
}