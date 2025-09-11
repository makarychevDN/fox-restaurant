using UnityEngine;
using UnityEngine.Events;

namespace foxRestaurant
{
    public class SeatPlace : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particles;
        [SerializeField] private AudioSource payMoneySound;
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

        private void CustomerLeftHandler()
        {
            particles.Play();
            payMoneySound.Play();
            SetCustomer(null);
        }
    }
}