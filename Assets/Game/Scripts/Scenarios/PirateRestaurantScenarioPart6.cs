using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization;

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
        [SerializeField] private LocalizedString line;

        protected override void InitTyped(RestaurantEncounter encounter)
        {
            Camera.main.transform.position = new Vector3(0, 0, -10);
            encounter.Ticker.Pause();
            customer.Init(encounter, seatPlace, ducky, () => popcicle);
            customer.CenterOnNewParent(seatPlace.transform);
            seatPlace.SetCustomer(customer);
            encounter.ItemsSpawner.SpawnItem(foodPrefab, encounter, popcicle, encounter.SlotsManager.BottomRowSlots[1], 0);
        }

        protected override async UniTask StartScenarioTyped(RestaurantEncounter encounter)
        {
            await WaitForCustomerIsFed(customer, encounter);
            await WaitForCustomerToLeave(customer);
            await UniTask.Delay(1000);
            successSound.Play();
            await UniTask.Delay(3000);
            await red.Say(line);
        }

        private UniTask<bool> WaitForCustomerIsFed(Customer customer, RestaurantEncounter encounter)
        {
            var tcs = new UniTaskCompletionSource<bool>();

            void AteHandler()
            {
                customer.OnAte.RemoveListener(AteHandler);
                tcs.TrySetResult(true);
                encounter.Ticker.SetRegularTickingSpeed();
            }

            customer.OnAte.AddListener(AteHandler);
            return tcs.Task;
        }

        private UniTask<bool> WaitForCustomerToLeave(Customer customer)
        {
            var tcs = new UniTaskCompletionSource<bool>();

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