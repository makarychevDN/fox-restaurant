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
            /*await Cutscene();
            encounter.ItemSpawnTimer.SetBlocked(false);
            encounter.GarbageCan.SetBlocked(false);
            await TheFirstWave();
            await TheSecondWave();
            await TheThirdWave();
            await UniTask.Delay(1000);*/
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
            encounter.ItemsOperations.ClearAllTheItems();
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
            await boris.Say("Ohhh,<pause:0.5> thank you, everyone.".Locailze());
            await boris.Say("I haven't had such a good time in a long time!".Locailze());
            await boris.Say("These past few days, everyone has been so busy and worried that I almost forgot what it felt like.".Locailze());
            await boris.Say("And now we're all sitting at the same table again!".Locailze());
            await boris.Say("Sharing delicious food and stories!".Locailze());
            await boris.Say("And look at me!<pause:0.5> I can already feel that I'm doing so much better!".Locailze());
            await boris.Say("All of us!".Locailze());
            await boris.Say("It's all thanks to you guys!".Locailze());
            RedAndSilverLookAt(runningPeople[0].transform);
            await runningPeople[0].Say("Hooray for Aunty Adele,<pause:0.5> the Clifforder, and Leshy!".Locailze());
            RedAndSilverLookAt(runningPeople[1].transform);
            await runningPeople[1].Say("Hooray for Leshy the doctor!".Locailze());
            RedAndSilverLookAt(runningPeople[2].transform);
            await runningPeople[2].Say("t<volume:0>...".Locailze());
            RedAndSilverLookAt(boris.transform);
            await boris.Say("Come on, Vasya,<pause:0.5> you know what you're supposed to say!".Locailze());
            RedAndSilverLookAt(runningPeople[2].transform);
            await runningPeople[2].Say("...".Locailze());
            await runningPeople[2].Say("t<volume:1>Hooray.".Locailze());
            RedAndSilverLookAt(boris.transform);
            await boris.Say("And now come here, I'll give you all a hug!".Locailze());
            RedAndSilverLookAt(runningPeople[0].transform);
            await runningPeople[0].Say("Boris, what are you doing?!".Locailze());
            await runningPeople[0].Say("No matter how kind Leshy has been today, he's still a forest monster!".Locailze());
            await runningPeople[0].Say("He'll eat you!".Locailze());
            RedAndSilverLookAt(boris.transform);
            await boris.Say("I don't care, even if he were the devil himself!".Locailze());
            await boris.Say("He helped my friends, and I'm going to hug him no matter what!".Locailze());
            RedAndSilverLookAt(adelesEye);
            await adele.Say("Boris,<pause:0.5> be reasonable,<pause:0.5> don't.".Locailze());
            await adele.Say("Feeling better doesn't mean you've fully recovered yet.".Locailze());
            RedAndSilverLookAt(boris.transform);
            await boris.Say("Oh, stop putting on a show, you scaredy-cat!".Locailze());
            await boris.Say("I'm coming!".Locailze());
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

            await red.Say("Oh...".Locailze());

            RedAndSilverLookAt(runningPeoplePoofEffects[0].transform);
            await runningPeople[0].Say("Wait a second!<pause:0.5> So he's not Leshy at all!".Locailze());
            RedAndSilverLookAt(runningPeoplePoofEffects[1].transform);
            await runningPeople[1].Say("He's just a grumpy little fox!".Locailze());
            silver.LookAt(redsEyes);
            red.LookAt(silversEyes);
            await silver.Say("Oh my gosh!");
            await silver.Say("Why didn't you tell me you weren't Leshy?".Locailze());
            await red.Say("t<volume:0>.<pause:0.5>.<pause:0.5>.<pause:0.5><volume:1> I'm going to burst with indignation.".Locailze());
            await red.Say("I hope you're proud of yourself.".Locailze());
            await silver.Say("Of course.".Locailze());
            RedAndSilverLookAt(adelesEye);
            await adele.Say("Enough bickering, little foxes.".Locailze());
            await adele.Say("Come on, let's discuss what we're going to do with you next.".Locailze());
        }

        private void RedAndSilverLookAt(Transform target)
        {
            red.LookAt(target);
            silver.LookAt(target);
        }
    }
}