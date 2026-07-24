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
            await red.Say("Надеюсь, это не какая-то злая шутка.");
            await TeachToUsePretzel();
            await red.Say("Вот это да.");
            await red.Say("Оно и вправду работает!");
            encounter.GarbageCan.SetBlocked(false);
            await TaechToHeatOven();
            await red.Say("Да с этой печкой мне сам черт не враг!");
            encounter.ItemSpawnTimer.SetBlocked(false);
            //await TheFirstWave();
            //await TheSecondWave();
            //await TheThirdWave();
            music.DOFade(0, 2);
            await UniTask.Delay(1000);
            await red.Say("Я неостановим!");
            successSound.Play();
            await UniTask.Delay(3000);
            impactSound.Play();
            await Camera.main.ShakeCamera(0.5f);
            await UniTask.Delay(500);
            await red.Say("Да чтож такое?");
            await red.Say("Впрочем,<pause:0.5> чем бы они там ни шумели - это не моя проблема.");
            await red.Say("Особенно, когда у меня перерыв.");
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
            await red.Say("Опять он за свое.");
            encounter.GarbageCan.SpawnPretzel();
            red.LookAt(encounter.GarbageCan.transform);
            await UniTask.Delay(500);
            await red.Say("Хорошо,<pause:0.5> а сейчас я дам ему кренделек.");
            await red.Say("И он как по волшебству поменяет свое мнение.");
            red.LookAt(null);
            await red.Say("<volume:0>.<pause:0.5>.<pause:0.5>.<pause:0.5><volume:1> Как же глупенько это прозвучало.");
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
            await red.Say("Ой-ей.<pause:0.5> Этот хряк какой-то нетерпеливый");
            await red.Say("Чтобы успеть его обслужить мне придется растопить печь,<pause:0.5> да погорячее.");
            var spawnedCoal = encounter.ItemsSpawner.SpawnFoodItem(encounter, coal, encounter.SlotsManager.BottomRowSlots[3]);
            (spawnedCoal as FoodItem).SetSatiety(6);
            red.LookAt(spawnedCoal.transform);
            await UniTask.Delay(1000);
            await red.Say("Да,<pause:0.5> это подойдет.");
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
                    new(goat),
                    new(cow),
                    new(hog),
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
                    new(goat),
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
                    new(goat),
                    new(cow),
                    new(goat),
                    new(cow),
                    new(hog),
                    new(goat),
                    new(goat),
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