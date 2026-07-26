using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

    namespace foxRestaurant
{
    public class RiversideScenarioPart6 : BaseScenario<RestaurantEncounter>
    {
        [SerializeField] private Character red;
        [SerializeField] private Character adele;
        [SerializeField] private AudioSource successSound;
        [SerializeField] private AudioSource impactSound;
        [SerializeField] private AudioSource music;

        [Header("Customers")]
        [SerializeField] private CustomerData hog;
        [SerializeField] private CustomerData hurryingHog;
        [SerializeField] private CustomerData cow;
        [SerializeField] private CustomerData goat;

        [Header("Dishes Data")]
        [SerializeField] private ItemData mushroomSoup;
        [SerializeField] private ItemData chickenSoup;
        [SerializeField] private ItemData mushroomPotRoast;
        [SerializeField] private ItemData chickenPotRoast;

        [Header("Ingredients Data")]
        [SerializeField] private ItemData mushroom;
        [SerializeField] private ItemData chicken;
        [SerializeField] private ItemData potRoast;
        [SerializeField] private ItemData soup;
        [SerializeField] private ItemData tea;
        [SerializeField] private ItemData compote;
        [SerializeField] private ItemData pretzel;
        [SerializeField] private ItemData bigCoal;
        [SerializeField] private ItemData coal;

        [Header("SeatPlaces")]
        [SerializeField] private Table table;

        private RestaurantEncounter encounter;


        protected override void InitTyped(RestaurantEncounter encounter)
        {
            this.encounter = encounter;
            encounter.ItemSpawnTimer.SetBlocked(true);
            encounter.GarbageCan.SetBlocked(true);
        }

        protected override async UniTask StartScenarioTyped(RestaurantEncounter encounter)
        {            
            await UniTask.Delay(1000);
            await red.Say("Hopefully this isn't some kind of cruel joke.".Locailze());
            await TeachToUsePretzel();
            await red.Say("Well, I'll be damned.".Locailze());
            await red.Say("It actually works!".Locailze());
            encounter.GarbageCan.SetBlocked(false);
            await TaechToHeatOven();
            await red.Say("With this oven, I'm not afraid of anyone!".Locailze());
            encounter.ItemSpawnTimer.SetBlocked(false);
            await TheFirstWave();
            await TheSecondWave();
            await TheThirdWave();
            music.DOFade(0, 2);
            await UniTask.Delay(1000);
            await red.Say("I'm unstoppable!".Locailze());
            successSound.Play();
            await UniTask.Delay(3000);
            impactSound.Play();
            await Camera.main.ShakeCamera(0.5f);
            await UniTask.Delay(500);
            await red.Say("What the heck is that?".Locailze());
            await red.Say("Well,<pause:0.5> whatever all that noise is about, it's not my problem.".Locailze());
            await red.Say("Especially when I'm on a break.".Locailze());
        }

        private async UniTask TeachToUsePretzel()
        {
            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<UniTask>[]
                {
                    () => encounter.ItemsOperations.SpawnStartItems(new List<ItemData>()
                    {
                        compote,
                        compote,
                        mushroom,
                        soup
                    }),
                },
                Customers = new List<QueuedCustomer>()
                {
                    new(hog) { OrderFactory = () => compote, SeatPlace = table.SeatPlaces[3] },
                    new(hog) { OrderFactory = () => compote, SeatPlace = table.SeatPlaces[1] },
                    new(goat) { OrderFactory = () => chickenPotRoast, PretzelOrderHandler = _ => mushroomSoup, SeatPlace = table.SeatPlaces[2] },
                },
                AfterInitSpawn = new Func<UniTask>[]
                {
                    TeachToUsePretzelMonologue
                },

                CustomersToFeed = 2,
            });
        }

        private async UniTask TeachToUsePretzelMonologue()
        {
            red.LookAt(encounter.CustomersManager.Customers[2].transform);
            await red.Say("He's doing it again.".Locailze());
            encounter.GarbageCan.SpawnPretzel();
            red.LookAt(encounter.GarbageCan.transform);
            await UniTask.Delay(500);
            await red.Say("Alright,<pause:0.5> this time I'll give him a pretzel.".Locailze());
            await red.Say("And he'll magically change his mind.".Locailze());
            red.LookAt(null);
            await red.Say("t<volume:0>.<pause:0.5>.<pause:0.5>.<pause:0.5><volume:1> That sounded so silly.".Locailze());
        }

        private async UniTask TaechToHeatOven()
        {
            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<UniTask>[]
                {
                    () => encounter.ItemsOperations.SpawnStartItems(new List<ItemData>()
                    {
                        compote,
                    }),
                },
                Customers = new List<QueuedCustomer>()
                {
                    new(hurryingHog) { OrderFactory = () => tea, PretzelOrderHandler = _ => compote },
                },
                AfterInitSpawn = new Func<UniTask>[]
                {
                    TaechToHeatOvenMonologue
                },

                CustomersToFeed = 1,
            });
        }

        private async UniTask TaechToHeatOvenMonologue()
        {
            red.LookAt(encounter.CustomersManager.Customers[0].transform);
            await red.Say("Whoa.<pause:0.5> This piggy is really impatient.".Locailze());
            await red.Say("To serve him in time, I'll have to fire up the oven.".Locailze());
            var spawnedCoal = encounter.ItemsSpawner.SpawnFoodItem(encounter, coal, encounter.SlotsManager.BottomRowSlots[3]);
            (spawnedCoal as FoodItem).SetSatiety(6);
            red.LookAt(spawnedCoal.transform);
            await UniTask.Delay(1000);
            await red.Say("Yeah,<pause:0.5> this should work.".Locailze());
            red.LookAt(null);
        }

        private async UniTask TheFirstWave()
        {
            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<UniTask>[]
                {
                    () => encounter.ItemsOperations.SpawnStartItems()
                },

                Customers = new List<QueuedCustomer>
                {
                    new(hog),
                    new(cow),
                    new(goat),
                    new(cow),
                    new(hog),
                    new(cow),
                    new(hog),
                    new(cow),
                    new(hog),
                    new(cow),
                    new(hog),
                    new(cow),
                }
            });
        }

        private async UniTask TheSecondWave()
        {
            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<UniTask>[]
                {
                    () => encounter.ItemsOperations.SpawnStartItems()
                },

                Customers = new List<QueuedCustomer>
                {
                    new(hog),
                    new(cow),
                    new(goat),
                    new(cow),
                    new(hog),
                    new(goat),
                    new(hog),
                    new(cow),
                    new(hog),
                    new(cow),
                    new(hog),
                    new(cow),
                }
            });
        }

        private async UniTask TheThirdWave()
        {
            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<UniTask>[]
                {
                    () => encounter.ItemsOperations.SpawnStartItems()
                },

                Customers = new List<QueuedCustomer>
                {
                    new(hog),
                    new(goat),
                    new(goat),
                    new(cow),
                    new(hog),
                    new(goat),
                    new(hog),
                    new(cow),
                    new(hog),
                    new(cow),
                    new(hog),
                    new(cow),
                }
            });
        }
    }
}