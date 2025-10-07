using System.Linq;
using TMPro;
using UnityEngine;

namespace foxRestaurant
{
    public class ItemSpawnTimer : MonoBehaviour, ITickable
    {
        [SerializeField] private ItemsSpawner spawner;
        [SerializeField] private float spawnInterval = 5f;
        private float timer;
        private TMP_Text timerDisplayer;
        private RestaurantEncounter restaurantEncounter;

        public void Init(RestaurantEncounter restaurantEncounter)
        {
            this.restaurantEncounter = restaurantEncounter;
            restaurantEncounter.Ticker.AddTickable(this);
            timerDisplayer = restaurantEncounter.SlotsManager.SpawningSlots.Last().GetComponentInChildren<TMP_Text>();
        }

        public void Tick(float deltaTime)
        {
            timer += deltaTime;
            if (timer >= spawnInterval)
            {
                if (restaurantEncounter.SlotsManager.SpawningSlots.Where(slot => slot.Empty).Count() == 0)
                {
                    timerDisplayer.text = "<mspace=1em>ERROR</mspace>";
                    timerDisplayer.color = Extensions.HexToColor("#9c2d2d");
                    return;
                }

                timer = 0f;
                spawner.SpawnIngredient();
            }

            timerDisplayer.text = (spawnInterval - timer).ToString("<mspace=1em>0.0s</mspace>").Replace(',', ':');
            timerDisplayer.color = Extensions.HexToColor("#848f2e");
        }
    }
}