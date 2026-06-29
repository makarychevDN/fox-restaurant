using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class RiversideScenarioPart4 : BaseScenario<RestaurantEncounter>
    {
        [SerializeField] private Character red;
        [SerializeField] private AudioSource poofSound;

        [Header("Adele's intro speech setup")]
        [SerializeField] private Character adele;
        [SerializeField] private Transform adeleSprite;
        [SerializeField] private Transform adelesEyes;
        [SerializeField] private ParticleSystem adelesPoofEffect;

        [Header("Ill goat setup")]
        [SerializeField] private Character illGoat;
        [SerializeField] private ParticleSystem illGoatsPoofEffect;

        [Header("Customers")]
        [SerializeField] private CustomerData hog;
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

        [Header("SeatPlaces")]
        [SerializeField] private Table table;

        private RestaurantEncounter encounter;

        protected override void InitTyped(RestaurantEncounter encounter)
        {
            this.encounter = encounter;
            red.LookAt(adelesEyes);
            encounter.ItemSpawnTimer.SetBlocked(true);
        }

        protected override async UniTask StartScenarioTyped(RestaurantEncounter encounter)
        {
            //await UniTask.Delay(1000);
            //await IntroDialogue();
            //await UniTask.Delay(500);            
            //await TutorialWaves();
            encounter.ItemSpawnTimer.SetBlocked(false);
            await TheFirstWave();
            encounter.ItemSpawnTimer.SetBlocked(true);
            await AfterTutorialWavesDialogue();
            await IllGoatTutorial();
            await AfterIllGoatTutoiralDialogue();
            await CureIllGoatTutorial();
            await AfterCureIllGoatDialogue();
        }

        private async UniTask IntroDialogue()
        {
            await adele.Say("И так,<pause:0.5> все наши продукты и травы в твоем распоряжении.");
            await adele.Say("Не заставляй меня пожалеть об этом, оранжевенький.");

            await red.Say("Я справлюсь.");
            await red.Say("Вряд-ли делать лекарственные супчики труднее, чем готовить обычную еду.");

            await adele.Say("Надеюсь, что это и вправду так.");

            adele.gameObject.SetActive(false);
            adelesPoofEffect.Play();
            poofSound.Play();

            red.LookAt(null);

            await UniTask.Delay(1000);
            await red.Say("Щас как дам больно бациллам этим,<pause:0.5> усиков не соберут.");
        }

        private async UniTask TutorialWaves()
        {
            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<UniTask>[]
                {
                    () => encounter.ItemsOperations.SpawnStartItems( new List<ItemData>
                    {
                        mushroom, chicken, soup, soup
                    })
                },
                Customers = new List<QueuedCustomer>
                {
                    new(cow) { OrderFactory = () => mushroomSoup },
                    new(cow) { OrderFactory = () => chickenSoup },
                },
                CustomersToFeed = 2
            });

            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<UniTask>[]
                {
                    () => encounter.ItemsOperations.SpawnStartItems( new List<ItemData>
                    {
                        mushroom, chicken, potRoast, potRoast
                    })
                },
                Customers = new List<QueuedCustomer>
                {
                    new(hog) { OrderFactory = () => mushroomPotRoast },
                    new(hog) { OrderFactory = () => chickenPotRoast },
                },
                CustomersToFeed = 2
            });
        }

        private async UniTask TheFirstWave()
        {
            encounter.ItemSpawnTimer.SetBlocked(false);
            adele.gameObject.SetActive(false);
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
                    new(hog),
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

        private async UniTask AfterTutorialWavesDialogue()
        {
            await red.Say("А это не так уж и сложно");

            UpdateAdelesPosition();
            adele.gameObject.SetActive(true);
            adelesPoofEffect.Play();
            poofSound.Play();
            red.LookAt(adelesEyes);

            await UniTask.Delay(1000);

            await adele.Say("Рано радуешься, оранжевенький.");
            await adele.Say("Все те, кто к тебе только успел прийти не болеют в полной мере.");
            await adele.Say("В худшем случае чувствуют легкое недомогание.");
            await adele.Say("Я тебе привела первого настоящего пациента.");
            await red.Say("Подавайте его сюда.");

            red.LookAt(illGoat.transform);
            illGoat.gameObject.SetActive(true);
            illGoatsPoofEffect.Play();
            poofSound.Play();

            await UniTask.Delay(1500);
            await illGoat.Say("Я<pause:0.75> не буду есть отраву лешего!");
            await illGoat.Say("И даже не проси!");
            await adele.Say("*вздох*<pause:0.75> Будешь, если хочешь выздороветь.");
            await illGoat.Say("Тогда я выбираю болеть!");
            await adele.Say("Нет,<pause:0.5> ты выбираешь выздоравливать, ты<pause:0.5> - неблагодарное недоразумение.");
            await adele.Say("И это не просьба.");
            await illGoat.Say("...");
            await adele.Say("Вот и помалкивай.");

            await UniTask.Delay(1000);
            red.LookAt(adelesEyes);
            await adele.Say("Не обращай внимания, на то, что говорит этот болван.");
            await adele.Say("Он последний день страдает от жара.");
            await adele.Say("И своей болезнью портит жизнь всем окружающим.");
            await adele.Say("Всех, кто сидит рядом с ним будет труднее вылечить.");
            await adele.Say("Покажи, что можешь, оранжевенький.");

            adele.gameObject.SetActive(false);
            adelesPoofEffect.Play();
            poofSound.Play();
            red.LookAt(null);

            await UniTask.Delay(500);

            illGoat.gameObject.SetActive(false);
            illGoatsPoofEffect.Play();
            poofSound.Play();

            await UniTask.Delay(500);
        }

        private async UniTask IllGoatTutorial()
        {
            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<UniTask>[]
                {
                    () => encounter.ItemsOperations.SpawnStartItems(new List<ItemData>{ mushroom, mushroom, potRoast, potRoast })
                },
                Customers = new List<QueuedCustomer>
                {
                    new(cow) { SeatPlace = table.SeatPlaces[0], OrderFactory = () => chickenSoup },
                    new(cow) { SeatPlace = table.SeatPlaces[2], OrderFactory = () => mushroomPotRoast },
                    new(goat) { SeatPlace = table.SeatPlaces[1], OrderFactory = () => chickenSoup},
                },
                AfterInitSpawn = new Func<UniTask>[]
                {
                    LookAtCertainCustomer(encounter, 2),
                    () => red.Say("Ой-ей!"),
                    () => red.Say("Больные клиенты и вправду могут доставить хлопот."),
                    LookAtTheCursor()
                },
                CustomersToFeed = 1
            });
        }

        private async UniTask AfterIllGoatTutoiralDialogue()
        {
            await red.Say("Блин,<pause:0.5> а это было не просто.");

            UpdateAdelesPosition();
            adele.gameObject.SetActive(true);
            adelesPoofEffect.Play();
            poofSound.Play();
            red.LookAt(adelesEyes);

            await UniTask.Delay(1000);

            await adele.Say("А я говорила.");
            await adele.Say("Но жар легко поддается лечению.");
            await adele.Say("В грибной смеси есть ягоды рябины, ему должно полегчать от них.");
            await adele.Say("Попробуй.");

            adele.gameObject.SetActive(false);
            adelesPoofEffect.Play();
            poofSound.Play();
            red.LookAt(null);
            await UniTask.Delay(500);
        }

        private async UniTask CureIllGoatTutorial()
        {
            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<UniTask>[]
                {
                    () => encounter.ItemsOperations.SpawnStartItems(new List<ItemData>{ chicken, mushroom, potRoast, soup })
                },
                Customers = new List<QueuedCustomer>
                {
                    new(goat) { SeatPlace = table.SeatPlaces[0], OrderFactory = () => mushroomPotRoast },
                    new(cow) { SeatPlace = table.SeatPlaces[1], OrderFactory = () => chickenSoup},
                },
                CustomersToFeed = 1
            });
        }

        private async UniTask AfterCureIllGoatDialogue()
        {
            await red.Say("Так,<pause:0.5> кажется,<pause:0.5> я начинаю понимать.");

            UpdateAdelesPosition();
            adele.gameObject.SetActive(true);
            adelesPoofEffect.Play();
            poofSound.Play();
            red.LookAt(adelesEyes);

            await adele.Say("Это хорошо,<pause:0.5> потому что я не планирую с тобой няньчиться весь день.");
            await adele.Say("Зови, если что-то пойдет не по плану.");

            adele.gameObject.SetActive(false);
            adelesPoofEffect.Play();
            poofSound.Play();
            red.LookAt(null);
        }

        private void UpdateAdelesPosition()
        {
            bool isRedOnTheRight = red.transform.position.x > 0;
            float adelesXPosition = isRedOnTheRight ? -6 : 6;

            adele.transform.position = new Vector3(adelesXPosition, adele.transform.position.y, adele.transform.position.z);
            adelesPoofEffect.transform.position = new Vector3(adelesXPosition, adelesPoofEffect.transform.position.y, adelesPoofEffect.transform.position.z);

            adeleSprite.transform.localRotation = Quaternion.Euler(new Vector3
            (
                adele.transform.localRotation.x,
                180 * (!isRedOnTheRight).ToInt(),
                adele.transform.localRotation.z
            ));
        }

        private Func<UniTask> LookAtCertainCustomer(RestaurantEncounter encounter, int customersIndex)
        {
            return new Action(() => red.LookAt(encounter.CustomersManager.Customers[customersIndex].transform)).WrapToTask();
        }

        private Func<UniTask> LookAtTheCursor()
        {
            return new Action(() => red.LookAt(null)).WrapToTask();
        }
    }
}