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
        [SerializeField] private List<ItemData> itemsToCureFever;

        public ICustomerEffectInstance CreateInstance() => new FeverEffectInstance(additionalHungerPounts, itemsToCureFever);
        public GameObject GetViewPrefab() => viewPrefab;
    }

    public class FeverEffectInstance : ICustomerEffectInstance
    {
        private Customer owner;
        private RestaurantEncounter encounter;
        private int additionalHunger;
        private List<ItemData> itemsToCureFever;
        private List<SeatPlace> affectedNeighborSeatPlaces = new();
        private bool effectIsRemoved;

        public event Action OnTriggered;
        public event Action OnEffectIsCured;
        public event Action<Customer> OnTriggeredOnCertainCustomer;

        public FeverEffectInstance(int additionalHunger, List<ItemData> itemsToCureFever)
        {
            this.additionalHunger = additionalHunger;
            this.itemsToCureFever = itemsToCureFever;
        }

        public void Apply(Customer customer, RestaurantEncounter encounter)
        {
            owner = customer;
            this.encounter = encounter;

            GetNeighborSeatPlaces();
            ApplyEffectToNeighborSeatPlaces();
            owner.OnAteCertainFood.AddListener(AteFoodHandler);
            owner.OnStartLeavingProcess.AddListener(RemoveEffect);
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

        private void ApplyEffectToNeighborSeatPlaces()
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
            OnTriggeredOnCertainCustomer.Invoke(neighbor);
        }

        private SeatPlace TryToGetNeigborSeatPlaceByIndex(Customer owner, int index)
        {
            if (index < 0 || index >= owner.SeatPlace.Table.SeatPlaces.Count)
                return null;

            return owner.SeatPlace.Table.SeatPlaces[index];
        }

        private void AteFoodHandler(ItemData itemData)
        {
            if (!itemsToCureFever.Contains(itemData))
                return;

            RemoveEffect();
        }

        private void RemoveEffect()
        {
            if (effectIsRemoved)
                return;

            effectIsRemoved = true;
            OnEffectIsCured.Invoke();

            foreach (SeatPlace seatPlace in affectedNeighborSeatPlaces)
            {
                seatPlace.OnCustomerSatDown.RemoveListener(ApplyHunger);

                if (seatPlace.Customer != null)
                    seatPlace.Customer.GetSatiety(1);
            }
        }
    }
}