using DG.Tweening;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

namespace foxRestaurant
{
    public class GarbageCan : MonoBehaviour, ITickable
    {
        [Header("garbage can functionality")]
        [SerializeField] private ItemSlot slot;
        [SerializeField] private ParticleSystem itemDisappearParticles;

        [Header("oven functionality")]
        [SerializeField] private GameObject ovenPartsParent;
        [SerializeField] private bool ovenModeOn;
        [SerializeField] private float timeToSpawnPretzel;
        [SerializeField] private ItemData pretzelData;
        [SerializeField] private ItemData coaldata;
        [SerializeField] private AudioSource errorSound;
        [SerializeField] private Transform tranformToShake;
        [SerializeField] private TMP_Text timerDisplayer;
        [SerializeField] private ItemSlot slotToSpawnPretzel;

        private float pretzelSpawnTimer;
        private bool paused;
        private bool blocked;
        private bool errorDisplayedAlready;
        private RestaurantEncounter restaurantEncounter;

        public void Init(RestaurantEncounter restaurantEncounter)
        {
            this.restaurantEncounter = restaurantEncounter;
            restaurantEncounter.Ticker.AddTickable(this);
            slot.OnItemHasBeenPlaced.AddListener(ItemPlacedInGarbageCanHandler);
            Pause();
            ovenPartsParent.SetActive(ovenModeOn);
            blocked = !ovenModeOn;
        }

        private void ItemPlacedInGarbageCanHandler(Item item)
        {
            if (ovenModeOn)
            {
                int multiplier = item.ItemData == coaldata ? 4 : 1;
                float tickValue = multiplier * (item as FoodItem).Satiety;
                Tick(tickValue);
                restaurantEncounter.DynamicTextManager.SpawnDynamicText(transform.position + Vector3.right * 3, $"-{tickValue} sec", ReservedColors.YellowUI, transform.position + Vector3.one * 3);
            }

            itemDisappearParticles.Play();
            slot.Clear();
        }

        public void Tick(float deltaTime)
        {
            if (blocked)
                return;

            if (paused)
                return;

            pretzelSpawnTimer += deltaTime;
            if (pretzelSpawnTimer >= timeToSpawnPretzel)
            {
                if (restaurantEncounter.SlotsManager.PretzelSlots.Count(slot => slot.Empty) == 0)
                {
                    if (errorDisplayedAlready)
                        return;

                    timerDisplayer.text = "<mspace=1em>ERROR</mspace>";
                    timerDisplayer.color = Extensions.HexToColor("#9c2d2d");
                    errorSound.Play();
                    errorDisplayedAlready = true;
                    tranformToShake.DOShakeScale(0.1f, 0.25f, 10, 0);
                    return;
                }

                pretzelSpawnTimer = 0f;
                SpawnPretzel();
                errorDisplayedAlready = false;
            }

            timerDisplayer.text = (timeToSpawnPretzel - pretzelSpawnTimer).ToString("<mspace=1em>0.0s</mspace>").Replace(',', ':');
            timerDisplayer.color = Extensions.HexToColor("#848f2e");
        }

        public void SpawnPretzel()
        {
            restaurantEncounter.ItemsSpawner.SpawnFoodItem(restaurantEncounter, pretzelData, slotToSpawnPretzel);
        }

        public void Pause() => paused = true;
        public void Unpause() => paused = false;
        public void SetBlocked(bool blocked) => this.blocked = blocked;
        public void ResetTimer() => pretzelSpawnTimer = 0f;
    }
}