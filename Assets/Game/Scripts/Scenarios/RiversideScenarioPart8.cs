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
            await runningPeople[0].Say("Run for your lives!".Locailze());

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

            await boris.Say("Oooh,<pause:0.5> how wonderful!<pause:0.5> We have guests, and you've all put together such a lovely little picnic!".Locailze());
            await boris.Say("And nobody invited me, you rascals.".Locailze());
            await BorisSneeze();

            adele.gameObject.SetActive(true);
            adelePoofEffect.Play();
            poofSound.Play();
            await UniTask.Delay(1000);

            await adele.Say("Boris, go back home now!".Locailze());
            await adele.Say("We'll bring your food to you.".Locailze());

            await boris.Say("Go back inside?<pause:0.5> Oh, no way!".Locailze());
            await boris.Say("I'm not missing all the fun with our new fluffy friends!".Locailze());

            await BorisSneeze();
            await UniTask.Delay(1000);

            await adele.Say("Boris,<pause:0.5> I'm begging you,<pause:0.5> you'll tear this whole place apart.".Locailze());
            await boris.Say("Don't be so dramatic, Adele.<pause:0.5> I don't sneeze that hard.".Locailze());

            await BorisSneeze();
            await UniTask.Delay(1000);

            await boris.Say("I'll just sit here with you guys.<pause:0.5> I won't bother anyone!".Locailze());
            await boris.Say("We get visitors so rarely!<pause:0.5> There's no way I'm missing this!".Locailze());

            await UniTask.Delay(1000);
            red.LookAt(adelesEye);
            await adele.Say("Listen carefully, Red.".Locailze());
            await adele.Say("Do whatever it takes,<pause:0.5> but don't let him sneeze.".Locailze());
            await adele.Say("If he does too hard, we have to rebuild this place from the ruins!".Locailze());
            await red.Say("I won't let you down.");
            await red.Say("The Hippopotamus Oath.".Locailze());

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
                    new(bull),
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
                    new(bull),
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
                    new(bull),
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