using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace foxRestaurant
{
    public class SeatPlacesManager : MonoBehaviour
    {
        [SerializeField] private List<SeatPlace> seatPlaces;
        [SerializeField] private List<Table> tables;
        public List<SeatPlace> SeatPlaces => seatPlaces;
        public List<SeatPlace> FreeSeatPlaces => seatPlaces.Where(seatplace => !seatplace.IsTaken).ToList();

        public void Init(RestaurantEncounter restaurantEncounter)
        {
            foreach(var table in tables)
            {
                foreach(var seatPlace in table.SeatPlaces)
                {
                    seatPlaces.Add(seatPlace);
                    seatPlace.Init(restaurantEncounter, table);
                }
            }
        }
    }
}