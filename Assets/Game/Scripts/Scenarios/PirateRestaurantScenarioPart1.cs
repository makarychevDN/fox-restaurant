using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

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

        [Header("other links")]
        [SerializeField] private ItemSlot garbageCan;
        [SerializeField] private Transform parentOfBottomItemSlots;
        [SerializeField] private Character redTheCook;
        [SerializeField] private AudioSource brokenSound;

        [SerializeField] private ItemSpawnTimer itemSpawnTimer;
        [SerializeField] private GameObject slotsToSpawnFoodParent;
        [SerializeField] private GameObject effectOnSlotsToSpawnFoodAppear;
        [SerializeField] private AudioSource soundOnSlotsToSpawnFoodAppear;

        private List<Customer> customersToFeed = new();
        private List<ItemSlot> itemSlots;
        private TaskCompletionSource<bool> completionSource = new();
        private int switchConeAndPopsicleCount;

        protected override async Task StartScenarioTyped(RestaurantEncounter encounter)
        {
            itemSlots = encounter.SlotsManager.Slots.Where(slot => slot.RequiredItemsType == ItemType.Food && slot.gameObject.activeSelf).ToList();

            /*await Task.Delay(500);
            await redTheCook.Say("*sigh*<pause:1> Another day, another dollar.");
            await TeachToFeedCustomers(encounter);
            await Task.Delay(500);
            await TeachToUnderstandHPMechanic(encounter);
            await Task.Delay(500);
            await GarbageCanTutorial(encounter);
            await Task.Delay(500);
            await TeachToFuseIngredients(encounter);
            await Task.Delay(500);*/
            await TeachToManageSpawnedItems(encounter);
            //await Task.Delay(500);
            //await MiniBossDialogue(encounter);
            await MiniBossWave(encounter);
            await completionSource.Task;
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
                "Damn,<pause:0.5> this kiddo seems tough,<pause:0.5> he needs at least two icecreams.",
                (doggo, SwitchConeAndPopsicle));

            await redTheCook.Say("Big boy, huh?<pause:0.75> The bigger they are, the harder they fall.");

            await FixWave(encounter, new List<ItemData> { popsicleData, iceCreamConeData, popsicleData, iceCreamConeData },
                (doggo, SwitchConeAndPopsicle), (kitty, () => popsicleData), (duck, () => iceCreamConeData));

            await FixWave(encounter, new List<ItemData> { popsicleData, iceCreamConeData, popsicleData, iceCreamConeData },
                (doggo, SwitchConeAndPopsicle), (doggo, SwitchConeAndPopsicle));
        }

        private async Task TeachToFuseIngredients(RestaurantEncounter encounter)
        {
            await FixWave(encounter, new List<ItemData> { cherrySyrupData, iceCreamPlateData },
                new List<string> { "Oh,<pause:0.5> wow.<pause:0.5> The first complex order for today.",
                "Still too easy though.",
                "Need to mix the ingredients." },
                (duck, () => cherryIceCreamPlateData));
            await redTheCook.Say("*sigh*<pause:1> I wonder what it's like to cook real dishes?");

            await FixWave(encounter, new List<ItemData> { cherrySyrupData, iceCreamPlateData, chocolateSyrupData, iceCreamPlateData },
                (doggo, () => cherryIceCreamPlateData), (kitty, () => chocolateIceCreamPlateData));

            await FixWave(encounter, new List<ItemData> { cherrySyrupData, iceCreamPlateData, iceCreamConeData, popsicleData },
                (doggo, () => cherryIceCreamPlateData), (kitty, () => iceCreamConeData), (duck, () => popsicleData));
        }

        private async Task GarbageCanTutorial(RestaurantEncounter encounter)
        {
            brokenSound.Play();
            await parentOfBottomItemSlots.DOShakeScale(0.3f, 0.5f, 10, 0).AsyncWaitForCompletion();
            parentOfBottomItemSlots.transform.localScale = Vector3.one;

            List<Item> spawnedItems = new();
            for (int i = 0; i < 4; i++)
            {
                await Task.Delay(100);
                spawnedItems.Add(encounter.ItemsSpawner.SpawnFoodItem(encounter, coalData, itemSlots[i]));
            }

            await Task.Delay(500);
            await redTheCook.Say("Stupid old junk.");
            await redTheCook.Say("Have to clean up the mess.");
            await redTheCook.Say("Where is the garbage can?");
            garbageCan.gameObject.SetActive(true);
            await Task.Delay(500);
            await redTheCook.Say("Oh,<pause:0.5> there you are.");
            await WaitForAllItemsToBeDisposed(spawnedItems);
            await redTheCook.Say("Shoo,<pause:0.5> small stinkers.");
        }

        private async Task TeachToManageSpawnedItems(RestaurantEncounter encounter)
        {
            slotsToSpawnFoodParent.SetActive(true);
            effectOnSlotsToSpawnFoodAppear.SetActive(true);
            soundOnSlotsToSpawnFoodAppear.Play();
            await Task.Delay(500);
            await redTheCook.Say("Oh.<pause:0.5> The real game begins.");
            await redTheCook.Say("It's easy.<pause:0.5> This thing spawns items,<pause:0.5> I use them.");
            await redTheCook.Say("And remember,<pause:0.5> don't let it get clogged up.");
            itemSpawnTimer.Init(encounter);

            /*await FixWave(encounter, new List<ItemData>(),
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
                (encounter.DecksManager.GetRandomCustomer(), encounter.DecksManager.GetRandomDish));*/
        }


        private async Task MiniBossDialogue(RestaurantEncounter encounter)
        {
            encounter.Ticker.Pause();
            await HeavyStep(0.25f, 0.25f);
            await Task.Delay(750);
            await redTheCook.Say("*sigh*<pause:1> Everyday in the same time");
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
            await bearMiniBoss.Character.Say("Oh,<pause: 0.5> it's you again, Red <pause:0.5>NOT<pause:0.5> a Cook.");
            await redTheCook.Say("Yarrr,<pause: 0.5> welcome aboard, young sailor.");
            await bearMiniBoss.Character.Say("I don't need your greetings, Red <pause:0.5>NOT<pause:0.5> a Cook.");
            await bearMiniBoss.Character.Say("I need you to work faster, not louder.");
            await redTheCook.Say("Do you know that this is a restaurant for kids?");
            await bearMiniBoss.Character.Say("Pfft.<pause:0.5> This place is full of pirates.");
            await bearMiniBoss.Character.Say("Pirates are too scary for them,<pause:0.5> so <pause:0.5>YEAH,<pause:0.5> it's clearly for adults.");
            await redTheCook.Say("At least you are not too old for this.");
            await bearMiniBoss.Character.Say("I'm at the perfect age for this place, so, <pause:0.5>YEAH.<pause:0.5>");
            blinkSound.Play();
            blinkAnimator.SetTrigger("blink");
            await Task.Delay(1000);
            await bearMiniBoss.Character.Say("And I need some calories to be in the perfect shape.<pause:0.5> So, hurry up, Red <pause:0.5>NOT<pause:0.5> a Cook.");
            bearMiniBoss.gameObject.SetActive(false);
            miniBossSeatPlace.gameObject.SetActive(true);
            encounter.Ticker.SetRegularTickingSpeed();
        }

        private async Task MiniBossWave(RestaurantEncounter encounter)
        {
            await FixWave(encounter, new List<ItemData>(),
                (doggo, encounter.DecksManager.GetRandomDish),
                (kitty, encounter.DecksManager.GetRandomDish),
                (bearMiniBossData, encounter.DecksManager.GetRandomDish),
                (duck, encounter.DecksManager.GetRandomDish));
        }

        private Task WaitForAllItemsToBeDisposed(List<Item> itemsToDispose)
        {
            var tcs = new TaskCompletionSource<bool>();

            var remaining = new HashSet<Item>(itemsToDispose);

            void OnItemDisposed(Item item)
            {
                if (remaining.Contains(item))
                {
                    remaining.Remove(item);
                    if (remaining.Count == 0)
                    {
                        garbageCan.OnItemHasBeenPlaced.RemoveListener(OnItemDisposed);
                        tcs.TrySetResult(true);
                    }
                }
            }

            garbageCan.OnItemHasBeenPlaced.AddListener(OnItemDisposed);
            return tcs.Task;
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

                var tasks = customersToFeed.Select(WaitForCustomerToLeave).ToArray();
                var results = await Task.WhenAll(tasks);

                success = results.All(r => r);
                if (!success)
                    await redTheCook.Say("damn...<pause:1> Ok, let's try again");
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

        public void Complete()
        {
            if (completionSource != null && !completionSource.Task.IsCompleted)
            {
                completionSource.SetResult(true);
            }
        }

        private async void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Complete();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                blinkSound.Play();
                blinkAnimator.SetTrigger("blink");
            }
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