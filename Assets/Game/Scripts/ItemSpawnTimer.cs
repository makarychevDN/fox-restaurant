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

        public void Init(RestaurantEncounter restaurantEncounter)
        {
            restaurantEncounter.Ticker.AddTickable(this);
            timerDisplayer = restaurantEncounter.SlotsManager.SpawningSlots.Last().GetComponentInChildren<TMP_Text>();
        }

        public void Tick(float deltaTime)
        {
            timer += deltaTime;
            if (timer >= spawnInterval)
            {
                timer = 0f;
                spawner.SpawnIngredient();
            }

            timerDisplayer.text = (spawnInterval - timer).ToString();
        }
    }
}