using foxRestaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization;

namespace foxRestaurant
{
    public class PirateRestaurantScenarioPart5 : BaseScenario<RestaurantEncounter>
    {
        [Header("characters")]
        [SerializeField] private Character red;
        [SerializeField] private Character lameJoe;
        [SerializeField] private Character speaker;
        [SerializeField] private Character kidFromTheCustomers;
        [SerializeField] private Character kidOnTheStreet1;
        [SerializeField] private Character kidOnTheStreet2;
        [SerializeField] private CustomerData doggo;
        [SerializeField] private CustomerData kitty;
        [SerializeField] private CustomerData duck;
        [SerializeField] private CustomerData seal;

        [Header("items data")]
        [SerializeField] private ItemData iceCreamConeData;
        [SerializeField] private ItemData popsicleData;
        [SerializeField] private ItemData iceCreamPlateData;
        [SerializeField] private ItemData cherrySyrupData;
        [SerializeField] private ItemData chocolateSyrupData;
        [SerializeField] private ItemData cherryIceCreamPlateData;
        [SerializeField] private ItemData chocolateIceCreamPlateData;
        [SerializeField] private ItemData coalData;

        [SerializeField] LocalizedString waveIsFailedLine;

        [Header("other links")]
        [SerializeField] private ItemSlot garbageCan;
        [SerializeField] private ItemSpawnTimer itemSpawnTimer;
        [SerializeField] private GameObject slotsToSpawnFoodParent;
        [SerializeField] private GameObject effectOnSlotsToSpawnFoodAppear;
        [SerializeField] private AudioSource boowompSound;
        [SerializeField] private AudioSource poofSound;
        [SerializeField] private ParticleSystem lameJoeAppearParticles;
        [SerializeField] private ParticleSystem sealGirlAppearParticles;

        private List<Customer> customersToFeed = new();
        private List<ItemSlot> itemSlots;

        protected override async Task StartScenarioTyped(RestaurantEncounter encounter)
        {
            itemSlots = encounter.SlotsManager.Slots.Where(slot => slot.RequiredItemsType == ItemType.Food && slot.gameObject.activeSelf).ToList();
            await IntroSpeech();
            await Waves(encounter);
            await CapitanAppears();
        }

        private async Task IntroSpeech()
        {
            //todo curtains appear
            await red.Say("Ok, that's it. 3,<pause:1> 2,<pause:1> 1.");
            lameJoe.gameObject.SetActive(true);
            await red.Say("<volume:0>.<pause:0.5>.<pause:0.5>.<pause:0.5><volume:1> Работает!<pause:0.75> Никакой толпы не набежало!");
            await lameJoe.Say("Я же говорил!<pause:0.75> План-капкан!");
            await red.Say("Ладно,<pause:0.5> давай с этим покончим.");
        }

        private async Task Waves(RestaurantEncounter encounter)
        {
            garbageCan.gameObject.SetActive(true);
            slotsToSpawnFoodParent.SetActive(true);
            effectOnSlotsToSpawnFoodAppear.SetActive(true);
            itemSpawnTimer.Init(encounter);

            await FixWave(encounter, new List<ItemData>() { },
                (seal, () => encounter.DecksManager.GetRandomDish()),
                (kitty, () => encounter.DecksManager.GetRandomDish()),
                (kitty, () => encounter.DecksManager.GetRandomDish()),
                (duck, () => encounter.DecksManager.GetRandomDish()));

            await FixWave(encounter, new List<ItemData>() { },
                (seal, () => encounter.DecksManager.GetRandomDish()),
                (kitty, () => encounter.DecksManager.GetRandomDish()),
                (doggo, () => encounter.DecksManager.GetRandomDish()),
                (duck, () => encounter.DecksManager.GetRandomDish()));

            await FixWave(encounter, new List<ItemData>() { },
                (seal, () => encounter.DecksManager.GetRandomDish()),
                (doggo, () => encounter.DecksManager.GetRandomDish()),
                (doggo, () => encounter.DecksManager.GetRandomDish()),
                (duck, () => encounter.DecksManager.GetRandomDish()));

            await FixWave(encounter, new List<ItemData>() { },
                (seal, () => encounter.DecksManager.GetRandomDish()),
                (seal, () => encounter.DecksManager.GetRandomDish()),
                (doggo, () => encounter.DecksManager.GetRandomDish()),
                (duck, () => encounter.DecksManager.GetRandomDish()));

            await FixWave(encounter, new List<ItemData>() { },
                (seal, () => encounter.DecksManager.GetRandomDish()),
                (seal, () => encounter.DecksManager.GetRandomDish()),
                (doggo, () => encounter.DecksManager.GetRandomDish()),
                (doggo, () => encounter.DecksManager.GetRandomDish()));
        }


