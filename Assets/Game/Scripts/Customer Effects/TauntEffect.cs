using System;
using UnityEngine;

namespace foxRestaurant
{
    [CreateAssetMenu(fileName = "taunt effect", menuName = "Scriptable Objects/Customer Effects/taunt effect")]
    public class TauntEffect : ScriptableObject, ICustomerEffect
    {
        [SerializeField] private float cooldown;

        public ICustomerEffectInstance CreateInstance()
        {
            return new TauntEffectInstance(cooldown);
        }
    }

    public class TauntEffectInstance : ICustomerEffectInstance, ITickable
    {
        private bool isActive = true;
        private float timer;
        private float cooldown;

        public event Action<float, float> OnTick;
        public event Action<bool> OnStateChanged;

        public TauntEffectInstance(float cooldown)
        {
            this.cooldown = cooldown;
            timer = cooldown;
        }

        public void Apply(Customer customer)
        {
            customer.OnAte.AddListener(TurnOffTaunt);
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
        private void TurnOffTaunt() => SetActiveState(false);

        private void SetActiveState(bool state)
        {
            isActive = state;
            OnStateChanged.Invoke(isActive);
        }
    }
}