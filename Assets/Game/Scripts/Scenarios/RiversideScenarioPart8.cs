using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class RiversideScenarioPart8 : BaseScenario<RestaurantEncounter>
    {
        [SerializeField] private Character red;
        [SerializeField] private Character adele;
        [SerializeField] private Character boris;
        [SerializeField] private Transform adelesEye;
        [SerializeField] private ParticleSystem borisPoofEffect;
        [SerializeField] private ParticleSystem adelePoofEffect;
        [SerializeField] private List<Character> runningPeople;
        [SerializeField] private List<ParticleSystem> runningPeoplePoofEffects;
        [SerializeField] private AudioSource successSound;
        [SerializeField] private AudioSource farImpactSound;
        [SerializeField] private AudioSource poofSound;
        [SerializeField] private AudioSource sneezeSound;
        [SerializeField] private AudioSource music;

        [Header("Customers")]
        [SerializeField] private CustomerData hog;
        [SerializeField] private CustomerData cow;
        [SerializeField] private CustomerData goat;
        [SerializeField] private CustomerData bull;

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
        }

        protected override async UniTask StartScenarioTyped(RestaurantEncounter encounter)
        {
            await Cutscene();
            encounter.ItemSpawnTimer.SetBlocked(true);
            encounter.GarbageCan.SetBlocked(true);
            await TutorialWave();
            encounter.ItemSpawnTimer.SetBlocked(false);
            encounter.GarbageCan.SetBlocked(false);
            await TheFirstWave();
            await TheSecondWave();
            await TheThirdWave();
        }

        private async UniTask Cutscene()
        {
            red.LookAt(boris.transform);
            await UniTask.Delay(1000);
            await BorisSneeze();

            red.LookAt(runningPeople[0].transform);
            await runningPeople[0].Say("Спасайся кто может!");

            for (int i = 0; i < runningPeople.Count; i++)
            {
                runningPeople[i].gameObject.SetActive(false);
                runningPeoplePoofEffects[i].Play();
                poofSound.Play();
                await UniTask.Delay(250);
            }

            red.LookAt(boris.transform);
            boris.gameObject.SetActive(true);
            borisPoofEffect.Play();
            await BorisSneeze();
            await UniTask.Delay(1000);

            await boris.Say("Охх,<pause:0.5> как здорово,<pause:0.5> у нас гости и вы устроили такой милый пикник!");
            await boris.Say("А меня не позвали, проказники.");
            await BorisSneeze();

            adele.gameObject.SetActive(true);
            adelePoofEffect.Play();
            poofSound.Play();
            await UniTask.Delay(1000);

            await adele.Say("Боря, иди обратно в дом сейчас же!");
            await adele.Say("Мы тебе принесем еды туда.");

            await boris.Say("Обратно в дом?<pause:0.5> Ну уж нетушки!");
            await boris.Say("Я не хочу пропустить все веселье с нашими новыми пушистыми друзьями!");

            await BorisSneeze();
            await UniTask.Delay(1000);

            await adele.Say("Боря,<pause:0.5> умоляю тебя,<pause:0.5> ты сейчас здесь все разнесешь.");
            await boris.Say("Не драматизируй, Адель,<pause:0.5>  не так уж сильно я и чихаю.");

            await BorisSneeze();
            await UniTask.Delay(1000);

            await boris.Say("Я просто посижу с вами здесь, ребята.<pause:0.5> Я никого не побеспокою!");
            await boris.Say("К нам так редко заходят гости!<pause:0.5> Я ни за что не упущу такую возможность!");

            await UniTask.Delay(1000);
            red.LookAt(adelesEye);
            await adele.Say("Слушай сюда, Рыжий.");
            await adele.Say("Делай все что угодно,<pause:0.5> но не давай ему чихнуть.");
            await adele.Say("Если он разойдется, мы это место будем собирать по щепкам!");
            await red.Say("Я не подведу.");
            await red.Say("Клятва гиппопотама.");

            adele.gameObject.SetActive(false);
            adelePoofEffect.Play();
            poofSound.Play();
            await UniTask.Delay(500);

            red.LookAt(null);

            boris.gameObject.SetActive(false);
            borisPoofEffect.Play();
            poofSound.Play();
            await UniTask.Delay(500);
        }

        private async UniTask BorisSneeze()
        {
            farImpactSound.Play();
            sneezeSound.Play();
            Camera.main.ShakeCamera(1);
            await boris.transform.DOScale(1.3f, 0.1f);
            await boris.transform.DOScale(1, 0.1f);
            await UniTask.Delay(800);
        }

        private async UniTask TutorialWave()
        {
            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<UniTask>[] 
                {
                    () => encounter.ItemsOperations.SpawnStartItems(new List<ItemData>() { tea, tea, tea ,tea })
                },

                Customers = new List<QueuedCustomer>
                {
                    new(bull) { OrderFactory = () => tea }
                }
            });
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
                    new(bull),
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
                    new(bull),
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
                    new(bull),
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
                }
            });
        }
    }
}