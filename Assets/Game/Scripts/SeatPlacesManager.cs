using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace foxRestaurant
{
    public class SeatPlacesManager : MonoBehaviour
    {
        [SerializeField] private List<SeatPlace> seatPlaces;
        public List<SeatPlace> SeatPlaces => seatPlaces;
        public List<SeatPlace> FreeSeatPlaces => seatPlaces.Where(seatplace => !seatplace.IsTaken).ToList();

        public void Init(RestaurantEncounter restaurantEncounter)
        {
            seatPlaces.ForEach(seatPlace => seatPlace.Init(restaurantEncounter));
        }
    }
}