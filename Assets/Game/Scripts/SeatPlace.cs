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

            if(customerItemSlot != null)
                customerItemSlot.gameObject.SetActive(true);
        }
    }
}