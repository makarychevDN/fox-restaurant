using System.Collections.Generic;
using UnityEngine;

namespace foxRestaurant
{
    public class SeatPlacesManager : MonoBehaviour
    {
        [SerializeField] private List<SeatPlace> seatPlaces;
        public List<SeatPlace> SeatPlaces => seatPlaces;

        public void Init(RestaurantEncounter restaurantEncounter)
        {
            seatPlaces.ForEach(seatPlace => seatPlace.Init(restaurantEncounter));
        }
    }
}