        private async Task CapitanAppears()
        {
            await speaker.Say("Йарр. Вы чего здесь удумали, салаги?");
            //todo мультики выключаются
            await kidFromTheCustomers.Say("Эй, мы же смотрим!");
            await speaker.Say("Я тебя отправил на палубу, чтобы ты привлек мне посетителей, а не заперся от них!");
            await speaker.Say("Ничего, сейчас мы это исправим. Йарр!");
            //todo шторы поднимаются

            await kidOnTheStreet1.Say("Смотрите, это же хромой Джо!");
            await kidOnTheStreet2.Say("Это точно он, скорее к нему!");
            //todo начинается землятрясение
            await red.Say("О нет.");
            //оно усиливается
            await lameJoe.Say("Беги!");
            // землятрясение достигает пика, камера трясется, зал заполняет толпа детей

        }

        private async Task FixWave(RestaurantEncounter encounter, List<ItemData> itemsToSpawnData, params (CustomerData, Func<ItemData>)[] customersAndTheirOrders)
        {
            await FixWave(encounter, itemsToSpawnData, new List<string>(), customersAndTheirOrders);
        }

        private async Task FixWave(RestaurantEncounter encounter, List<ItemData> itemsToSpawnData, string commentary, params (CustomerData, Func<ItemData>)[] customersAndTheirOrders)
        {
            await FixWave(encounter, itemsToSpawnData, new List<string> { commentary }, customersAndTheirOrders);
        }

        private async Task FixWave(RestaurantEncounter encounter, List<ItemData> itemsToSpawnData, List<string> commentaries, params (CustomerData, Func<ItemData>)[] customersAndTheirOrders)
        {
            bool success = false;
            while (!success)
            {
                Time.timeScale = 1;
                encounter.BlockInput();
                customersToFeed.Clear();

                if (itemsToSpawnData.Count > 0)
                {
                    itemSlots.ForEach(slot => slot.Clear());
                    FindObjectsOfType<Item>().ToList().ForEach(item => Destroy(item.gameObject));
                }

                for (int i = 0; i < itemsToSpawnData.Count; i++)
                {
                    await Task.Delay(500);
                    encounter.ItemsSpawner.SpawnFoodItem(encounter, itemsToSpawnData[i], itemSlots[i]);
                }

                foreach (var customerAndOrder in customersAndTheirOrders)
                {
                    await Task.Delay(500);

                    var customer = encounter.CustomerSpawner.TryToSpawnCustomer(
                        customerAndOrder.Item1,
                        customerAndOrder.Item2
                    );

                    customersToFeed.Add(customer);
                }

                foreach (var commentary in commentaries)
                {
                    await red.Say(commentary);
                }
                encounter.UnblockInput();

                var tasks = customersToFeed.Select(WaitForCustomerToLeave).ToArray();
                var results = await Task.WhenAll(tasks);
                Time.timeScale = 1;

                success = results.All(r => r);
                if (!success)
                    await red.Say(waveIsFailedLine);
            }
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