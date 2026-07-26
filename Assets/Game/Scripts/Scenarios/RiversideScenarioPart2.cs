using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Android;

namespace foxRestaurant
{
    public class RiversideScenarioPart2 : BaseScenario<ListenDialoguesEncounter>
    {
        [Header("the forest scene")]
        [SerializeField] private GameObject theForestScene;
        [SerializeField] private Character silverInTheForest;
        [SerializeField] private Character redInTheForest;
        [SerializeField] private Transform pointOnTheRightBeyondOfScreen;
        [SerializeField] private Transform silversEyesInTheForest;
        [SerializeField] private Transform redsEyesInTheForest;
        [SerializeField] private Transform PointToLookAtItInTheForest;
        [SerializeField] private AudioSource bushesSound;

        [Header("the entrance scene")]
        [SerializeField] private GameObject theEntranceScene;
        [SerializeField] private Character silverOnTheEntrance;
        [SerializeField] private Character redOnTheEntrance;
        [SerializeField] private Transform hogOnTheEntrance;
        [SerializeField] private Transform silverOnTheEntrancePaw;

        [Header("in the crowd scene")]
        [SerializeField] private GameObject theCrowdScene;

        [SerializeField] private Character silverInTheCrowd;
        [SerializeField] private Character redInTheCrowd;
        [SerializeField] private Character hogInTheCrowd;
        [SerializeField] private Character adeleInTheCrowd;
        [SerializeField] private Character someoneInTheCrowd;
        [SerializeField] private GameObject crowd;

        [SerializeField] private ParticleSystem hogDisappearParticles;
        [SerializeField] private List<ParticleSystem> crowdAppearParticles;

        [SerializeField] private AudioSource earthQuakeSounds;
        [SerializeField] private AudioSource percussion;
        [SerializeField] private AudioSource poofSound;

