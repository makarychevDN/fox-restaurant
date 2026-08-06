using DG.Tweening;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class ItemSpawnTimer : MonoBehaviour, ITickable
    {
        [SerializeField] private ItemsSpawner spawner;
        [SerializeField] private Button spawnButton;
        [SerializeField] private float spawnInterval = 5f;
        [SerializeField] private AudioSource errorSound;
        [SerializeField] private Transform tranformToShake;
        private float timer;
        private TMP_Text timerDisplayer;
        private RestaurantEncounter restaurantEncounter;
        private bool errorDisplayedAlready;
        private bool paused;
        private bool blocked;

        public void Init(RestaurantEncounter restaurantEncounter)
        {
            this.restaurantEncounter = restaurantEncounter;
            restaurantEncounter.Ticker.AddTickable(this);
            timerDisplayer = restaurantEncounter.SlotsManager.SpawnerSlots.Last().GetComponentInChildren<TMP_Text>();
            spawnButton.onClick.AddListener(SkipTimeTillSpawnItem);
            Pause();
        }

        public void Tick(float deltaTime)
        {
            if (blocked)
                return;

            if (paused)
                return;

            timer += deltaTime;
            timer = Mathf.Clamp(timer, 0f, spawnInterval);

            if (timer >= spawnInterval)
            {
                if (restaurantEncounter.SlotsManager.SpawnerSlots.Count(slot => slot.Empty) == 0)
                {
                    if(errorDisplayedAlready)
                        return;

                    timerDisplayer.text = "<mspace=1em>ERROR</mspace>";
                    timerDisplayer.color = Extensions.HexToColor("#9c2d2d");
                    errorSound.Play();
                    errorDisplayedAlready = true;
                    tranformToShake.DOShakeScale(0.1f, 0.25f, 10, 0);
                    return;
                }

                timer = 0f;
                spawner.SpawnIngredient();
                errorDisplayedAlready = false;
            }

            timerDisplayer.text = (spawnInterval - timer).ToString("<mspace=1em>0.0s</mspace>").Replace(',', ':');
            timerDisplayer.color = Extensions.HexToColor("#848f2e");
        }

        public void SkipTimeTillSpawnItem()
        {
            if (blocked || paused)
                return;

            float skippedTime = spawnInterval - timer;
            restaurantEncounter.Ticker.TickAllTickables(skippedTime);
            string skippedTimeString = skippedTime.ToString("0.0s");
            restaurantEncounter.DynamicTextManager.SpawnDynamicText(timerDisplayer.transform.position + Vector3.left * 3, $"{skippedTimeString}\nspent", ReservedColors.YellowUI, timerDisplayer.transform.position + new Vector3(-3, 3, 0));
        }

        public void Pause() => paused = true;
        public void Unpause() => paused = false;
        public void SetBlocked(bool blocked) => this.blocked = blocked;
        public void ResetTimer() => timer = 0f;
    }
}