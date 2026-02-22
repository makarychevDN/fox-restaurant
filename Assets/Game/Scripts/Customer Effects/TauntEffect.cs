using System;
using UnityEngine;

namespace foxRestaurant
{
    [CreateAssetMenu(fileName = "taunt effect", menuName = "Scriptable Objects/Customer Effects/taunt effect")]
    public class TauntEffect : ScriptableObject, ICustomerEffect, IAbleToReturnViewPrefab
    {
        [SerializeField] private float cooldown;
        [SerializeField] private GameObject viewPrefab;

        public ICustomerEffectInstance CreateInstance()
        {
            return new TauntEffectInstance(cooldown);
        }

        public GameObject GetViewPrefab() => viewPrefab;
    }

    public class TauntEffectInstance : ICustomerEffectInstance, ITickable
    {
        private bool isActive = true;
        private float timer;
        private float cooldown;
        private RestaurantEncounter encounter;

        public event Action<float, float> OnTick;
        public event Action<bool> OnStateChanged;

        public TauntEffectInstance(float cooldown)
        {
            this.cooldown = cooldown;
            timer = cooldown;
        }

        public void Apply(Customer customer, RestaurantEncounter encounter)
        {
            this.encounter = encounter;
            customer.OnAte.AddListener(TurnOffTaunt);
            encounter.CustomersManager.OnCertainCustomerAdded.AddListener(UpdateStateForCustomer);
            UpdateStateForCustomers();
            customer.OnLeft.AddListener(() => RemoveListeners(customer, encounter));
        }

        private void RemoveListeners(Customer customer, RestaurantEncounter encounter)
        {
            customer.OnAte.RemoveListener(TurnOffTaunt);
            encounter.CustomersManager.OnCertainCustomerAdded.RemoveListener(UpdateStateForCustomer);
        }

        public void Tick(float deltaTime)
        {
            if (isActive)
                return;

            timer -= deltaTime;
            timer = Mathf.Clamp(timer, 0, cooldown);
            OnTick?.Invoke(timer, cooldown);

            if (timer == 0)
                TurnOnTaunt();
        }

        private void TurnOnTaunt() => SetActiveState(true);
        private void TurnOffTaunt()
        {
            SetActiveState(false);
            timer = cooldown;
        }

        private void SetActiveState(bool state)
        {
            isActive = state;
            OnStateChanged.Invoke(isActive);
            UpdateStateForCustomers();
        }

        private void UpdateStateForCustomers()
        {
            foreach (var customer in encounter.CustomersManager.Customers)
            {
                UpdateStateForCustomer(customer);
            }
        }

        private void UpdateStateForCustomer(Customer customer)
        {
            if (customer.HasTauntEffect)
                return;

            customer.SetBlockedByTauntOnAnotherCustomer(isActive);
        }
    }
}