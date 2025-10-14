using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace foxRestaurant
{
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField] private Customer customerPrefab;
        [SerializeField] private List<SeatPlace> seatPlaces;
        [SerializeField] private AudioSource customerCameSound;
        private RestaurantEncounter restaurantEncounter;

        public UnityEvent OnCustomerSpawningRejected;

        public void Init(RestaurantEncounter restaurantEncounter)
        {
            this.restaurantEncounter = restaurantEncounter;
            seatPlaces.ForEach(seatPlace => seatPlace.Init(restaurantEncounter));
        }

        public Customer SpawnCustomer(SeatPlace seatPlace, CustomerData customerData, Func<ItemData> getItemDataToOrderFunc)
        {
            customerCameSound.Play();

            var customer = Instantiate(customerPrefab);
            customer.transform.parent = seatPlace.transform;
            customer.transform.localPosition = Vector3.zero;
            customer.transform.localScale = Vector3.one;
            customer.transform.localRotation = Quaternion.identity;
            customer.Init(restaurantEncounter, customerData, getItemDataToOrderFunc);
            return customer;
        }

        public Customer TryToSpawnCustomer(CustomerData customerData, Func<ItemData> getItemDataToOrderFunc)
        {
            var seatPlaceToSpawn = 
                seatPlaces.Where(seatPlace => !seatPlace.IsTaken).ToList().GetRandomElement();

            if (seatPlaceToSpawn == null)
                return null;

            var customer = SpawnCustomer(
                seatPlaceToSpawn, 
                customerData,
                getItemDataToOrderFunc
            );

            return customer;
        }
    }
}