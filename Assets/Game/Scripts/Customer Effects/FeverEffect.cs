using System;
using System.Collections.Generic;
using UnityEngine;

namespace foxRestaurant
{
    [CreateAssetMenu(fileName = "fever effect", menuName = "Scriptable Objects/Customer Effects/fever effect")]
    public class FeverEffect : ScriptableObject, ICustomerEffect, IAbleToReturnViewPrefab
    {
        [SerializeField] private GameObject viewPrefab;
        [SerializeField] private int additionalHungerPoints = 1;
        [SerializeField] private List<ItemData> itemsToCureFever;

        public ICustomerEffectInstance CreateInstance() => new FeverEffectInstance(additionalHungerPoints, itemsToCureFever);
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
        private bool feverIsCured;

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
            owner.OnStartLeavingProcessSatisfied.AddListener(OnStartLeavingSatisfiedProcessHandler);
            owner.OnLeft.AddListener(RemoveEffect);
            owner.OnLeft.AddListener(RemoveListeners);
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

                if (seatPlace.Customer != null)
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

            CureFever();
        }

        private void OnStartLeavingSatisfiedProcessHandler(bool isSatisfied)
        {
            if (isSatisfied)
                CureFever();
        }

        private void CureFever()
        {
            if (feverIsCured)
                return;

            feverIsCured = true;
            OnEffectIsCured.Invoke();
            RemoveEffect();
        }

        private void RemoveEffect()
        {
            if (effectIsRemoved)
                return;

            foreach (SeatPlace seatPlace in affectedNeighborSeatPlaces)
            {
                seatPlace.OnCustomerSatDown.RemoveListener(ApplyHunger);

                if (seatPlace.Customer != null)
                    seatPlace.Customer.RemoveHunger(additionalHunger);
            }

            effectIsRemoved = true;
        }

        private void RemoveListeners()
        {
            owner.OnAteCertainFood.RemoveListener(AteFoodHandler);
            owner.OnStartLeavingProcessSatisfied.RemoveListener(OnStartLeavingSatisfiedProcessHandler);
            owner.OnLeft.RemoveListener(RemoveEffect);
            owner.OnLeft.RemoveListener(RemoveListeners);
        }
    }
}