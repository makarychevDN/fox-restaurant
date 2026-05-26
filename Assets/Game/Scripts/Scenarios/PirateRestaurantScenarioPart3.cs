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

        protected override async Task StartScenarioTyped(RestaurantEncounter encounter)
        {
            await IntroSpeech();
            await SealTutorail(encounter);
            await Waves(encounter);
            await SealGirlGoesAway(encounter);
            await LameJoeAppears();
        }

        private async Task IntroSpeech()
        {
            await red.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]); 
            await red.Say(dialogueLines[Next]);
        }

        private async Task SealTutorail(RestaurantEncounter encounter)
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
                BeforeWave = new Func<Task>[] 
                { 
                    () => encounter.ItemsOperations.SpawnStartItems(new List<ItemData>() 
                    {
                        popsicleData,
                        cherrySyrupData,
                        iceCreamPlateData,
                        popsicleData
                    }) 
                },
                Customers = new List<(CustomerData, Func<ItemData>)>()
                {
                    (seal, () => cherryIceCreamPlateData),
                    (kitty, () => popsicleData)
                },
                CustomersToFeed = 2
            });
        }

        private async Task Waves(RestaurantEncounter encounter)
        {
            encounter.ItemSpawnTimer.SetBlocked(false);

            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<Task>[] { () => encounter.ItemsOperations.SpawnStartItems() },
                Customers = new List<(CustomerData, Func<ItemData>)>()
                {
                    (seal, () => encounter.DecksManager.GetRandomDish()),
                    (kitty, () => encounter.DecksManager.GetRandomDish()),
                    (kitty, () => encounter.DecksManager.GetRandomDish()),
                    (duck, () => encounter.DecksManager.GetRandomDish()),
                    (doggo, () => encounter.DecksManager.GetRandomDish()),
                    (seal, () => encounter.DecksManager.GetRandomDish()),
                    (kitty, () => encounter.DecksManager.GetRandomDish()),
                    (duck, () => encounter.DecksManager.GetRandomDish()),
                    (seal, () => encounter.DecksManager.GetRandomDish()),
                    (kitty, () => encounter.DecksManager.GetRandomDish()),
                    (kitty, () => encounter.DecksManager.GetRandomDish()),
                    (duck, () => encounter.DecksManager.GetRandomDish()),
                }
            });

            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<Task>[] { () => encounter.ItemsOperations.SpawnStartItems() },
                Customers = new List<(CustomerData, Func<ItemData>)>()
                {
                    (seal, () => encounter.DecksManager.GetRandomDish()),
                    (doggo, () => encounter.DecksManager.GetRandomDish()),
                    (kitty, () => encounter.DecksManager.GetRandomDish()),
                    (duck, () => encounter.DecksManager.GetRandomDish()),
                    (doggo, () => encounter.DecksManager.GetRandomDish()),
                    (seal, () => encounter.DecksManager.GetRandomDish()),
                    (doggo, () => encounter.DecksManager.GetRandomDish()),
                    (doggo, () => encounter.DecksManager.GetRandomDish()),
                    (seal, () => encounter.DecksManager.GetRandomDish()),
                    (kitty, () => encounter.DecksManager.GetRandomDish()),
                    (kitty, () => encounter.DecksManager.GetRandomDish()),
                    (duck, () => encounter.DecksManager.GetRandomDish()),
                }
            });

            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<Task>[] { () => encounter.ItemsOperations.SpawnStartItems() },
                Customers = new List<(CustomerData, Func<ItemData>)>()
                {
                    (seal, () => encounter.DecksManager.GetRandomDish()),
                    (seal, () => encounter.DecksManager.GetRandomDish()),
                    (doggo, () => encounter.DecksManager.GetRandomDish()),
                    (duck, () => encounter.DecksManager.GetRandomDish()),
                    (doggo, () => encounter.DecksManager.GetRandomDish()),
                    (doggo, () => encounter.DecksManager.GetRandomDish()),
                    (seal, () => encounter.DecksManager.GetRandomDish()),
                    (kitty, () => encounter.DecksManager.GetRandomDish()),
                    (seal, () => encounter.DecksManager.GetRandomDish()),
                    (kitty, () => encounter.DecksManager.GetRandomDish()),
                    (kitty, () => encounter.DecksManager.GetRandomDish()),
                    (duck, () => encounter.DecksManager.GetRandomDish()),
                }
            });
        }

        private async Task SealGirlGoesAway(RestaurantEncounter encounter)
        {
            backgroundMusic.DOFade(0, 1);
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
            await Task.Delay(3000);
        }

        private async Task LameJoeAppears()
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