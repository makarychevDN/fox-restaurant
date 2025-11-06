using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization;

namespace foxRestaurant
{
    public class PirateRestaurantScenarioPart1 : BaseScenario<RestaurantEncounter>
    {
        [Header("items data")]
        [SerializeField] private ItemData iceCreamConeData;
        [SerializeField] private ItemData popsicleData;
        [SerializeField] private ItemData iceCreamPlateData;
        [SerializeField] private ItemData cherrySyrupData;
        [SerializeField] private ItemData chocolateSyrupData;
        [SerializeField] private ItemData cherryIceCreamPlateData;
        [SerializeField] private ItemData chocolateIceCreamPlateData;
        [SerializeField] private ItemData coalData;

        [Header("customers data")]
        [SerializeField] private CustomerData doggo;
        [SerializeField] private CustomerData kitty;
        [SerializeField] private CustomerData duck;
        [SerializeField] private CustomerData bearMiniBossData;

        [Header("mini boss setup")]
        [SerializeField] private Customer bearMiniBoss;
        [SerializeField] private GameObject bearsUI;
        [SerializeField] private AudioSource blinkSound;
        [SerializeField] private Animator blinkAnimator;
        [SerializeField] private SeatPlace miniBossSeatPlace;
        [SerializeField] private AudioSource heavyStepSound;

        [Header("music")]
        [SerializeField] private AudioSource regularMusic;
        [SerializeField] private AudioSource bossMusic;

        [Header("other links")]
        [SerializeField] private ItemSlot garbageCan;
        [SerializeField] private Transform parentOfBottomItemSlots;
        [SerializeField] private Character redTheCook;
        [SerializeField] private AudioSource brokenSound;
        [SerializeField] private AudioSource successSound;
        [SerializeField] private AudioSource hornSound;

        [SerializeField] private ItemSpawnTimer itemSpawnTimer;
        [SerializeField] private GameObject slotsToSpawnFoodParent;
        [SerializeField] private GameObject effectOnSlotsToSpawnFoodAppear;
        [SerializeField] private AudioSource soundOnSlotsToSpawnFoodAppear;

        [Header("Dialogue Lines")]
        [SerializeField] LocalizedString waveIsFailedLine;
        [SerializeField] List<LocalizedString> dialogueLines;

        private List<Customer> customersToFeed = new();
        private List<ItemSlot> itemSlots;
        private int switchConeAndPopsicleCount;

        protected override async Task StartScenarioTyped(RestaurantEncounter encounter)
        {
            itemSlots = encounter.SlotsManager.Slots.Where(slot => slot.RequiredItemsType == ItemType.Food && slot.gameObject.activeSelf).ToList();

            encounter.BlockInput();
            await Task.Delay(500);
            await redTheCook.Say(dialogueLines[0]);
            encounter.UnblockInput();

            await TeachToFeedCustomers(encounter);
            await Task.Delay(500);
            await TeachToUnderstandHPMechanic(encounter);
            await Task.Delay(500);
            await GarbageCanTutorial(encounter);
            await Task.Delay(500);
            await TeachToFuseIngredients(encounter);
            await Task.Delay(500);
            await TeachToManageSpawnedItems(encounter);
            await Task.Delay(500);
            await MiniBossDialogue(encounter);
            await MiniBossWave(encounter);
            await MiniBossDialogueAfterWave(encounter);
        }

        private async Task TeachToFeedCustomers(RestaurantEncounter encounter)
        {
            await FixWave(encounter, new List<ItemData> { iceCreamConeData, iceCreamConeData },
                (duck, () => iceCreamConeData), (kitty, () => iceCreamConeData));

            await FixWave(encounter, new List<ItemData> { popsicleData, iceCreamConeData },
                (duck, () => iceCreamConeData), (kitty, () => popsicleData));
        }

        private async Task TeachToUnderstandHPMechanic(RestaurantEncounter encounter)
        {
            await FixWave(encounter, new List<ItemData> { popsicleData, iceCreamConeData },
                dialogueLines[1].GetLocalizedString(),
                (doggo, SwitchConeAndPopsicle));

            encounter.BlockInput();
            await redTheCook.Say(dialogueLines[2].GetLocalizedString());
            encounter.UnblockInput();

            await FixWave(encounter, new List<ItemData> { popsicleData, iceCreamConeData, popsicleData, iceCreamConeData },
                (doggo, SwitchConeAndPopsicle), (kitty, () => popsicleData), (duck, () => iceCreamConeData));

            await FixWave(encounter, new List<ItemData> { popsicleData, iceCreamConeData, popsicleData, iceCreamConeData },
                (doggo, () => iceCreamConeData), (doggo, () => popsicleData));
        }

