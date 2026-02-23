using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization;

namespace foxRestaurant
{
    public class PirateRestaurantScenarioPart3 : BaseScenario<RestaurantEncounter>
    {
        [Header("characters")]
        [SerializeField] private Character red; 
        [SerializeField] private Character lameJoe;
        [SerializeField] private Character sealGirl;
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
        [SerializeField] private AudioSource backgroundMusic;
        [SerializeField] private AudioSource successSound;
        [SerializeField] private ParticleSystem lameJoeAppearParticles;
        [SerializeField] private ParticleSystem sealGirlAppearParticles;

        private List<Customer> customersToFeed = new();
        private List<ItemSlot> itemSlots;

        [SerializeField] List<LocalizedString> dialogueLines;
        private int stringsCounter = 0;
        private int Next => stringsCounter++;

        protected override async Task StartScenarioTyped(RestaurantEncounter encounter)
        {
            itemSlots = encounter.SlotsManager.Slots.Where(slot => slot.RequiredItemsType == ItemType.Food && slot.gameObject.activeSelf).ToList();
            await IntroSpeech();
            await SealTutorail(encounter);
            await Waves(encounter);
            await SealGirlGoesAway(encounter);
            await LameJoeAppears();
        }

        private async Task IntroSpeech()
        {
            await red.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]); 
            await red.Say(dialogueLines[Next]);
        }

        private async Task SealTutorail(RestaurantEncounter encounter)
        {
            sealGirl.gameObject.SetActive(true);
            sealGirlAppearParticles.Play();
            poofSound.Play();

            red.LookAt(sealGirl.transform);
            await red.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            red.LookAt(null);

            sealGirl.gameObject.SetActive(false);
            sealGirlAppearParticles.Play();

            await FixWave(encounter, new List<ItemData>() { popsicleData, popsicleData, cherrySyrupData, iceCreamPlateData },
                (seal, () => cherryIceCreamPlateData),
                (kitty, () => popsicleData));
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

        private async Task SealGirlGoesAway(RestaurantEncounter encounter)
        {
            backgroundMusic.DOFade(0, 1);
            encounter.Ticker.Pause();
            await red.Say(dialogueLines[Next]);

            sealGirl.gameObject.SetActive(true);
            sealGirlAppearParticles.Play();
            poofSound.Play();
            red.LookAt(sealGirl.transform);
            await sealGirl.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await sealGirl.Say(dialogueLines[Next]);
            await sealGirl.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await sealGirl.Say(dialogueLines[Next]);
            sealGirl.gameObject.SetActive(false);
            sealGirlAppearParticles.Play();
            poofSound.Play();
            red.LookAt(null);

            await red.Say(dialogueLines[Next]);
            successSound.Play();
            await Task.Delay(3000);
        }

        private async Task LameJoeAppears()
        {
            lameJoe.gameObject.SetActive(true);
            red.LookAt(lameJoe.transform);
            await lameJoe.Say(dialogueLines[Next]);
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

        protected override void InitTyped(RestaurantEncounter encounter) { }
    }
}