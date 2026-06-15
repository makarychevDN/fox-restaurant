using System;
using System.Collections.Generic;
using UnityEngine;

namespace foxRestaurant
{
    [CreateAssetMenu(fileName = "fever effect", menuName = "Scriptable Objects/Customer Effects/fever effect")]
    public class FeverEffect : ScriptableObject, ICustomerEffect, IAbleToReturnViewPrefab
    {
        [SerializeField] private GameObject viewPrefab;
        [SerializeField] private int additionalHungerPounts = 10;

        public ICustomerEffectInstance CreateInstance() => new FeverEffectInstance(additionalHungerPounts);
        public GameObject GetViewPrefab() => viewPrefab;
    }

    public class FeverEffectInstance : ICustomerEffectInstance
    {
        private Customer owner;
        private RestaurantEncounter encounter;
        private int additionalHunger;
        private List<SeatPlace> affectedNeighborSeatPlaces = new();

        public event Action OnTriggered;
        public event Action<Customer> OnTriggeredOnCertainCustomer;

        public FeverEffectInstance(int additionalHunger)
        {
            this.additionalHunger = additionalHunger;
        }

        public void Apply(Customer customer, RestaurantEncounter encounter)
        {
            owner = customer;
            this.encounter = encounter;

            GetNeighborSeatPlaces();
            AddListenersToNeighborSeatPlaces();
            owner.OnStartLeavingProcess.AddListener(RemoveListenres);
        }

        private void GetNeighborSeatPlaces()
        {
            int ownerIndex = owner.SeatPlace.Table.SeatPlaces.IndexOf(owner.SeatPlace);
            SeatPlace leftNeighbor = TryToGetNeigborSeatPlaceByIndex(owner, ownerIndex - 1);
            SeatPlace rightNeighbor = TryToGetNeigborSeatPlaceByIndex(owner, ownerIndex + 1);
            if (leftNeighbor != null)
                affectedNeighborSeatPlaces.Add(leftNeighbor);
            if (rightNeighbor != null)
                affectedNeighborSeatPlaces.Add(rightNeighbor);
        }

        private void AddListenersToNeighborSeatPlaces()
        {
            foreach (SeatPlace seatPlace in affectedNeighborSeatPlaces)
            {
                seatPlace.OnCustomerSatDown.AddListener(ApplyHunger);

                if(seatPlace.Customer != null)
                    ApplyHunger(seatPlace.Customer);
            }
        }

        private void ApplyHunger(Customer neighbor)
        {
            neighbor.AddHunger(additionalHunger);

            Debug.Log(OnTriggeredOnCertainCustomer);
            Debug.Log(neighbor);

            OnTriggeredOnCertainCustomer.Invoke(neighbor);
        }

        private SeatPlace TryToGetNeigborSeatPlaceByIndex(Customer owner, int index)
        {
            if (index < 0 || index >= owner.SeatPlace.Table.SeatPlaces.Count)
                return null;

            return owner.SeatPlace.Table.SeatPlaces[index];
        }

        private void RemoveListenres()
        {
            foreach (SeatPlace seatPlace in affectedNeighborSeatPlaces)
            {
                seatPlace.OnCustomerSatDown.RemoveListener(ApplyHunger);
            }
        }
    }
}