using DG.Tweening;
using foxRestaurant;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization;

namespace foxRestaurant
{
    public class PirateRestaurantScenarioPart5 : BaseScenario<RestaurantEncounter>
    {
        [Header("characters")]
        [SerializeField] private Character red;
        [SerializeField] private Character lameJoe;
        [SerializeField] private Character capitan;
        [SerializeField] private List<CharacterParticlePair> talkingKidsSetup;
        [SerializeField] private List<Character> kidsOnTheStreet;
        [SerializeField] private List<GameObject> crowdPacks;
        [SerializeField] private List<GameObject> silhouettes;

        [Header("customers data")]
        [SerializeField] private CustomerData doggo;
        [SerializeField] private CustomerData kitty;
        [SerializeField] private CustomerData duck;
        [SerializeField] private CustomerData seal;

        [Header("decorations")]
        [SerializeField] private GameObject cartoon;
        [SerializeField] private GameObject capitanCallsCartoon;
        [SerializeField] private Transform curtains;
        [SerializeField] private Transform additionalSpeakers;
        [SerializeField] private Transform screen;
        [SerializeField] private AudioSource poofSound;
        [SerializeField] private AudioSource additionalSpeakersMovingSound;
        [SerializeField] private AudioSource vhsStartsSound;
        [SerializeField] private AudioSource vhsArtifactsSound;
        [SerializeField] private AudioSource microphoneArtifactsSound;
        [SerializeField] private AudioSource curtainsSound;
        [SerializeField] private AudioSource backgroundMusic;
        [SerializeField] private AudioSource earthQuakeSound;
        [SerializeField] private ParticleSystem lameJoeAppearParticles;

        [Header("dialogues setup")]
        [SerializeField] List<LocalizedString> dialogueLines;

        private int stringsCounter = 0;
        private int Next => stringsCounter++;

        protected override async Task StartScenarioTyped(RestaurantEncounter encounter)
        {
            capitan.OnEmote.AddListener(ShakeCameraOnce);

            await CurtainsAppears();
            await IntroSpeech();
            await Task.Delay(1000);
            await CartoonScreenAppearsAnimation();
            await Waves(encounter);
            encounter.BlockInput();
            await DialogueAfterWaves();
            await Task.Delay(2000);
            await CapitanAppears();
            await CrowdAppears();
        }

        private async Task CurtainsAppears()
        {
            curtainsSound.Play();
            await curtains.DOLocalMove(new Vector3(0, 15), 1.5f).SetEase(Ease.InExpo).AsyncWaitForCompletion();
        }

        private async Task IntroSpeech()
        {
            await red.Say(dialogueLines[Next]);

            lameJoe.gameObject.SetActive(true);
            lameJoeAppearParticles.Play();
            poofSound.Play();
            red.LookAt(lameJoe.transform);
            await Task.Delay(1500);

            foreach (var pack in talkingKidsSetup)
            {
                pack.character.gameObject.SetActive(true);
                pack.particle.Play();
                pack.poofSound.Play();
                await Task.Delay(200);
            }

            await talkingKidsSetup[2].character.Say(dialogueLines[Next]);
            await talkingKidsSetup[3].character.Say(dialogueLines[Next]);
            await talkingKidsSetup[1].character.Say(dialogueLines[Next]);
            await lameJoe.Say(dialogueLines[Next]);
            await talkingKidsSetup[3].character.Say(dialogueLines[Next]);
            await talkingKidsSetup[1].character.Say(dialogueLines[Next]);
            await lameJoe.Say(dialogueLines[Next]);
            await lameJoe.Say(dialogueLines[Next]);
            await talkingKidsSetup[2].character.Say(dialogueLines[Next]);
            await talkingKidsSetup[3].character.Say(dialogueLines[Next]);
            await talkingKidsSetup[0].character.Say(dialogueLines[Next]);
            await lameJoe.Say(dialogueLines[Next]);
            await talkingKidsSetup[3].character.Say(dialogueLines[Next]);


            foreach (var pack in talkingKidsSetup)
            {
                pack.character.gameObject.SetActive(false);
                pack.particle.Play();
                pack.poofSound.Play();
                await Task.Delay(200);
            }

            await red.Say(dialogueLines[Next]);
            await lameJoe.Say(dialogueLines[Next]);
            red.LookAt(null);
            await red.Say(dialogueLines[Next]);
        }

