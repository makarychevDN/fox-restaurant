using UnityEngine;
using UnityEngine.Events;

namespace foxRestaurant
{
    public class SeatPlace : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particles;
        [SerializeField] private AudioSource payMoneySound;
        [SerializeField] private AudioSource bonkSound;
        private Customer customer;

        public UnityEvent OnCustomerTookSeat;
        public UnityEvent OnCustomerLeave;

        public bool IsTaken => customer != null;

        public void SetCustomer(Customer customer)
        {
            if(customer == null)
            {
                this.customer.OnLeft.RemoveListener(CustomerLeftHandler);
            }
            else
            {
                particles.Play();
                customer.OnLeft.AddListener(CustomerLeftHandler);
            }

            this.customer = customer;
        }

        private void CustomerLeftHandler(bool customerIsSatisfied)
        {
            particles.Play();

            if(customerIsSatisfied)
                payMoneySound.Play();
            else
                bonkSound.Play();
            SetCustomer(null);
        }
    }
}