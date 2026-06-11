using System.Collections.Generic;
using UnityEngine;

namespace foxRestaurant
{
    public class Table : MonoBehaviour
    {
        [SerializeField] private List<SeatPlace> seatPlaces;

        public List<SeatPlace> SeatPlaces => seatPlaces;
    }
}