        private async Task TeachToFuseIngredients(RestaurantEncounter encounter)
        {
            await FixWave(encounter, new List<ItemData> { cherrySyrupData, iceCreamPlateData },
                new List<string> { dialogueLines[8].GetLocalizedString(),
                dialogueLines[9].GetLocalizedString(),
                dialogueLines[10].GetLocalizedString() },
                (duck, () => cherryIceCreamPlateData));

            encounter.BlockInput();
            await redTheCook.Say(dialogueLines[11].GetLocalizedString());
            encounter.UnblockInput();

            await FixWave(encounter, new List<ItemData> { cherrySyrupData, iceCreamPlateData, chocolateSyrupData, iceCreamPlateData },
                (doggo, () => cherryIceCreamPlateData), (kitty, () => chocolateIceCreamPlateData));

            await FixWave(encounter, new List<ItemData> { cherrySyrupData, iceCreamPlateData, iceCreamConeData, popsicleData },
                (doggo, () => cherryIceCreamPlateData), (kitty, () => iceCreamConeData), (duck, () => popsicleData));
        }

        private async Task GarbageCanTutorial(RestaurantEncounter encounter)
        {
            encounter.BlockInput();
            brokenSound.Play();
            await parentOfBottomItemSlots.DOShakeScale(0.3f, 0.5f, 10, 0).AsyncWaitForCompletion();
            parentOfBottomItemSlots.transform.localScale = Vector3.one;

            List<Item> spawnedItems = new();
            for (int i = 0; i < 4; i++)
            {
                await Task.Delay(100);
                spawnedItems.Add(encounter.ItemsSpawner.SpawnFoodItem(encounter, coalData, itemSlots[i]));
            }
            int satietySum = spawnedItems.Sum(foodItem => (foodItem as FoodItem).Satiety);

            await Task.Delay(500);
            await redTheCook.Say(dialogueLines[3].GetLocalizedString());
            await redTheCook.Say(dialogueLines[4].GetLocalizedString());
            await redTheCook.Say(dialogueLines[5].GetLocalizedString());
            garbageCan.gameObject.SetActive(true);
            await Task.Delay(500);
            await redTheCook.Say(dialogueLines[6].GetLocalizedString());
            encounter.UnblockInput();
            await WaitForHungerPointsDisposed(satietySum);
            encounter.BlockInput();
            await redTheCook.Say(dialogueLines[7].GetLocalizedString());
            encounter.UnblockInput();
        }

        private async Task TeachToManageSpawnedItems(RestaurantEncounter encounter)
        {
            slotsToSpawnFoodParent.SetActive(true);
            effectOnSlotsToSpawnFoodAppear.SetActive(true);
            soundOnSlotsToSpawnFoodAppear.Play();
            encounter.BlockInput();
            await Task.Delay(500);
            await redTheCook.Say(dialogueLines[12].GetLocalizedString());
            await redTheCook.Say(dialogueLines[13].GetLocalizedString());
            await redTheCook.Say(dialogueLines[14].GetLocalizedString());
            encounter.UnblockInput();
            itemSpawnTimer.Init(encounter);

            await FixWave(encounter, new List<ItemData>(),
                (encounter.DecksManager.GetRandomCustomer(), encounter.DecksManager.GetRandomDish),
                (encounter.DecksManager.GetRandomCustomer(), encounter.DecksManager.GetRandomDish));

            await FixWave(encounter, new List<ItemData>(),
                (encounter.DecksManager.GetRandomCustomer(), encounter.DecksManager.GetRandomDish),
                (encounter.DecksManager.GetRandomCustomer(), encounter.DecksManager.GetRandomDish),
                (encounter.DecksManager.GetRandomCustomer(), encounter.DecksManager.GetRandomDish));

            await FixWave(encounter, new List<ItemData>(),
                (encounter.DecksManager.GetRandomCustomer(), encounter.DecksManager.GetRandomDish),
                (encounter.DecksManager.GetRandomCustomer(), encounter.DecksManager.GetRandomDish),
                (encounter.DecksManager.GetRandomCustomer(), encounter.DecksManager.GetRandomDish),
                (encounter.DecksManager.GetRandomCustomer(), encounter.DecksManager.GetRandomDish));
        }


        private async Task MiniBossDialogue(RestaurantEncounter encounter)
        {
            encounter.BlockInput();
            regularMusic.Stop();
            encounter.Ticker.Pause();
            await HeavyStep(0.25f, 0.25f);
            await Task.Delay(750);
            await redTheCook.Say(dialogueLines[15].GetLocalizedString());
            await HeavyStep(0.5f, 0.5f);
            await Task.Delay(750);
            await HeavyStep(0.75f, 0.75f);
            await Task.Delay(750);
            await HeavyStep(1f, 1);
            await Task.Delay(1500);
            miniBossSeatPlace.gameObject.SetActive(true);
            bearMiniBoss.gameObject.SetActive(true);
            miniBossSeatPlace.SetCustomer(bearMiniBoss);
            await HeavyStep(1f, 1);
            await Task.Delay(1000);
            blinkSound.Play();
            blinkAnimator.SetTrigger("blink");
            await Task.Delay(1000);
            await bearMiniBoss.Character.Say(dialogueLines[16].GetLocalizedString());
            await redTheCook.Say(dialogueLines[17].GetLocalizedString());
            await bearMiniBoss.Character.Say(dialogueLines[18].GetLocalizedString());
            await bearMiniBoss.Character.Say(dialogueLines[19].GetLocalizedString());
            await redTheCook.Say(dialogueLines[20].GetLocalizedString());
            await bearMiniBoss.Character.Say(dialogueLines[21].GetLocalizedString());
            await bearMiniBoss.Character.Say(dialogueLines[22].GetLocalizedString());
            await redTheCook.Say(dialogueLines[23].GetLocalizedString());
            await bearMiniBoss.Character.Say(dialogueLines[24].GetLocalizedString());
            blinkSound.Play();
            blinkAnimator.SetTrigger("blink");
            await Task.Delay(1000);
            await bearMiniBoss.Character.Say(dialogueLines[25].GetLocalizedString());
            bearMiniBoss.gameObject.SetActive(false);
            miniBossSeatPlace.gameObject.SetActive(true);
            encounter.Ticker.SetRegularTickingSpeed();
            encounter.UnblockInput();
        }

