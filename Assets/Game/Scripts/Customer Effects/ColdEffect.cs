using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace foxRestaurant
{
    [CreateAssetMenu(fileName = "cold effect", menuName = "Scriptable Objects/Customer Effects/cold effect")]
    public class ColdEffect : ScriptableObject, ICustomerEffect, IAbleToReturnViewPrefab
    {
        [SerializeField] private float cooldown;
        [SerializeField] private int additionalHunger;
        [SerializeField] private GameObject viewPrefab;

        public ICustomerEffectInstance CreateInstance()
        {
            return new ColdEffectInstance(additionalHunger, cooldown);
        }

        public GameObject GetViewPrefab() => viewPrefab;
    }

    public class ColdEffectInstance : ICustomerEffectInstance, ITickable
    {
        private int additionalHunger;
        private float timer;
        private float cooldown;
        private RestaurantEncounter encounter;
        private Customer owner;

        public event Action<float, float> OnTick;
        public event Action OnSneeze;
        public event Action OnColdCured;

        public ColdEffectInstance(int additionalHunger, float cooldown)
        {
            this.additionalHunger = additionalHunger;
            this.cooldown = cooldown;
        }

        public void Apply(Customer customer, RestaurantEncounter encounter)
        {
            this.encounter = encounter;
            owner = customer;
            customer.OnAte.AddListener(CureCold);
            customer.OnStartLeavingProcess.RemoveListener(CureCold);
        }

        public void Tick(float deltaTime)
        {
            if (owner.IsLeaving)
                return;

            timer += deltaTime;
            OnTick.Invoke(timer, cooldown);

            if (timer >= cooldown)
            {
                Sneeze();
            }
        }

        private void Sneeze()
        {
            foreach (var customer in encounter.CustomersManager.Customers)
            {
                customer.AddHunger(additionalHunger);
            }

            ResetTimer();
            OnSneeze.Invoke();
        }

        private void CureCold()
        {
            OnColdCured.Invoke();
            ResetTimer();
        }

        private void ResetTimer()
        {
            timer = 0;
            OnTick.Invoke(timer, cooldown);
        }
    }
}
