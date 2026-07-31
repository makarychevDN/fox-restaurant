using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace foxRestaurant
{
    public class RiversiwdeScenarioPart7 : BaseScenario<ListenDialoguesEncounter>
    {
        [SerializeField] private List<AudioSource> backgroundAmbients;
        [SerializeField] private Character red;
        [SerializeField] private Character silver;
        [SerializeField] private Character hog;
        [SerializeField] private Character goat;
        [SerializeField] private List<Character> peopleBeyondScreen;
        [SerializeField] private Transform redsEyes;
        [SerializeField] private Transform herbs;
        [SerializeField] private Transform silversPaw;
        [SerializeField] private Transform silversEyes;
        [SerializeField] private Transform theCenterOfBoiler;
        [SerializeField] private Transform citizens;
        [SerializeField] private Animator redsHandsAnimator;
        [SerializeField] private AudioSource poofSound;
        [SerializeField] private AudioSource splashSound;
        [SerializeField] private AudioSource farImpactSound;
        [SerializeField] private AudioSource sneezeSound;
        [SerializeField] private ParticleSystem goatsPoofEffect;
        [SerializeField] private ParticleSystem hogsPoofEffect;

        protected override void InitTyped(ListenDialoguesEncounter encounter)
        {
            foreach (AudioSource ambient in backgroundAmbients)
            {
                ambient.DOFade(1, 1);
            }
        }

        protected override async UniTask StartScenarioTyped(ListenDialoguesEncounter encounter)
        {
            await UniTask.Delay(1000);
            await red.Say("t<volume:0>.<pause:0.5>.<pause:0.5>.<pause:0.5><volume:1> Now.".Locailze());
            redsHandsAnimator.SetBool("isMixing", false);
            red.LookAt(herbs);
            silver.LookAt(herbs);
            await silversPaw.DOLocalMove(new Vector3(2.25f, -1.33f), 0.5f).AsyncWaitForCompletion().AsUniTask();
            herbs.parent = herbs.parent.parent;
            splashSound.Play();
            await herbs.DOMove(new Vector3(-4, -15), 0.5f).SetEase(Ease.InQuad).AsyncWaitForCompletion().AsUniTask();
            await silversPaw.DOLocalMove(new Vector3(-0.06f, -3.01f), 0.5f).AsyncWaitForCompletion().AsUniTask();
            redsHandsAnimator.SetBool("isMixing", true);
            await UniTask.Delay(2000);
            silver.LookAt(redsEyes);
            await silver.Say("Do you think this counts as a remedy?".Locailze());
            red.LookAt(silversEyes);
            await red.Say("Probably not.<pause:0.5> It's just soup with herbs.".Locailze());
            await silver.Say("Yeah,<pause:0.5> but then what even counts as a remedy if not soup with herbs?".Locailze());
            redsHandsAnimator.SetBool("isMixing", false);
            await red.Say("Hmm,<pause:0.5> that's<pause:0.75> a very interesting observation.".Locailze());
            await silver.Say("The apprentice potion maker has surpassed his master!".Locailze());
            redsHandsAnimator.SetBool("isMixing", true);
            await red.Say("Focus!".Locailze());
            red.LookAt(theCenterOfBoiler);
            await silver.Say("Oh, come on,<pause:0.5> it was funny!".Locailze());
            red.LookAt(silversEyes);
            await red.Say("The kitchen is no place for fooling around!".Locailze());
            await red.Say("This is our workshop, and we're creating great things here!".Locailze());
            red.LookAt(theCenterOfBoiler);
            await silver.Say("t<volume:0>.<pause:0.5>.<pause:0.5>.<pause:0.5><volume:1> It was still funny though.".Locailze());
            redsHandsAnimator.SetBool("isMixing", false);
            red.LookAt(silversEyes);
            await red.Say("Fine,<pause:0.5> it was a little funny.".Locailze());
            await silver.Say("A small victory!".Locailze());
            redsHandsAnimator.SetBool("isMixing", true);
            red.LookAt(theCenterOfBoiler);
            await red.Say("You're a scallywag!".Locailze());

            await citizens.DOMove(new Vector3(0, 3.65f), 2).AsyncWaitForCompletion().AsUniTask();
            goat.SetDialoguePopUpCentering(DialogueDisplayer.Centering.Right);
            await goat.Say("Did you hear that?".Locailze());
            await goat.Say("This isn't some kind of medicine for illnesses,<pause:0.5> it's just ordinary soup!".Locailze());
            await hog.Say("Well, I don't know, Vasya.<pause:0.5> I actually feel better after eating his brew.".Locailze());
            await hog.Say("And it's the tastiest soup I've ever had!".Locailze());
            silver.LookAt(citizens);
            red.LookAt(citizens);
            await goat.Say("Enjoy it while you can.".Locailze());
            await goat.Say("It's obvious he's fattening us up so we'll taste better ourselves.".Locailze());
            redsHandsAnimator.SetBool("isMixing", false);
            await hog.Say("Have you completely lost your mind?".Locailze());
            await hog.Say("You're seriously going to accuse Leshy of something now?".Locailze());
            await goat.Say("We would've kicked that Leshy out ourselves if it weren't for that damn witch!".Locailze());

            await red.Say("I'm not Le...".Locailze());
            await silver.Say("Wait, did I just hear someone disrespect the great Leshy?".Locailze());

            await hog.Say("It wasn't me!<pause:0.5> It was all him!".Locailze());
            hog.gameObject.SetActive(false);
            hogsPoofEffect.Play();
            poofSound.Play();
            await UniTask.Delay(350);

            await goat.Say("Me?!<pause:0.5> I didn't do anything!".Locailze());
            goat.gameObject.SetActive(false);
            goatsPoofEffect.Play();
            poofSound.Play();
            await UniTask.Delay(350);

            await silver.Say("Heheh.".Locailze());
            await red.Say("*sigh*t".Locailze());

            red.LookAt(theCenterOfBoiler);
            silver.LookAt(theCenterOfBoiler);
            redsHandsAnimator.SetBool("isMixing", true);
            await UniTask.Delay(1500);

            farImpactSound.volume = 0.33f;
            farImpactSound.Play();
            await Camera.main.ShakeCamera(1);

            red.LookAt(peopleBeyondScreen[0].transform);
            silver.LookAt(peopleBeyondScreen[0].transform);
            await peopleBeyondScreen[0].Say("He broke free!".Locailze());

            farImpactSound.volume = 0.67f;
            sneezeSound.volume = 0.25f;
            farImpactSound.Play();
            sneezeSound.Play();
            await Camera.main.ShakeCamera(1);
            await UniTask.Delay(1000);

            peopleBeyondScreen[1].SetDialoguePopUpCentering(DialogueDisplayer.Centering.Right);
            red.LookAt(peopleBeyondScreen[1].transform);
            silver.LookAt(peopleBeyondScreen[1].transform);
            await peopleBeyondScreen[1].Say("Everyone save yourselves!".Locailze());

            red.LookAt(silversEyes);
            silver.LookAt(redsEyes);
            farImpactSound.volume = 1f;
            sneezeSound.volume = 0.5f;
            farImpactSound.Play();
            sneezeSound.Play();
            Camera.main.ShakeCamera(1, duration: 2);
            await UniTask.Delay(1000);
        }
    }
}