using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [SerializeField] private GameObject customersToFeedPanel;
        [SerializeField] private GameObject nextInLineCustomerPanel;
        [SerializeField] private ParticleSystem particlesOnCustomersToFeedPanelAppear;
        [SerializeField] private WavesInfoPanel wavesInfoPanel;
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

        private int switchConeAndPopsicleCount;

        protected override async Task StartScenarioTyped(RestaurantEncounter encounter)
        {            
            await Task.Delay(500);            

            await Intro(encounter);
            await TeachToFeedCustomers(encounter);
            await TeachToUnderstandHPMechanic(encounter);
            await GarbageCanTutorial(encounter);

            garbageCan.gameObject.SetActive(true);

            await TeachToFuseIngredients(encounter);

            await TeachToWorkWithProgressBar(encounter);
            await TeachToWorkWithTheLineOfCustomers(encounter);

            itemSpawnTimer.SetBlocked(false);

            await TeachToManageSpawnedItems(encounter);
            await MiniBossDialogue(encounter);
            await MiniBossWave(encounter);
            await MiniBossDialogueAfterWave(encounter);
        }

        private async Task Intro(RestaurantEncounter encounter)
        {
            await redTheCook.Say(dialogueLines[0]);
        }

        private async Task TeachToFeedCustomers(RestaurantEncounter encounter)
        {
            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<Task>[] { () => encounter.ItemsOperations.SpawnStartItems(new List<ItemData> { iceCreamConeData, iceCreamConeData }) },
                Customers = new List<(CustomerData, Func<ItemData>)> { (duck, () => iceCreamConeData), (kitty, () => iceCreamConeData) },
                CustomersToFeed = 2
            });

            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<Task>[] { () => encounter.ItemsOperations.SpawnStartItems(new List<ItemData> { popsicleData, iceCreamConeData, popsicleData }) },
                Customers = new List<(CustomerData, Func<ItemData>)> { (duck, () => iceCreamConeData), (kitty, () => popsicleData), (duck, () => popsicleData) },
                CustomersToFeed = 3
            });
        }

        private async Task TeachToUnderstandHPMechanic(RestaurantEncounter encounter)
        {
            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<Task>[] { () => encounter.ItemsOperations.SpawnStartItems(new List<ItemData> { popsicleData, iceCreamConeData }) },
                Customers = new List<(CustomerData, Func<ItemData>)> { (doggo, SwitchConeAndPopsicle) },
                AfterInitSpawn = new Func<Task>[] 
                {
                    LookAtTheFirstSpawnedCustomer(encounter),
                    () => redTheCook.Say(dialogueLines[1]),
                    LookAtTheCursor()
                },
                CustomersToFeed = 1
            });

            await redTheCook.Say(dialogueLines[2].GetLocalizedString());

            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<Task>[] { () => encounter.ItemsOperations.SpawnStartItems(new List<ItemData> { popsicleData, iceCreamConeData, popsicleData, iceCreamConeData }) },
                Customers = new List<(CustomerData, Func<ItemData>)> { (doggo, SwitchConeAndPopsicle), (kitty, () => popsicleData), (duck, () => iceCreamConeData) },
                CustomersToFeed = 3
            });

            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<Task>[] { () => encounter.ItemsOperations.SpawnStartItems(new List<ItemData> { popsicleData, iceCreamConeData, popsicleData, iceCreamConeData }) },
                Customers = new List<(CustomerData, Func<ItemData>)> { (doggo, () => iceCreamConeData), (doggo, () => popsicleData) },
                CustomersToFeed = 2
            });
        }

        private async Task GarbageCanTutorial(RestaurantEncounter encounter)
        {
            brokenSound.Play();
            redTheCook.LookAt(parentOfBottomItemSlots);
            await parentOfBottomItemSlots.DOShakeScale(0.3f, 0.5f, 10, 0).AsyncWaitForCompletion();
            parentOfBottomItemSlots.transform.localScale = Vector3.one;

            List<Item> spawnedItems = new();
            for (int i = 0; i < 4; i++)
            {
                await Task.Delay(100);
                spawnedItems.Add(encounter.ItemsSpawner.SpawnFoodItem(encounter, coalData, encounter.SlotsManager.BottomRowSlots[i]));
            }
            int satietySum = spawnedItems.Sum(foodItem => (foodItem as FoodItem).Satiety);

            await Task.Delay(500);
            await redTheCook.Say(dialogueLines[3].GetLocalizedString());
            await redTheCook.Say(dialogueLines[4].GetLocalizedString());
            await redTheCook.Say(dialogueLines[5].GetLocalizedString());

            garbageCan.gameObject.SetActive(true);
            redTheCook.LookAt(garbageCan.transform);

            await Task.Delay(500);
            await redTheCook.Say(dialogueLines[6].GetLocalizedString());
            redTheCook.LookAt(null);

            await WaitForHungerPointsDisposed(satietySum);

            redTheCook.LookAt(garbageCan.transform);
            await redTheCook.Say(dialogueLines[7].GetLocalizedString());
            redTheCook.LookAt(null);
        }

        private async Task TeachToFuseIngredients(RestaurantEncounter encounter)
        {
            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig
            {
                BeforeWave = new Func<Task>[] { () => encounter.ItemsOperations.SpawnStartItems(new List<ItemData> { cherrySyrupData, iceCreamPlateData }) },
                Customers = new List<(CustomerData, Func<ItemData>)> { (duck, () => cherryIceCreamPlateData) },
                AfterInitSpawn = new Func<Task>[] {
                    LookAtTheFirstSpawnedCustomer(encounter),
                    () => redTheCook.Say(dialogueLines[8]),
                    () => redTheCook.Say(dialogueLines[9]),
                    () => redTheCook.Say(dialogueLines[10]),
                    LookAtTheCursor()
                },
                CustomersToFeed = 1
            });

            await redTheCook.Say(dialogueLines[11].GetLocalizedString());

            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<Task>[] 
                {
                    () => encounter.ItemsOperations.SpawnStartItems(new List<ItemData>
                    {
                        cherrySyrupData,
                        iceCreamPlateData,
                        chocolateSyrupData,
                        iceCreamPlateData
                    })
                },
                Customers = new List<(CustomerData, Func<ItemData>)>
                {
                    (doggo, () => cherryIceCreamPlateData),
                    (kitty, () => chocolateIceCreamPlateData)
                },
                CustomersToFeed = 2
            });

            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<Task>[] {
                () => encounter.ItemsOperations.SpawnStartItems(new List<ItemData>
                    {
                        cherrySyrupData,
                        iceCreamPlateData,
                        iceCreamConeData,
                        popsicleData
                    })
                },
                Customers = new List<(CustomerData, Func<ItemData>)>
                {
                    (doggo, () => cherryIceCreamPlateData),
                    (kitty, () => iceCreamConeData),
                    (duck, () => popsicleData)
                },
                CustomersToFeed = 3
            });
        }

        private async Task TeachToWorkWithProgressBar(RestaurantEncounter encounter)
        {
            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<Task>[]    {
                    () => encounter.ItemsOperations.SpawnStartItems(new List<ItemData> { iceCreamConeData, iceCreamConeData, iceCreamConeData })
                },
                Customers = new List<(CustomerData, Func<ItemData>)>
                {
                    (kitty, () => iceCreamConeData),
                    (duck, () => iceCreamConeData),
                    (kitty, () => iceCreamConeData),
                    (duck, () => iceCreamConeData)
                },
                AfterInitSpawn = new Func<Task>[]
                {
                    () => redTheCook.Say("Обычно мне не нужно обслуживать всех на свете."),
                    () => redTheCook.Say("Достаточно разобраться с основной массой."),
                    () =>
                    {
                        redTheCook.LookAt(customersToFeedPanel.transform);
                        customersToFeedPanel.gameObject.SetActive(true);
                        wavesInfoPanel.RebuildContentSizeFitters();
                        soundOnSlotsToSpawnFoodAppear.Play();
                        particlesOnCustomersToFeedPanelAppear.Play();
                        return Task.Delay(500);
                    },
                    () => redTheCook.Say("Эта железяка показывает сколько посетителей осталось удовлетворить."),
                    LookAtTheCursor()
                },
                CustomersToFeed = 3
            });
        }

        private async Task TeachToWorkWithTheLineOfCustomers(RestaurantEncounter encounter)
        {
            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<Task>[]
                {
                    () => encounter.ItemsOperations.SpawnStartItems(new List<ItemData> { iceCreamConeData, popsicleData, iceCreamConeData, popsicleData })
                },
                Customers = new List<(CustomerData, Func<ItemData>)>
                {
                    (doggo, () => cherryIceCreamPlateData),
                    (doggo, () => cherryIceCreamPlateData),
                    (kitty, () => popsicleData),
                    (kitty, () => iceCreamConeData),
                    (duck, () => popsicleData),
                    (duck, () => iceCreamConeData),
                    (doggo, () => cherryIceCreamPlateData),
                    (doggo, () => cherryIceCreamPlateData),
                },
                AfterInitSpawn = new Func<Task>[]
                {
                    () => redTheCook.Say("Ой-ей.<pause:0.5> Мелюзги набежало так много, что все не влезли."),
                    () => redTheCook.Say("Часть ждет, когда освободится место, чтобы сделать заказ."),
                    () =>
                    {
                        redTheCook.LookAt(nextInLineCustomerPanel.transform);
                        nextInLineCustomerPanel.gameObject.SetActive(true);
                        soundOnSlotsToSpawnFoodAppear.Play();
                        return Task.Delay(500);
                    },
                    () => redTheCook.Say("Эта штука показывает, кто в очереди следующий."),
                    () => redTheCook.Say("Бывает полезно потянуть время,<pause:0.5> но лучше не заигрываться с этим."),
                    () => redTheCook.Say("У клиентов в очереди тоже есть терпение."),
                    LookAtTheCursor()
                },
                CustomersToFeed = 4

            });
        }

        private async Task TeachToManageSpawnedItems(RestaurantEncounter encounter)
        {
            redTheCook.LookAt(slotsToSpawnFoodParent.transform);
            slotsToSpawnFoodParent.SetActive(true);
            effectOnSlotsToSpawnFoodAppear.SetActive(true);
            soundOnSlotsToSpawnFoodAppear.Play();
            await Task.Delay(500);
            await redTheCook.Say(dialogueLines[12].GetLocalizedString());
            await redTheCook.Say(dialogueLines[13].GetLocalizedString());
            await redTheCook.Say(dialogueLines[14].GetLocalizedString());
            redTheCook.LookAt(null);

            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<Task>[] 
                {
                    () => encounter.ItemsOperations.SpawnStartItems(new List<ItemData>
                    { 
                        encounter.DecksManager.GetRandomIngredient(), 
                        encounter.DecksManager.GetRandomIngredient(), 
                        encounter.DecksManager.GetRandomIngredient(),
                        encounter.DecksManager.GetRandomIngredient()
                    }) 
                },
                Customers = new List<(CustomerData, Func<ItemData>)>
                {
                    (doggo, encounter.DecksManager.GetRandomDish),
                    (kitty, encounter.DecksManager.GetRandomDish),
                    (duck, encounter.DecksManager.GetRandomDish),
                    (doggo, encounter.DecksManager.GetRandomDish),
                    (kitty, encounter.DecksManager.GetRandomDish),
                    (duck, encounter.DecksManager.GetRandomDish),
                    (doggo, encounter.DecksManager.GetRandomDish),
                    (kitty, encounter.DecksManager.GetRandomDish),
                    (duck, encounter.DecksManager.GetRandomDish),
                    (doggo, encounter.DecksManager.GetRandomDish),
                    (kitty, encounter.DecksManager.GetRandomDish),
                    (duck, encounter.DecksManager.GetRandomDish),
                    (duck, encounter.DecksManager.GetRandomDish),
                }
            });
        }


        private async Task MiniBossDialogue(RestaurantEncounter encounter)
        {
            encounter.BlockInput();
            regularMusic.Stop();
            encounter.Ticker.Pause();
            redTheCook.LookAt(bearMiniBoss.transform);
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
            redTheCook.LookAt(null);
        }

        private async Task MiniBossWave(RestaurantEncounter encounter)
        {
            bossMusic.Play();
            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<Task>[]
                {
                    () => encounter.ItemsOperations.SpawnStartItems(new List<ItemData>
                    {
                        encounter.DecksManager.GetRandomIngredient(),
                        encounter.DecksManager.GetRandomIngredient(),
                        encounter.DecksManager.GetRandomIngredient(),
                        encounter.DecksManager.GetRandomIngredient()
                    })
                },
                Customers = new List<(CustomerData, Func<ItemData>)>()
                {
                    (bearMiniBossData, encounter.DecksManager.GetRandomDish),
                    (doggo, encounter.DecksManager.GetRandomDish),
                    (kitty, encounter.DecksManager.GetRandomDish),
                    (duck, encounter.DecksManager.GetRandomDish),
                    (doggo, encounter.DecksManager.GetRandomDish),
                    (kitty, encounter.DecksManager.GetRandomDish),
                    (duck, encounter.DecksManager.GetRandomDish),
                    (doggo, encounter.DecksManager.GetRandomDish),
                    (kitty, encounter.DecksManager.GetRandomDish),
                    (duck, encounter.DecksManager.GetRandomDish),
                    (doggo, encounter.DecksManager.GetRandomDish),
                    (kitty, encounter.DecksManager.GetRandomDish),
                }
            });
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
            redTheCook.LookAt(bearMiniBoss.transform);
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
            bearMiniBoss.LeaveSatisfied(true);
            await Task.Delay(1000);
            await redTheCook.Say(dialogueLines[31].GetLocalizedString());
            redTheCook.LookAt(null);
            successSound.Play();
            await Task.Delay(3000);
            hornSound.Play();
            await Task.Delay(3000);
            await redTheCook.Say(dialogueLines[32].GetLocalizedString());
        }

        private ItemData SwitchConeAndPopsicle()
        {
            switchConeAndPopsicleCount++;
            return switchConeAndPopsicleCount % 2 == 0 ? popsicleData : iceCreamConeData;
        }

        private async Task HeavyStep(float soundVolume, float strenght, int shakingValue = 50)
        {
            heavyStepSound.volume = soundVolume;
            heavyStepSound.Play();
            await Camera.main.transform.DOShakePosition(0.3f, strenght, shakingValue).AsyncWaitForCompletion();
            Camera.main.transform.position = new Vector3(0, 0, -10);
        }

        protected override void InitTyped(RestaurantEncounter encounter) 
        {
            encounter.ItemSpawnTimer.SetBlocked(true);
        }

        private Func<Task> LookAtTheFirstSpawnedCustomer(RestaurantEncounter encounter)
        {
            return new Action(() => redTheCook.LookAt(encounter.CustomersManager.Customers[0].transform)).WrapToTask();
        }

        private Func<Task> LookAtTheCursor()
        {
            return new Action(() => redTheCook.LookAt(null)).WrapToTask();
        }
    }
}