        private async Task MiniBossWave(RestaurantEncounter encounter)
        {
            bossMusic.Play();
            await FixWave(encounter, new List<ItemData>(),
                (bearMiniBossData, encounter.DecksManager.GetRandomDish),
                (doggo, encounter.DecksManager.GetRandomDish),
                (kitty, encounter.DecksManager.GetRandomDish),                
                (duck, encounter.DecksManager.GetRandomDish));
        }

        private Task WaitForHungerPointsDisposed(int targetHungerSum)
        {
            var tcs = new TaskCompletionSource<bool>();
            int currentSum = 0;

            void OnItemDisposed(Item item)
            {
                currentSum += (item as FoodItem).Satiety;
                if (currentSum >= targetHungerSum)
                {
                    garbageCan.OnItemHasBeenPlaced.RemoveListener(OnItemDisposed);
                    tcs.TrySetResult(true);
                }
            }

            garbageCan.OnItemHasBeenPlaced.AddListener(OnItemDisposed);
            return tcs.Task;
        }

        private async Task MiniBossDialogueAfterWave(RestaurantEncounter encounter)
        {
            encounter.BlockInput();
            bossMusic.Stop();
            encounter.Ticker.Pause();
            miniBossSeatPlace.gameObject.SetActive(true);
            bearMiniBoss.gameObject.SetActive(true);
            await HeavyStep(1f, 1);
            await Task.Delay(1000);
            await bearMiniBoss.Character.Say(dialogueLines[26].GetLocalizedString());
            await redTheCook.Say(dialogueLines[27].GetLocalizedString());
            await bearMiniBoss.Character.Say(dialogueLines[28].GetLocalizedString());
            blinkSound.Play();
            blinkAnimator.SetTrigger("blink");
            await Task.Delay(1000);
            await bearMiniBoss.Character.Say(dialogueLines[29].GetLocalizedString());
            await bearMiniBoss.Character.Say(dialogueLines[30].GetLocalizedString());
            bearMiniBoss.LeaveSatisfied();
            await Task.Delay(1000);
            await redTheCook.Say(dialogueLines[31].GetLocalizedString());
            successSound.Play();
            await Task.Delay(3000);
            hornSound.Play();
            await Task.Delay(3000);
            await redTheCook.Say(dialogueLines[32].GetLocalizedString());
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
                encounter.BlockInput();
                customersToFeed.Clear();

                for (int i = 0; i < itemsToSpawnData.Count; i++)
                {
                    await Task.Delay(500);
                    encounter.ItemsSpawner.SpawnFoodItem(encounter, itemsToSpawnData[i], itemSlots[i]);
                }

                foreach(var customerAndOrder in customersAndTheirOrders)
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
                    await redTheCook.Say(commentary);
                }
                encounter.UnblockInput();

                var tasks = customersToFeed.Select(WaitForCustomerToLeave).ToArray();
                var results = await Task.WhenAll(tasks);

                success = results.All(r => r);
                if (!success)
                    await redTheCook.Say(waveIsFailedLine);
            }
        }

        private ItemData SwitchConeAndPopsicle()
        {
            switchConeAndPopsicleCount++;
            return switchConeAndPopsicleCount % 2 == 0 ? popsicleData : iceCreamConeData;
        }

        private Task<bool> WaitForCustomerToLeave(Customer customer)
        {
            var tcs = new TaskCompletionSource<bool>();

            void OnLeftHandler(bool wasSatisfied)
            {
                customer.OnLeft.RemoveListener(OnLeftHandler);
                tcs.TrySetResult(wasSatisfied);
            }

            customer.OnLeft.AddListener(OnLeftHandler);
            return tcs.Task;
        }

        private async Task HeavyStep(float soundVolume, float strenght, int shakingValue = 50)
        {
            heavyStepSound.volume = soundVolume;
            heavyStepSound.Play();
            await Camera.main.transform.DOShakePosition(0.3f, strenght, shakingValue).AsyncWaitForCompletion();
            Camera.main.transform.position = new Vector3(0, 0, -10);
        }
    }
}