using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class WavesInfoPanel : MonoBehaviour
    {
        [SerializeField] private GameObject segmentPrefab;
        [SerializeField] private Transform parentForSegments;
        [SerializeField] private Transform customersToFeedPanel;
        [SerializeField] private Image nextCustomerImage;
        [SerializeField] private TMP_Text nextCustomersPatience;
        [SerializeField] private TMP_Text customersCount;
        [SerializeField] private GameObject patienceInfoParent;
        [SerializeField] private GameObject noMoreCustomersInWave;
        [SerializeField] private ParticleSystem poofParticles;
        [SerializeField] private List<RectTransform> contentSizeFitters;
        private RestaurantEncounter restaurantEncounter;
        private List<GameObject> segments = new();

        public void Init(RestaurantEncounter restaurantEncounter)
        {
            this.restaurantEncounter = restaurantEncounter;
            restaurantEncounter.CurrentWaveManager.OnNextCustomerUpdated.AddListener(UpdateNextCustomerImage);
            restaurantEncounter.CurrentWaveManager.OnNextCustomersPatienceUpdated.AddListener(UpdatePatience);
            restaurantEncounter.CurrentWaveManager.OnCustomersToFeedCountUpdated.AddListener(UpdateSegments);
            restaurantEncounter.CurrentWaveManager.OnFedCustomersCountUpdated.AddListener(UpdateCheckmarks);
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


        private void UpdateSegments(int newSegmentsCount)
        {
            int segmentsToSpawnCount = newSegmentsCount - segments.Count;
            while (segmentsToSpawnCount > 0)
            {
                segments.Add(Instantiate(segmentPrefab, parentForSegments));
                segmentsToSpawnCount--;
            }

            for (int i = 0; i < segments.Count; i++)
            {
                segments[i].SetActive(i < newSegmentsCount);
            }

            RebuildContentSizeFitters();
            ShakeSegmentsParent();
        }

        private async Task ShakeSegmentsParent()
        {
            await customersToFeedPanel.DOScale(2, 0.05f).AsyncWaitForCompletion();
            customersToFeedPanel.DOScale(1, 0.05f);
        }

        public void RebuildContentSizeFitters()
        {
            foreach (var fitter in contentSizeFitters)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(fitter);
            }
        }

        private void UpdateCheckmarks(int checkMarksCount)
        {
            if (checkMarksCount > segments.Count(s => s.activeSelf))
                return;

            for (int i = 0; i < segments.Count; i++)
            {
                segments[i].transform.GetChild(0).gameObject.SetActive(i < checkMarksCount);
            }

            if(checkMarksCount > 0)
                ShakeCheckmark(segments[checkMarksCount - 1].transform.GetChild(0).transform);
        }

        private async Task ShakeCheckmark(Transform checkmark)
        {
            await checkmark.DOScale(2, 0.1f).AsyncWaitForCompletion();
            checkmark.DOScale(1, 0.1f);
        }
    }
}