        protected override void InitTyped(ListenDialoguesEncounter encounter)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z);
        }

        protected override async UniTask StartScenarioTyped(ListenDialoguesEncounter encounter)
        {
            await SceneInTheForest();
            await SceneOnTheEntrance();
            await SceneInTheCrowd();
        }

        private async UniTask SceneInTheForest()
        {
            await redInTheForest.transform.DotweenSteps(new Vector3(7.25f, redInTheForest.transform.position.y), new Vector3(1.2f, 0.8f), 2, 5);
            redInTheForest.LookAt(silversEyesInTheForest);
            await bushesSound.DOFade(0, 0.25f).ToUniTask();
            bushesSound.volume = 0.5f;
            bushesSound.Play();
            await silverInTheForest.transform.DotweenSteps(new Vector3(-7.25f, redInTheForest.transform.position.y), new Vector3(1.15f, 0.85f), 2, 5);
            silverInTheForest.LookAt(redsEyesInTheForest);
            await bushesSound.DOFade(0, 0.25f).ToUniTask();
            await redInTheForest.Say("When did you change your hat?".Locailze());
            await silverInTheForest.Say("When our road trip turned into a hike, dummy.".Locailze());
            redInTheForest.LookAt(PointToLookAtItInTheForest);
            await redInTheForest.Say("Look.".Locailze());
            silverInTheForest.LookAt(PointToLookAtItInTheForest);
            await redInTheForest.Say("I think we made it.".Locailze());
            theForestScene.SetActive(false);
        }

        private async UniTask SceneOnTheEntrance()
        {
            theEntranceScene.SetActive(true);
            theEntranceScene.transform.position = new Vector3(theEntranceScene.transform.position.x, -17, theEntranceScene.transform.position.z);
            theEntranceScene.transform.localScale = Vector3.one * 1.5f;
            await UniTask.Delay(1000);
            await silverOnTheEntrance.Say("No<pause:0.5>, this isn't Clifford.<pause:0.5> This is Riverside.<pause:0.5> See?".Locailze());
            await redOnTheEntrance.Say("Damn.".Locailze());
            await silverOnTheEntrance.Say("Hey,<pause:0.75> look on the bright side. At least we're not lost anymore!".Locailze());
            await silverOnTheEntrance.Say("And would you look at that!".Locailze());
            theEntranceScene.transform.DOScale(Vector3.one, 1);
            await theEntranceScene.transform.DOMove(Vector3.zero, 1).ToUniTask();
            await hogOnTheEntrance.DotweenSteps(new Vector3(5, hogOnTheEntrance.position.y), new Vector3(1.15f, 0.85f), 0.75f, 2);
            await hogOnTheEntrance.DOLocalRotate(new Vector3(0, 0, -5), 0.15f).ToUniTask();
            await silverOnTheEntrance.Say("A local!".Locailze());
            theEntranceScene.SetActive(false);
        }

        private void ZoomCamera(Camera cam, float targetSize, float duration)
        {
            DOTween.To(() => cam.orthographicSize, x => cam.orthographicSize = x, targetSize, duration);
        }

        private async UniTask SceneInTheCrowd()
        {
            theCrowdScene.gameObject.SetActive(true);
            silverInTheCrowd.SetDialoguePopUpCentering(DialogueDisplayer.Centering.Right);
            redInTheCrowd.LookAt(hogInTheCrowd.transform);
            silverInTheCrowd.LookAt(hogInTheCrowd.transform);
            await UniTask.Delay(2000);
            await redInTheCrowd.Say("Hi there.".Locailze());
            hogInTheCrowd.transform.DotweenStep(hogInTheCrowd.transform.position + Vector3.right, new Vector3(1.2f, 0.8f), 0.15f);
            await hogInTheCrowd.Say("AAAH!".Locailze());
            hogInTheCrowd.transform.DotweenStep(hogInTheCrowd.transform.position + Vector3.right, new Vector3(1.2f, 0.8f), 0.15f);
            await hogInTheCrowd.Say("That damn Clifforder brought Leshy into our village!".Locailze());
            hogInTheCrowd.transform.DotweenStep(hogInTheCrowd.transform.position + Vector3.right, new Vector3(1.2f, 0.8f), 0.15f);
            await hogInTheCrowd.Say("We're doomed!".Locailze());
            hogInTheCrowd.gameObject.SetActive(false);
            poofSound.Play();
            hogDisappearParticles.Play();
            await UniTask.Delay(1000);
            await redInTheCrowd.Say("Me? Leshy?".Locailze());
            earthQuakeSounds.Play();
            var tweener = Camera.main.SetCameraShakingLoopAnimation(0.3f);
            await UniTask.Delay(3000);
            tweener.Kill();
            earthQuakeSounds.DOFade(0, 0.5f);
            percussion.DOFade(0.5f, 0.5f);
            percussion.Play();
            Camera.main.transform.position = new Vector3(0, 0, -10);
            crowd.gameObject.SetActive(true);
            poofSound.Play();
            crowdAppearParticles.ForEach(particles => particles.Play());
            someoneInTheCrowd.SetDialoguePopUpCentering(DialogueDisplayer.Centering.Right);
            await UniTask.Delay(1000);
            await someoneInTheCrowd.Say("Don't move, forest monster!".Locailze());
            await silverInTheCrowd.Say("Yeah,<pause:0.5> definitely you.".Locailze());
            await someoneInTheCrowd.Say("Silence, you filthy Clifforder!".Locailze());
            await someoneInTheCrowd.Say("First you brought disease upon us!".Locailze());
            await someoneInTheCrowd.Say("And now you've decided to sic Leshy on us!".Locailze());
            await someoneInTheCrowd.Say("We won't let that happen!".Locailze());
            await silverInTheCrowd.Say("I think there's been a huge misunderstanding.".Locailze());
            await silverInTheCrowd.Say("We're not from Clifford.<pause:0.5> We were actually just about to leave.".Locailze());
            await someoneInTheCrowd.Say("You're not getting away that easily after everything you've done!".Locailze());
            percussion.DOFade(1, 0.5f);
            await redInTheCrowd.Say("Uh-oh.".Locailze());
            await crowd.transform.DotweenSteps(new Vector3(-1.5f, 1.5f), new Vector3(1.05f, 0.95f), 2, 2);
            adeleInTheCrowd.gameObject.SetActive(true);
            poofSound.Play();
            await crowd.transform.DOMove(new Vector3(0, 0), 0.5f).ToUniTask();
            percussion.DOFade(0, 0.25f);
            redInTheCrowd.LookAt(adeleInTheCrowd.transform);
            silverInTheCrowd.LookAt(adeleInTheCrowd.transform);
            await adeleInTheCrowd.Say("Alright,<pause:0.5> break it up.<pause:0.5> Nothing to see here.".Locailze());
            await adeleInTheCrowd.Say("These two are coming with me.".Locailze());
            await someoneInTheCrowd.Say("But they're from Clifford!".Locailze());
            await adeleInTheCrowd.Say("Where they're from is none of your concern.<pause:0.75> You've still got plenty of work to do today!".Locailze());
            await adeleInTheCrowd.Say("All of you!".Locailze());
            await someoneInTheCrowd.Say("But!..".Locailze());
            await adeleInTheCrowd.Say("No buts!<pause:0.75> Hasn't anyone put a curse on you in a while?".Locailze());
            await someoneInTheCrowd.Say("Tch!<pause:0.5> Witch!<pause:0.5> Come on,<pause:0.5> the hag's found herself a new pair of toys.".Locailze());
            await adeleInTheCrowd.Say("WHO ARE YOU CALLING A HAG, YOU PEST?".Locailze());
            await someoneInTheCrowd.Say("Yikes!".Locailze());
            crowd.gameObject.SetActive(false);
            crowdAppearParticles.ForEach(particles => particles.Play());
            poofSound.Play();
            await UniTask.Delay(500);
            await adeleInTheCrowd.Say("t<volume:0>...".Locailze());
            await adeleInTheCrowd.Say("t<volume:1>*sigh*<pause:0.75> Follow me,<pause:0.5> before they change their minds.".Locailze());
        }
    }
}