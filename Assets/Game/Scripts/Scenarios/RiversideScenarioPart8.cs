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
        [SerializeField] private CookPositionController cookPositionController;
        [SerializeField] private Character adele;
        [SerializeField] private Character silver;
        [SerializeField] private Character boris;
        [SerializeField] private Transform adelesEye;
        [SerializeField] private Transform silversEyes;
        [SerializeField] private Transform redsEyes;
        [SerializeField] private Transform redsLeaves;
        [SerializeField] private List<Transform> fallingLeaves;
        [SerializeField] private ParticleSystem redsPoofEffect;
        [SerializeField] private ParticleSystem borisPoofEffect;
        [SerializeField] private ParticleSystem adelePoofEffect;
        [SerializeField] private ParticleSystem silversPoofEffect;
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
            runningPeople.ForEach(p => p.SetDialoguePopUpCentering(DialogueDisplayer.Centering.Center));
        }

        protected override async UniTask StartScenarioTyped(RestaurantEncounter encounter)
        {
            await Cutscene();
            encounter.ItemSpawnTimer.SetBlocked(false);
            encounter.GarbageCan.SetBlocked(false);
            await TheFirstWave();
            await TheSecondWave();
            await TheThirdWave();
            await UniTask.Delay(1000);
            await CutsceneAfterWaves();
        }

        private async UniTask Cutscene()
        {
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
            await red.Say("I won't let you down.".Locailze());
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
        
        private async UniTask CutsceneAfterWaves()
        {
            encounter.BlockInput();
            cookPositionController.ChangePosition(new Vector3(0, -9.7f));
            await UniTask.Delay(200);
            silver.LookAt(redsEyes);
            red.LookAt(adelesEye.transform);
            adele.gameObject.SetActive(true);
            adelePoofEffect.Play();
            poofSound.Play();
            await UniTask.Delay(1000);

            red.LookAt(silversEyes.transform);
            silver.gameObject.SetActive(true);
            silversPoofEffect.Play();
            poofSound.Play();
            await UniTask.Delay(1000);

            RedAndSilverLookAt(boris.transform);
            boris.transform.position = new Vector3(5, -1);
            borisPoofEffect.transform.position = new Vector3(5, 1.5f);
            boris.gameObject.SetActive(true);
            borisPoofEffect.Play();
            poofSound.Play();
            await UniTask.Delay(250);

            for (int i = 0; i < runningPeople.Count; i++)
            {
                RedAndSilverLookAt(runningPeople[i].transform);
                runningPeople[i].gameObject.SetActive(true);
                runningPeoplePoofEffects[i].Play();
                poofSound.Play();
                await UniTask.Delay(250);
            }

            await UniTask.Delay(1000);
            RedAndSilverLookAt(boris.transform);
            await boris.Say("Охх,<pause:0.5> спасибо ребята.");
            await boris.Say("Я давно уже так славно не проводил время!");
            await boris.Say("Последние дни все так суетились и переживали, что я уже почти забыл, каково это.");
            await boris.Say("И вот мы снова за одним столом!");
            await boris.Say("Делимся вкусностями и историями!");
            await boris.Say("И посмотрите на меня!<pause:0.5> Я уже чувствую, что мне стало гораздо лучше!");
            await boris.Say("Всем нам!");
            await boris.Say("Это все благодаря вам, ребята!");
            RedAndSilverLookAt(runningPeople[0].transform);
            await runningPeople[0].Say("Ура тетушке Адель,<pause:0.5> клиффордцу и лешему!");
            RedAndSilverLookAt(runningPeople[1].transform);
            await runningPeople[1].Say("Ура лешему-доктору!");
            RedAndSilverLookAt(runningPeople[2].transform);
            await runningPeople[2].Say("<volume:0>...");
            RedAndSilverLookAt(boris.transform);
            await boris.Say("Ну же, Вася,<pause:0.5> ты знаешь, что нужно сказать!");
            RedAndSilverLookAt(runningPeople[2].transform);
            await runningPeople[2].Say("...");
            await runningPeople[2].Say("<volume:1>Ура.");
            RedAndSilverLookAt(boris.transform);
            await boris.Say("А теперь идите сюда, я вас всех обниму!");
            RedAndSilverLookAt(runningPeople[0].transform);
            await runningPeople[0].Say("Боря, ты чего?!");
            await runningPeople[0].Say("Каким бы радушным леший сегодня не был, он все еще лесное чудище!");
            await runningPeople[0].Say("Он же тебя сожрет!");
            RedAndSilverLookAt(boris.transform);
            await boris.Say("Да будь он хоть сам дьявол!");
            await boris.Say("Он помог моим друзьям и я его обниму чего бы это ни стоило!");
            RedAndSilverLookAt(adelesEye);
            await adele.Say("Боря,<pause:0.5> будь благоразумен,<pause:0.5> не надо.");
            await adele.Say("То, что тебе стало лучше, еще не значит, что ты выздоровел полностью.");
            RedAndSilverLookAt(boris.transform);
            await boris.Say("Ох, хватит ломать комедию, недотрога!");
            await boris.Say("Я иду!");
            await UniTask.Delay(1000);
            redsPoofEffect.Play();
            redsLeaves.transform.position += Vector3.up * 3;
            redsLeaves.gameObject.SetActive(false);
            poofSound.Play();
            foreach (var leaf in fallingLeaves)
            {
                leaf.gameObject.SetActive(true);
                leaf.DOMove(leaf.transform.position += Vector3.up * 1.5f, 0.2f);
            }
            BorisSneeze();

            await UniTask.Delay(200);

            foreach (var leaf in fallingLeaves)
            {
                leaf.DOMoveY(leaf.transform.position.y - 10f, 5f)
                    .SetEase(Ease.Linear);

                leaf.DOMoveX(leaf.transform.position.x + 3f, 0.5f)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutSine);
            }

            RedAndSilverLookAt(fallingLeaves[0].transform);

            await UniTask.Delay(3500);

            await red.Say("Во дела.");

            RedAndSilverLookAt(runningPeoplePoofEffects[0].transform);
            await runningPeople[0].Say("Погодите-ка!<pause:0.5> Так это никакой и не леший!");
            RedAndSilverLookAt(runningPeoplePoofEffects[1].transform);
            await runningPeople[1].Say("Это просто ворчливый лисенок!");
            silver.LookAt(redsEyes);
            red.LookAt(silversEyes);
            await silver.Say("Боже мой!");
            await silver.Say("Почему ты мне сразу не сказал, что ты не леший?!");
            await red.Say("<volume:0>.<pause:0.5>.<pause:0.5>.<pause:0.5><volume:1> Я сейчас лопну от возмущения.");
            await red.Say("Я надеюсь, ты гордишься собой.");
            await silver.Say("Разумеется.");
            RedAndSilverLookAt(adelesEye);
            await adele.Say("Хватит препираться, лисятки.");
            await adele.Say("Пойдемте, обсудим, что делать с вами дальше.");
        }

        private void RedAndSilverLookAt(Transform target)
        {
            red.LookAt(target);
            silver.LookAt(target);
        }
    }
}