using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace foxRestaurant
{
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField] private Customer customerPrefab;
        [SerializeField] private AudioSource customerCameSound;
        private RestaurantEncounter restaurantEncounter;

        public UnityEvent OnCustomerSpawningRejected;

        public void Init(RestaurantEncounter restaurantEncounter)
        {
            this.restaurantEncounter = restaurantEncounter;
        }

        public Customer SpawnCustomer(SeatPlace seatPlace, CustomerData customerData, Func<ItemData> getItemDataToOrderFunc)
        {
            customerCameSound.Play();

            var customer = Instantiate(customerPrefab);
            customer.CenterOnNewParent(seatPlace.transform);
            customer.Init(restaurantEncounter, seatPlace, customerData, getItemDataToOrderFunc);
            seatPlace.SetCustomer(customer);
            return customer;
        }

        public Customer TryToSpawnCustomer(CustomerData customerData, Func<ItemData> getItemDataToOrderFunc)
        {
            var seatPlaceToSpawn = 
                restaurantEncounter.SeatPlacesManager.SeatPlaces.Where(
                    seatPlace => !seatPlace.IsTaken && seatPlace.gameObject.activeSelf).ToList().GetRandomElement();

            if (seatPlaceToSpawn == null)
                return null;

            var customer = SpawnCustomer(
                seatPlaceToSpawn, 
                customerData,
                getItemDataToOrderFunc
            );

            return customer;
        }

        public bool IsPossibleToSpawnCustomer => restaurantEncounter.SeatPlacesManager.SeatPlaces.Any(seatPlace => !seatPlace.IsTaken);
    }
}