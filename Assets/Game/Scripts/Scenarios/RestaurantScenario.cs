using UnityEngine;

namespace foxRestaurant
{
    public abstract class RestaurantScenario : MonoBehaviour
    {
        public abstract void Init(RestaurantEncounter restaurantEncounter);
    }
}