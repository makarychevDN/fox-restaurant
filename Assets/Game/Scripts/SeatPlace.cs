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
        private RestaurantEncounter restaurantEncounter;

        public UnityEvent OnCustomerTookSeat;
        public UnityEvent OnCustomerLeave;

        public bool IsTaken => customer != null;

        public void Init(RestaurantEncounter restaurantEncounter, Table table)
        {
            this.restaurantEncounter = restaurantEncounter;
        }

        public void SetCustomer(Customer customer)
        {
            if(customer == null)
            {
                this.customer.OnLeftSatisfied.RemoveListener(CustomerLeftHandler);
            }
            else
            {
                particles.Play();
                customer.OnLeftSatisfied.AddListener(CustomerLeftHandler);
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