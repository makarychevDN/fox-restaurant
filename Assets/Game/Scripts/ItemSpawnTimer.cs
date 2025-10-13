using DG.Tweening;
using System.Linq;
using TMPro;
using UnityEngine;

namespace foxRestaurant
{
    public class ItemSpawnTimer : MonoBehaviour, ITickable
    {
        [SerializeField] private ItemsSpawner spawner;
        [SerializeField] private float spawnInterval = 5f;
        [SerializeField] private AudioSource errorSound;
        [SerializeField] private Transform tranformToShake;
        private float timer;
        private TMP_Text timerDisplayer;
        private RestaurantEncounter restaurantEncounter;
        private bool errorDisplayedAlready;

        public void Init(RestaurantEncounter restaurantEncounter)
        {
            this.restaurantEncounter = restaurantEncounter;
            restaurantEncounter.Ticker.AddTickable(this);
            timerDisplayer = restaurantEncounter.SlotsManager.FoodSpawningSlots.Last().GetComponentInChildren<TMP_Text>();
        }

        public void Tick(float deltaTime)
        {
            timer += deltaTime;
            if (timer >= spawnInterval)
            {
                if (restaurantEncounter.SlotsManager.FoodSpawningSlots.Where(slot => slot.Empty).Count() == 0)
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
    }
}