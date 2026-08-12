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
        [SerializeField] private ItemData tea;
        [SerializeField] private ItemData compote;

        [Header("SeatPlaces")]
        [SerializeField] private Table table;

        private RestaurantEncounter encounter;

        protected override void InitTyped(RestaurantEncounter encounter)
        {
            this.encounter = encounter;
            red.LookAt(adelesEyes);
            encounter.ItemSpawnTimer.SetBlocked(true);
            encounter.GarbageCan.SetBlocked(true);
        }

        protected override async UniTask StartScenarioTyped(RestaurantEncounter encounter)
        {
            await UniTask.Delay(1000);
            await IntroDialogue();
            await UniTask.Delay(500);            
            await TutorialWaves();
            encounter.ItemSpawnTimer.SetBlocked(false);
            encounter.GarbageCan.SetBlocked(false);

            await TheZerothWave();
            encounter.ItemSpawnTimer.SetBlocked(true);
            encounter.GarbageCan.SetBlocked(true);
            await AfterTutorialWavesDialogue();
            await IllGoatTutorial();
            await AfterIllGoatTutoiralDialogue();
            await CureIllGoatTutorial();
            await AfterCureIllGoatDialogue();
            encounter.ItemSpawnTimer.SetBlocked(false);
            encounter.GarbageCan.SetBlocked(false);
            await TheFirstWave();
            await TheSecondWave();
            await TheThirdWave();
            await AllTheGoats();
        }

        private async UniTask IntroDialogue()
        {
            await adele.Say("Alright,<pause:0.5> all our ingredients and herbs are at your disposal.".Locailze());
            await adele.Say("Don't make me regret this, orange one.".Locailze());

            await red.Say("I've got this.".Locailze());
            await red.Say("Making medicinal soup can't be that much harder than cooking regular food.".Locailze());

            await adele.Say("I certainly hope you're right.".Locailze());

            adele.gameObject.SetActive(false);
            adelesPoofEffect.Play();
            poofSound.Play();

            red.LookAt(null);

            await UniTask.Delay(1000);
            await red.Say("I'm about to give those germs such a beating,<pause:0.5> they won't know what hit 'em.".Locailze());
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

        private async UniTask TheZerothWave()
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
            await red.Say("Hey, this isn't so hard.".Locailze());

            UpdateAdelesPosition();
            adele.gameObject.SetActive(true);
            adelesPoofEffect.Play();
            poofSound.Play();
            red.LookAt(adelesEyes);

            await UniTask.Delay(1000);

            await adele.Say("Don't celebrate too soon, orange one.".Locailze());
            await adele.Say("Everyone who's come to you so far isn't really sick yet.".Locailze());
            await adele.Say("They're just feeling a little under the weather.".Locailze());
            await adele.Say("I've brought you your first real patient.".Locailze());
            await red.Say("Send them in.".Locailze());

            red.LookAt(illGoat.transform);
            illGoat.gameObject.SetActive(true);
            illGoatsPoofEffect.Play();
            poofSound.Play();

            await UniTask.Delay(1500);
            await illGoat.Say("I'm<pause:0.75> not eating Leshy's poison!".Locailze());
            await red.Say("I'm not Leshy.".Locailze());
            await illGoat.Say("And I'm not listening to him either!".Locailze());
            await adele.Say("*sigh*<pause:0.75> You will if you want to get better.".Locailze());
            await illGoat.Say("Then I choose to stay sick!".Locailze());
            await adele.Say("No,<pause:0.5> you choose to get better, you<pause:0.5> ungrateful jackass.".Locailze());
            await adele.Say("And that's not a request.".Locailze());
            await illGoat.Say("...".Locailze());
            await adele.Say("Good. Then keep your mouth shut.".Locailze());

            await UniTask.Delay(1000);
            red.LookAt(adelesEyes);
            await adele.Say("Don't pay any attention to the nonsense this idiot says.".Locailze());
            await adele.Say("He's been burning up with a fever since yesterday.".Locailze());
            await adele.Say("And with his illness, he spoils the lives of everyone around him.".Locailze());
            await adele.Say("Anyone sitting next to him will be harder to cure.".Locailze());
            await adele.Say("Show me what you've got, orange one.".Locailze());

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
                    () => encounter.ItemsOperations.SpawnStartItems(new List<ItemData>{ compote, compote, compote, compote })
                },
                Customers = new List<QueuedCustomer>
                {
                    new(hog) { SeatPlace = table.SeatPlaces[0], OrderFactory = () => compote },
                    new(hog) { SeatPlace = table.SeatPlaces[2], OrderFactory = () => compote },
                    new(goat) { SeatPlace = table.SeatPlaces[1], OrderFactory = () => chickenSoup},
                },
                AfterInitSpawn = new Func<UniTask>[]
                {
                    LookAtCertainCustomer(encounter, 2),
                    () => red.Say("Whoa!".Locailze()),
                    () => red.Say("Sick customers really are a pain to deal with.".Locailze()),
                    LookAtTheCursor()
                },
                CustomersToFeed = 2
            });
        }

        private async UniTask AfterIllGoatTutoiralDialogue()
        {
            await red.Say("Damn,<pause:0.5> that wasn't easy.".Locailze());

            UpdateAdelesPosition();
            adele.gameObject.SetActive(true);
            adelesPoofEffect.Play();
            poofSound.Play();
            red.LookAt(adelesEyes);

            await UniTask.Delay(1000);

            await adele.Say("Told you so.".Locailze());
            await adele.Say("But fever's one of the easier illnesses to treat.".Locailze());
            await adele.Say("The mushroom mix contains rowan berries. They should help him feel better.".Locailze());
            await adele.Say("Give it a try.".Locailze());

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
                    () => encounter.ItemsOperations.SpawnStartItems(new List<ItemData>{ compote, mushroom, potRoast, compote })
                },
                Customers = new List<QueuedCustomer>
                {
                    new(hog) { SeatPlace = table.SeatPlaces[0], OrderFactory = () => compote },
                    new(hog) { SeatPlace = table.SeatPlaces[2], OrderFactory = () => compote },
                    new(goat) { SeatPlace = table.SeatPlaces[1], OrderFactory = () => mushroomPotRoast},
                },
                CustomersToFeed = 2
            });
        }

        private async UniTask AfterCureIllGoatDialogue()
        {
            await red.Say("Alright,<pause:0.5> I think<pause:0.5> I'm starting to get it.".Locailze());

            UpdateAdelesPosition();
            adele.gameObject.SetActive(true);
            adelesPoofEffect.Play();
            poofSound.Play();
            red.LookAt(adelesEyes);

            await adele.Say("Good,<pause:0.5> because I don't intend to babysit you all day.".Locailze());
            await adele.Say("Call me if something goes wrong.".Locailze());

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

        private async UniTask AllTheGoats()
        {
            encounter.ItemSpawnTimer.SetBlocked(true);
            encounter.Ticker.Pause();
            encounter.ItemsOperations.ClearAllTheItems();
            red.LookAt(illGoat.transform);

            await UniTask.Delay(250);
            encounter.CustomerSpawner.TryToSpawnCustomer(goat, () => chickenPotRoast);
            await UniTask.Delay(250);
            encounter.CustomerSpawner.TryToSpawnCustomer(goat, () => chickenSoup);
            await UniTask.Delay(250);
            encounter.CustomerSpawner.TryToSpawnCustomer(goat, () => tea);
            await UniTask.Delay(250);
            encounter.CustomerSpawner.TryToSpawnCustomer(goat, () => compote);
            await UniTask.Delay(1000);
            
            await red.Say("t<volume:0>.<pause:0.5>.<pause:0.5>.<pause:0.5><volume:1> No, this is just ridiculous!".Locailze());
            await red.Say("I can't work under these conditions.".Locailze());
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