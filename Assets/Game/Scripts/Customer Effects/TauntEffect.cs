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

        public TauntEffectInstance(float cooldown)
        {
            this.cooldown = cooldown;
            this.timer = cooldown;
        }

        public void Apply(Customer customer)
        {
            Debug.Log($"muhaha, Taunt from {customer}");
            customer.OnAte.AddListener(TurnOffTheTaunt);
        }

        public void Tick(float deltaTime)
        {
            if (isActive)
                return;

            timer -= deltaTime;
            timer = Mathf.Clamp(timer, 0, cooldown);

            if (timer == 0)
                TurnOnTheTaunt();
        }

        public void TurnOffTheTaunt()
        {
            isActive = false;
            Debug.Log("I tired of this, I need some rest");
        }

        public void TurnOnTheTaunt()
        {
            isActive = true;
            Debug.Log("my taunt is back!!!");
        }
    }
}