        private async Task CartoonScreenAppearsAnimation()
        {
            vhsStartsSound.Play();
            await screen.DOLocalMove(new Vector3(0, 7.3f), 0.6f).AsyncWaitForCompletion();
            await BlinkGameObjectNTimes(cartoon);
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

        private async Task DialogueAfterWaves()
        {
            foreach(var kidSetup in talkingKidsSetup)
            {
                kidSetup.character.transform.parent = kidSetup.scriptSeatplace.transform;
                kidSetup.character.transform.localPosition = new Vector2(-0f, 2f);
                kidSetup.character.transform.localScale = Vector3.one;
                kidSetup.character.gameObject.SetActive(true);
                kidSetup.particle.transform.position = kidSetup.character.transform.position;
                kidSetup.particle.Play();
                kidSetup.poofSound.Play();
                await Task.Delay(200);
            }

            await red.Say(dialogueLines[Next]);
            await talkingKidsSetup[2].character.Say(dialogueLines[Next]);
            await lameJoe.Say(dialogueLines[Next]);
            await talkingKidsSetup[1].character.Say(dialogueLines[Next]);
            await talkingKidsSetup[2].character.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
        }

        private async Task CapitanAppears()
        {
            vhsArtifactsSound.Play();
            backgroundMusic.Stop();
            await BlinkGameObjectNTimes(capitanCallsCartoon, 21);
            await talkingKidsSetup[2].character.Say(dialogueLines[Next]);
            await talkingKidsSetup[1].character.Say(dialogueLines[Next]);
            await capitan.Say(dialogueLines[Next]);
            await capitan.Say(dialogueLines[Next]);
            curtainsSound.Play();
            await curtains.DOLocalMove(new Vector3(0, 22.5f), 1.5f).AsyncWaitForCompletion();
            await Task.Delay(1000);
            additionalSpeakersMovingSound.Play();
            await additionalSpeakers.DOLocalMove(new Vector3(0, 8.5f), 1.75f).AsyncWaitForCompletion();
            microphoneArtifactsSound.Play();
            await Task.Delay(2000);
            await capitan.Say(dialogueLines[Next]);
            additionalSpeakersMovingSound.Play();
            additionalSpeakers.DOLocalMove(new Vector3(0, 14.2f), 1.75f);
            await capitan.Say(dialogueLines[Next]);
            screen.DOLocalMove(new Vector3(0, 15.2f), 1.75f).SetEase(Ease.InExpo);
        }

        private async Task CrowdAppears()
        {
            earthQuakeSound.Play();
            await Task.Delay(1000);
            Tweener shakingCameraLoop = Camera.main.SetCameraShakingLoopAnimation(0.1f);
            await kidsOnTheStreet[0].Say(dialogueLines[Next]);
            Camera.main.UpdateCameraShakingLoopAnimation(ref shakingCameraLoop, 0.25f);
            await kidsOnTheStreet[1].Say(dialogueLines[Next]);
            Camera.main.UpdateCameraShakingLoopAnimation(ref shakingCameraLoop, 0.5f);
            await red.Say(dialogueLines[Next]);

            int crowdPacksAmount = 9;
            int silhouettesCount = 0;
            for (int i = 0; i < crowdPacksAmount; i++)
            {
                Camera.main.UpdateCameraShakingLoopAnimation(ref shakingCameraLoop, 0.5f + i * 0.1f);
                crowdPacks[i].SetActive(true);
                poofSound.Play();

                if (i >= crowdPacksAmount - silhouettes.Count)
                {
                    silhouettes[silhouettesCount].SetActive(true);
                    silhouettesCount++;
                }

                await Task.Delay(500);
            }

            if (shakingCameraLoop != null)
            {
                shakingCameraLoop.Kill();
                shakingCameraLoop = null;
            }
        }

        private async Task BlinkGameObjectNTimes(GameObject go, int n = 5)
        {
            for (int i = 0; i < n; i++)
            {
                go.SetActive(!go.activeSelf);
                await Task.Delay(50);
            }
        }

        [System.Serializable]
        public class CharacterParticlePair
        {
            public Character character;
            public ParticleSystem particle;
            public AudioSource poofSound;
            public Transform scriptSeatplace;
        }

        private async void ShakeCameraOnce(string key)
        {
            if (key != "shake")
                return;

            await Camera.main.transform.DOShakePosition(0.3f, 0.25f, 50).AsyncWaitForCompletion();
            Camera.main.transform.position = new Vector3(0, 0, -10);
        }

        protected override void InitTyped(RestaurantEncounter encounter)
        {
            encounter.ItemSpawnTimer.SetBlocked(true);
        }
    }
}