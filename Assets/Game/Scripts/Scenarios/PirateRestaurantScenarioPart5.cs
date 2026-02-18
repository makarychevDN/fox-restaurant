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

        protected override async Task StartScenarioTyped(RestaurantEncounter encounter)
        {
            itemSlots = encounter.SlotsManager.Slots.Where(slot => slot.RequiredItemsType == ItemType.Food && slot.gameObject.activeSelf).ToList();
            capitan.OnEmote.AddListener(ShakeCameraOnce);

            /*await CurtainsAppears();
            //lameJoe.gameObject.SetActive(true);
            await IntroSpeech();
            await Task.Delay(1000);
            await CartoonScreenAppearsAnimation();
            //await Waves(encounter);
            await DialogueAfterWaves();
            await Task.Delay(2000);
            await CapitanAppears();*/
            await CrowdAppears();
        }

        private async Task CurtainsAppears()
        {
            curtainsSound.Play();
            await curtains.DOLocalMove(new Vector3(0, 15), 1.5f).SetEase(Ease.InExpo).AsyncWaitForCompletion();
        }

        private async Task IntroSpeech()
        {
            await red.Say("Хорошо,<pause:0.75> давай попробуем.<pause:1> 3,<pause:1> 2,<pause:1> 1.<pause:1> Поехали!");

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

            await talkingKidsSetup[2].character.Say("О боже,<pause:0.4> это же Хромой Джо!");
            await talkingKidsSetup[3].character.Say("Ура,<pause:0.4> Хромой Джо!<pause:0.4> Покатай меня!!!");
            await talkingKidsSetup[1].character.Say("И меня!!!");
            await lameJoe.Say("Йарр!<pause:0.75> Сегодня Хромой Джо себя не важно чувствует,<pause:0.4> так что шуметь мы сегодня не будем.");
            await talkingKidsSetup[3].character.Say("Ну блииин!<pause:0.75> Не честно!");
            await talkingKidsSetup[1].character.Say("Я думал мы повеселимся.");
            await lameJoe.Say("Не переживай, юнга,<pause:0.75> нам не обязательно шуметь, чтобы веселиться!");
            await lameJoe.Say("Например,<pause:0.75> мы можем посмотреть мультик!");
            await talkingKidsSetup[2].character.Say("Мультик - это хорошо.");
            await talkingKidsSetup[3].character.Say("Только, если интересный!");
            await talkingKidsSetup[0].character.Say("Надеюсь,<pause:0.5> в нем будет морская звезда...");
            await lameJoe.Say("Йарр,<pause:0.75> конечно, интересный!<pause:0.75> Но мы будем его смотреть в тишине.<pause:0.5> Договорились, юнги?");
            await talkingKidsSetup[3].character.Say("Договорились!");


            foreach (var pack in talkingKidsSetup)
            {
                pack.character.gameObject.SetActive(false);
                pack.particle.Play();
                pack.poofSound.Play();
                await Task.Delay(200);
            }

            await red.Say("<volume:0>.<pause:0.5>.<pause:0.5>.<pause:0.5><volume:1> Сработало!<pause:0.75> Никакой толпы не набежало!");
            await lameJoe.Say("Я же говорил!<pause:0.75> План-капкан!");
            red.LookAt(null);
            await red.Say("Дело остается за малым.<pause:0.75> За работу!");
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

            //return;

            await red.Say("Фух,<pause:0.5> юнги,<pause:0.5> кому еще мороженое?");
            await talkingKidsSetup[2].character.Say("<volume:0>...");
            await lameJoe.Say("Кажется,<pause:0.5> ты переработал всю работу.");
            await talkingKidsSetup[1].character.Say("Можно, пожалуйста, потише?");
            await talkingKidsSetup[2].character.Say("<volume:1>Мы тут вообще-то смотрим.");
            await red.Say("Ой,<pause:0.5> извините.");
        }

        private async Task CapitanAppears()
        {
            vhsArtifactsSound.Play();
            backgroundMusic.Stop();
            await BlinkGameObjectNTimes(capitanCallsCartoon, 21);
            await BlinkGameObjectNTimes(capitanCallsCartoon, 1);
            await talkingKidsSetup[2].character.Say("Ну, блииин!");
            await talkingKidsSetup[1].character.Say("Эй,<pause:0.5> мы же не досмотрели!");
            await capitan.Say("<volume:0.5>Йарр!<pause:0.5> Вы что здесь устроили, салаги?");
            await capitan.Say("Я сказал выпускать Хромого Джо, не для того, чтобы вы здесь прохлаждались!");
            curtainsSound.Play();
            await curtains.DOLocalMove(new Vector3(0, 22.5f), 1.5f).AsyncWaitForCompletion();
            await Task.Delay(1000);
            additionalSpeakersMovingSound.Play();
            await additionalSpeakers.DOLocalMove(new Vector3(0, 8.5f), 1.75f).AsyncWaitForCompletion();
            microphoneArtifactsSound.Play();
            await Task.Delay(2000);
            await capitan.Say("<emote:shake><volume:1>Эй,<pause:0.5><emote:shake> юнги,<pause:0.5><emote:shake> здесь все это время прятался Хромой Джо!<pause:0.5><emote:shake> Быстрее сюда,<pause:0.5><emote:shake> покажите, как вы его любите!");
            additionalSpeakersMovingSound.Play();
            additionalSpeakers.DOLocalMove(new Vector3(0, 14.2f), 1.75f);
            await capitan.Say("<volume:0.5>Вперед, салаги,<pause:0.75> добудьте мне все сокровища мира!<pause:0.75> Ахахахаах!");
            screen.DOLocalMove(new Vector3(0, 15.2f), 1.75f).SetEase(Ease.InExpo);
        }

        private async Task CrowdAppears()
        {
            earthQuakeSound.Play();
            await Task.Delay(1000);
            Tweener shakingCameraLoop = SetCameraShakingLoopAnimation(0.1f);
            await kidsOnTheStreet[0].Say("Это Хромой Джо!");
            UpdateCameraShakingLoopAnimation(shakingCameraLoop, 0.25f);
            await kidsOnTheStreet[1].Say("Бежим к нему скорее!");
            UpdateCameraShakingLoopAnimation(shakingCameraLoop, 0.5f);
            await red.Say("Ой-ей.");

            int crowdPacksAmount = 9;
            int silhouettesCount = 0;
            for (int i = 0; i < crowdPacksAmount; i++)
            {
                UpdateCameraShakingLoopAnimation(shakingCameraLoop, 0.5f + i * 0.1f);
                crowdPacks[i].SetActive(true);
                poofSound.Play();

                if (i >= crowdPacksAmount - silhouettes.Count)
                {
                    silhouettes[silhouettesCount].SetActive(true);
                    silhouettesCount++;
                }

                await Task.Delay(500);
            }

            await red.Say("Помогите.");
        }

        private async Task BlinkGameObjectNTimes(GameObject go, int n = 5)
        {
            for (int i = 0; i < n; i++)
            {
                go.SetActive(!go.activeSelf);
                await Task.Delay(50);
            }
        }

        private void UpdateCameraShakingLoopAnimation(Tweener shakingCameraLoop, float strength)
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