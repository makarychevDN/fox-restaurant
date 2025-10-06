using UnityEngine;

namespace foxRestaurant
{
    public class ItemSpawnTimer : MonoBehaviour, ITickable
    {
        [SerializeField] private ItemsSpawner spawner;
        [SerializeField] private float spawnInterval = 5f;
        private float timer;

        public void Init(RestaurantEncounter restaurantEncounter)
        {
            restaurantEncounter.Ticker.AddTickable(this);
        }

        public void Tick(float deltaTime)
        {
            timer += deltaTime;
            if (timer >= spawnInterval)
            {
                timer = 0f;
                spawner.SpawnIngredient();
            }
        }
    }
}