using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class PirateRestaurantScenarioPart6 : BaseScenario<RestaurantEncounter>
    {
        [SerializeField] private Customer customer;
        [SerializeField] private SeatPlace seatPlace;
        [SerializeField] private CustomerData ducky;
        [SerializeField] private ItemData popcicle;
        [SerializeField] private FoodItem foodPrefab;
        [SerializeField] private AudioSource successSound;
        [SerializeField] private Character red;
        private List<ItemSlot> itemSlots;

        protected override void InitTyped(RestaurantEncounter encounter)
        {
            itemSlots = encounter.SlotsManager.Slots.Where(slot => slot.RequiredItemsType == ItemType.Food && slot.gameObject.activeSelf).ToList();
            encounter.Ticker.Pause();
            seatPlace.Init(encounter);
            customer.Init(encounter, ducky, () => popcicle);
            customer.CenterOnNewParent(seatPlace.transform);
            seatPlace.SetCustomer(customer);
            encounter.ItemsSpawner.SpawnItem(foodPrefab, encounter, popcicle, itemSlots[1], 0);
        }

        protected override async Task StartScenarioTyped(RestaurantEncounter encounter)
        {
            await WaitForCustomerToLeave(customer);
            await Task.Delay(1000);
            successSound.Play();
            await Task.Delay(3000);
            await red.Say("*вздох* <pause:1> Домой.");
        }

        private Task<bool> WaitForCustomerToLeave(Customer customer)
        {
            var tcs = new TaskCompletionSource<bool>();

            void OnLeftHandler(bool wasSatisfied)
            {
                customer.OnLeftSatisfied.RemoveListener(OnLeftHandler);
                tcs.TrySetResult(wasSatisfied);
            }

            customer.OnLeftSatisfied.AddListener(OnLeftHandler);
            return tcs.Task;
        }
    }
}