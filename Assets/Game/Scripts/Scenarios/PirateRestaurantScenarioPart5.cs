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

        [Header("other links")]
        [SerializeField] private ItemSlot garbageCan;
        [SerializeField] private ItemSpawnTimer itemSpawnTimer;
        [SerializeField] private GameObject slotsToSpawnFoodParent;
        [SerializeField] private GameObject effectOnSlotsToSpawnFoodAppear;

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

        [Header("setup")]
        [SerializeField] LocalizedString waveIsFailedLine;

        private List<Customer> customersToFeed = new();
        private List<ItemSlot> itemSlots;

        [SerializeField] List<LocalizedString> dialogueLines;
        private int stringsCounter = 0;
        private int Next => stringsCounter++;

        protected override async Task StartScenarioTyped(RestaurantEncounter encounter)
        {
            itemSlots = encounter.SlotsManager.Slots.Where(slot => slot.RequiredItemsType == ItemType.Food && slot.gameObject.activeSelf).ToList();
            capitan.OnEmote.AddListener(ShakeCameraOnce);

            await CurtainsAppears();
            await IntroSpeech();
            await Task.Delay(1000);
            await CartoonScreenAppearsAnimation();
            await Waves(encounter);
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
            garbageCan.gameObject.SetActive(true);
            slotsToSpawnFoodParent.SetActive(true);
            effectOnSlotsToSpawnFoodAppear.SetActive(true);
            itemSpawnTimer.Init(encounter);

            await FixWave(encounter, new List<ItemData>() { },
                (seal, () => encounter.DecksManager.GetRandomDish()),
                (kitty, () => encounter.DecksManager.GetRandomDish()),
                (kitty, () => encounter.DecksManager.GetRandomDish()),
                (duck, () => encounter.DecksManager.GetRandomDish()));

            await FixWave(encounter, new List<ItemData>() { },
                (seal, () => encounter.DecksManager.GetRandomDish()),
                (kitty, () => encounter.DecksManager.GetRandomDish()),
                (doggo, () => encounter.DecksManager.GetRandomDish()),
                (duck, () => encounter.DecksManager.GetRandomDish()));

            await FixWave(encounter, new List<ItemData>() { },
                (seal, () => encounter.DecksManager.GetRandomDish()),
                (doggo, () => encounter.DecksManager.GetRandomDish()),
                (doggo, () => encounter.DecksManager.GetRandomDish()),
                (duck, () => encounter.DecksManager.GetRandomDish()));

            await FixWave(encounter, new List<ItemData>() { },
                (seal, () => encounter.DecksManager.GetRandomDish()),
                (seal, () => encounter.DecksManager.GetRandomDish()),
                (doggo, () => encounter.DecksManager.GetRandomDish()),
                (duck, () => encounter.DecksManager.GetRandomDish()));

            await FixWave(encounter, new List<ItemData>() { },
                (seal, () => encounter.DecksManager.GetRandomDish()),
                (seal, () => encounter.DecksManager.GetRandomDish()),
                (doggo, () => encounter.DecksManager.GetRandomDish()),
                (doggo, () => encounter.DecksManager.GetRandomDish()));

            encounter.Ticker.Pause();
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
            Tweener shakingCameraLoop = SetCameraShakingLoopAnimation(0.1f);
            await kidsOnTheStreet[0].Say(dialogueLines[Next]);
            UpdateCameraShakingLoopAnimation(ref shakingCameraLoop, 0.25f);
            await kidsOnTheStreet[1].Say(dialogueLines[Next]);
            UpdateCameraShakingLoopAnimation(ref shakingCameraLoop, 0.5f);
            await red.Say(dialogueLines[Next]);

            int crowdPacksAmount = 9;
            int silhouettesCount = 0;
            for (int i = 0; i < crowdPacksAmount; i++)
            {
                UpdateCameraShakingLoopAnimation(ref shakingCameraLoop, 0.5f + i * 0.1f);
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

        private void UpdateCameraShakingLoopAnimation(ref Tweener shakingCameraLoop, float strength)
        {
            shakingCameraLoop.Kill();
            shakingCameraLoop = SetCameraShakingLoopAnimation(strength);
        }

        private Tweener SetCameraShakingLoopAnimation(float strength) =>
            Camera.main.transform.DOShakePosition(0.3f, strength, 50).SetLoops(-1);

        private async Task FixWave(RestaurantEncounter encounter, List<ItemData> itemsToSpawnData, params (CustomerData, Func<ItemData>)[] customersAndTheirOrders)
        {
            await FixWave(encounter, itemsToSpawnData, new List<string>(), customersAndTheirOrders);
        }

        private async Task FixWave(RestaurantEncounter encounter, List<ItemData> itemsToSpawnData, string commentary, params (CustomerData, Func<ItemData>)[] customersAndTheirOrders)
        {
            await FixWave(encounter, itemsToSpawnData, new List<string> { commentary }, customersAndTheirOrders);
        }

        private async Task FixWave(RestaurantEncounter encounter, List<ItemData> itemsToSpawnData, List<string> commentaries, params (CustomerData, Func<ItemData>)[] customersAndTheirOrders)
        {
            bool success = false;
            while (!success)
            {
                Time.timeScale = 1;
                encounter.BlockInput();
                customersToFeed.Clear();

                if (itemsToSpawnData.Count > 0)
                {
                    itemSlots.ForEach(slot => slot.Clear());
                    FindObjectsOfType<Item>().ToList().ForEach(item => Destroy(item.gameObject));
                }

                for (int i = 0; i < itemsToSpawnData.Count; i++)
                {
                    await Task.Delay(500);
                    encounter.ItemsSpawner.SpawnFoodItem(encounter, itemsToSpawnData[i], itemSlots[i]);
                }

                foreach (var customerAndOrder in customersAndTheirOrders)
                {
                    await Task.Delay(500);

                    var customer = encounter.CustomerSpawner.TryToSpawnCustomer(
                        customerAndOrder.Item1,
                        customerAndOrder.Item2
                    );

                    customersToFeed.Add(customer);
                }

                foreach (var commentary in commentaries)
                {
                    await red.Say(commentary);
                }
                encounter.UnblockInput();

                var tasks = customersToFeed.Select(WaitForCustomerToLeave).ToArray();
                var results = await Task.WhenAll(tasks);
                Time.timeScale = 1;

                success = results.All(r => r);
                if (!success)
                    await red.Say(waveIsFailedLine);
            }
        }

        private Task<bool> WaitForCustomerToLeave(Customer customer)
        {
            var tcs = new TaskCompletionSource<bool>();

            void OnLeftHandler(bool wasSatisfied)
            {
                customer.OnLeftSatisfied.RemoveListener(OnLeftHandler);
                tcs.TrySetResult(wasSatisfied);
            }

            customer.OnLeftSatisfied.AddListener(OnLeftHandler);
            return tcs.Task;
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

        protected override void InitTyped(RestaurantEncounter encounter) { }
    }
}