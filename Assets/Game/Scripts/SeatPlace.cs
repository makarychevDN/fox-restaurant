using UnityEngine;
using UnityEngine.Events;

namespace foxRestaurant
{
    public class SeatPlace : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particles;
        [SerializeField] private AudioSource payMoneySound;
        [SerializeField] private AudioSource bonkSound;
        private ItemSlot customerItemSlot;
        private Customer customer;
        private RestaurantEncounter restaurantEncounter;

        public UnityEvent OnCustomerTookSeat;
        public UnityEvent OnCustomerLeave;

        public bool IsTaken => customer != null;

        public void Init(RestaurantEncounter restaurantEncounter)
        {
            this.restaurantEncounter = restaurantEncounter;
            customerItemSlot = restaurantEncounter.SlotsToPlaceCustomerItemsSpawner.SpawnSlot();
            customerItemSlot.transform.position = transform.position;
            customerItemSlot.OnItemHasBeenPlaced.AddListener(HandleCustomerItemPlaced);
            customerItemSlot.Init(restaurantEncounter);
            customerItemSlot.transform.rotation = transform.rotation;
        }

        private void HandleCustomerItemPlaced(Item item)
        {
            if (item == null)
                return;

            customer = restaurantEncounter.CustomerSpawner.SpawnCustomer(this, (CustomerData)item.ItemData, () => restaurantEncounter.DecksManager.GetRandomDish());

            customerItemSlot.Clear();
            customerItemSlot.gameObject.SetActive(false);
        }

        public void SetCustomer(Customer customer)
        {
            if(customer == null)
            {
                this.customer.OnLeftSatisfied.RemoveListener(CustomerLeftHandler);
                this.customer.OnLeftSatisfied.RemoveListener(PlayPayMoneySoundSound);
                this.customer.OnLeftUnsatisfied.RemoveListener(CustomerLeftHandler);
                this.customer.OnLeftUnsatisfied.RemoveListener(PlayBonkSound);
            }
            else
            {
                particles.Play();
                customer.OnLeftSatisfied.AddListener(CustomerLeftHandler);
                customer.OnLeftSatisfied.AddListener(PlayPayMoneySoundSound);
                customer.OnLeftUnsatisfied.AddListener(CustomerLeftHandler);
                customer.OnLeftUnsatisfied.AddListener(PlayBonkSound);
            }

            this.customer = customer;
        }

        private void CustomerLeftSatisfiedHandler()
        {
            particles.Play();
            SetCustomer(null);
            customerItemSlot.gameObject.SetActive(true);
        }

        private void CustomerLeftHandler()
        {
            particles.Play();
            SetCustomer(null);
            customerItemSlot.gameObject.SetActive(true);
        }

        private void PlayPayMoneySoundSound() => payMoneySound.Play();
        private void PlayBonkSound() => bonkSound.Play();
    }
}