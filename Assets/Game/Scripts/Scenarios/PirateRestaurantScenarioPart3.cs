using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
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
        [SerializeField] private ItemData popsicleData;
        [SerializeField] private ItemData iceCreamPlateData;
        [SerializeField] private ItemData cherrySyrupData;
        [SerializeField] private ItemData cherryIceCreamPlateData;

        [Header("other links")]
        [SerializeField] private AudioSource poofSound;
        [SerializeField] private AudioSource backgroundMusic;
        [SerializeField] private AudioSource successSound;
        [SerializeField] private ParticleSystem lameJoeAppearParticles;
        [SerializeField] private ParticleSystem sealGirlAppearParticles;

        [SerializeField] List<LocalizedString> dialogueLines;
        private int stringsCounter = 0;
        private int Next => stringsCounter++;

        protected override async UniTask StartScenarioTyped(RestaurantEncounter encounter)
        {
            await IntroSpeech();
            await SealTutorail(encounter);
            await Waves(encounter);
            await SealGirlGoesAway(encounter);
            await LameJoeAppears();
        }

        private async UniTask IntroSpeech()
        {
            await red.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]); 
            await red.Say(dialogueLines[Next]);
        }

        private async UniTask SealTutorail(RestaurantEncounter encounter)
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

            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<UniTask>[] 
                { 
                    () => encounter.ItemsOperations.SpawnStartItems(new List<ItemData>() 
                    {
                        popsicleData,
                        cherrySyrupData,
                        iceCreamPlateData,
                        popsicleData
                    }) 
                },
                Customers = new List<QueuedCustomer>()
                {
                    new(seal) { OrderFactory = () => cherryIceCreamPlateData },
                    new(kitty) { OrderFactory = () => popsicleData }
                },
                CustomersToFeed = 2
            });
        }

        private async UniTask Waves(RestaurantEncounter encounter)
        {
            encounter.ItemSpawnTimer.SetBlocked(false);

            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<UniTask>[] { () => encounter.ItemsOperations.SpawnStartItems() },
                Customers = new List<QueuedCustomer>()
                {
                    new(seal),
                    new(kitty),
                    new(kitty),
                    new(duck),
                    new(doggo),
                    new(seal),
                    new(kitty),
                    new(duck),
                    new(seal),
                    new(kitty),
                    new(kitty),
                    new(duck),
                }
            });

            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<UniTask>[] { () => encounter.ItemsOperations.SpawnStartItems() },
                Customers = new List<QueuedCustomer>()
                {
                    new(seal),
                    new(doggo),
                    new(kitty),
                    new(duck),
                    new(doggo),
                    new(seal),
                    new(doggo),
                    new(doggo),
                    new(seal),
                    new(kitty),
                    new(kitty),
                    new(duck),
                }
            });

            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<UniTask>[] { () => encounter.ItemsOperations.SpawnStartItems() },
                Customers = new List<QueuedCustomer>()
                {
                    new(seal),
                    new(seal),
                    new(doggo),
                    new(duck),
                    new(doggo),
                    new(doggo),
                    new(seal),
                    new(kitty),
                    new(seal),
                    new(kitty),
                    new(kitty),
                    new(duck),
                }
            });
        }

        private async UniTask SealGirlGoesAway(RestaurantEncounter encounter)
        {
            backgroundMusic.DOFade(0, 1);
            encounter.BlockInput();
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
            await UniTask.Delay(3000);
        }

        private async UniTask LameJoeAppears()
        {
            lameJoe.gameObject.SetActive(true);
            red.LookAt(lameJoe.transform);
            await lameJoe.Say(dialogueLines[Next]);
        }

        protected override void InitTyped(RestaurantEncounter encounter) 
        {
            encounter.ItemSpawnTimer.SetBlocked(true);
        }
    }
}