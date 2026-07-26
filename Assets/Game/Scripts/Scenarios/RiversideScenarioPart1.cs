using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class RiversideScenarioPart1 : BaseScenario<ListenDialoguesEncounter>
    {
        [Header("common")]
        [SerializeField] private AudioSource explorationAmbient;

        [Header("bus stop scene")]
        [SerializeField] private GameObject busStopScene;
        [SerializeField] private Transform bus;
        [SerializeField] private Transform silversEyes;
        [SerializeField] private Character silverOnBusStop;
        [SerializeField] private Character redOnBusStop;

        [Header("panorama scene")]
        [SerializeField] private GameObject busStopPanoramaScene;
        [SerializeField] private Character silverOnPanoramaBusStop;
        [SerializeField] private Character silverAboveTheForest;
        [SerializeField] private Character redAboveTheForest;
        [SerializeField] private Character redOnPanoramaBusStop;
        [SerializeField] private Transform signPosition;
        [SerializeField] private Transform silversEyesPanorama;
        [SerializeField] private Transform rightBeyondTheScreenPosition;

        [Header("path scene")]
        [SerializeField] private GameObject pathScene;
        [SerializeField] private Character silverOnPath;
        [SerializeField] private Character redOnPath;

        protected override void InitTyped(ListenDialoguesEncounter encounter) { }

        protected override async UniTask StartScenarioTyped(ListenDialoguesEncounter encounter)
        {
            await BusStopCutScene();
            await BusStopPanoramaCutScene();
            await PathCutScene();
            await BusStopPanoramaCutScene2();
        }

        private async UniTask BusStopCutScene()
        {
            redOnBusStop.LookAt(bus);
            await bus.DOMove(new Vector3(50, 0, 0), 2f).SetEase(Ease.InQuad).ToUniTask();
            await silverOnBusStop.Say("Wow, what a trip.".Locailze());
            redOnBusStop.LookAt(silversEyes);
            await redOnBusStop.Say("So, where are we now?".Locailze());
            await silverOnBusStop.Say("Somewhere not too far from Clifford.".Locailze());
            explorationAmbient.Play();
            await silverOnBusStop.Say("The signal's really bad out here.".Locailze());
            await silverOnBusStop.Say("So we'll have to improvise from here.".Locailze());
            await silverOnBusStop.Say("Catch another bus<pause:0.5> or ask the locals for directions.".Locailze());
            await silverOnBusStop.Say("t<volume:0>...".Locailze());
            busStopScene.gameObject.SetActive(false);
        }

        private async UniTask BusStopPanoramaCutScene()
        {
            busStopPanoramaScene.gameObject.SetActive(true);
            redOnPanoramaBusStop.LookAt(silversEyesPanorama);
            await silverOnPanoramaBusStop.Say("t<volume:0>...".Locailze());
            await silverOnPanoramaBusStop.Say("t<volume:1>Though<pause:1> that might be a bit of a problem.".Locailze());
            await redOnPanoramaBusStop.Say("Are we lost?".Locailze());
            await silverOnPanoramaBusStop.Say("Oh, don't be so dramatic.".Locailze());
            await silverOnPanoramaBusStop.Say("Worst case, we wait here for the next bus and head back.".Locailze());
            await redOnPanoramaBusStop.Say("No way!".Locailze());
            await redOnPanoramaBusStop.Say("We didn't come all the way out here just to give up now!".Locailze());
            redOnPanoramaBusStop.transform.rotation = Quaternion.Euler(0, 180, 0);
            redOnPanoramaBusStop.LookAt(rightBeyondTheScreenPosition);
            redOnPanoramaBusStop.transform.DotweenSteps(new Vector3(13.5f, -7.75f), new Vector3(1, 0.75f, 2f), 2f, 7);
            await UniTask.Delay(1000);
            await silverOnPanoramaBusStop.Say("Hey, where are you going?".Locailze());
            redOnPanoramaBusStop.LookAt(silversEyesPanorama);
            redOnPanoramaBusStop.transform.rotation = Quaternion.Euler(0, 0, 0);
            redOnPanoramaBusStop.SetDialoguePopUpCentering(DialogueDisplayer.Centering.Center);
            await redOnPanoramaBusStop.Say("To find that stupid little town.".Locailze());
            await redOnPanoramaBusStop.Say("And when I get there, I'm gonna cook an outrageous amount of spaghetti!".Locailze());
            await redOnPanoramaBusStop.Say("And the locals are gonna have to eat every last bit of it!".Locailze());
            await redOnPanoramaBusStop.Say("And they will love it!".Locailze());
            await redOnPanoramaBusStop.Say("!!!".Locailze());
            redOnPanoramaBusStop.LookAt(rightBeyondTheScreenPosition);
            redOnPanoramaBusStop.transform.rotation = Quaternion.Euler(0, 180, 0);

            List<UniTask> tasks = new List<UniTask>()
            {
                redOnPanoramaBusStop.transform.DotweenSteps(new Vector3(21f, -7.75f), new Vector3(1, 0.75f, 2f), 1.5f, 5),
                silverOnPanoramaBusStop.Say("I mean, where are you going?<pause:0.75> You just walked right past a sign.".Locailze())
            };
            await UniTask.WhenAll(tasks);

            redOnPanoramaBusStop.transform.rotation = Quaternion.Euler(0, 0, 0);
            redOnPanoramaBusStop.LookAt(signPosition);
            redOnPanoramaBusStop.SetDialoguePopUpCentering(DialogueDisplayer.Centering.Right);
            redOnPanoramaBusStop.SetDialoguePopUpLocalPosition(new Vector3(-880, 114));
            await redOnPanoramaBusStop.Say("Oh.".Locailze());
            await redOnPanoramaBusStop.transform.DotweenSteps(new Vector3(13.5f, -7.75f), new Vector3(1, 0.75f, 2f), 1.5f, 5);
            await UniTask.Delay(500);
            busStopPanoramaScene.gameObject.SetActive(false);
        }

        private async UniTask PathCutScene()
        {
            pathScene.gameObject.SetActive(true);
            await UniTask.Delay(500);
            redOnPath.SetDialoguePopUpCentering(DialogueDisplayer.Centering.Center);
            silverOnPath.SetDialoguePopUpCentering(DialogueDisplayer.Centering.Center);
            await redOnPath.Say("It says Clifford's just down this trail.".Locailze());
            await silverOnPath.Say("So what are we waiting for?".Locailze());
            await redOnPath.Say("Tremble, people of Clifford!".Locailze());
            await redOnPath.Say("Red is coming!".Locailze());
            pathScene.gameObject.SetActive(false);
        }

        private async UniTask BusStopPanoramaCutScene2()
        {
            busStopPanoramaScene.SetActive(true);
            silverOnPanoramaBusStop.transform.DotweenSteps(new Vector3(13.5f, -7.75f), new Vector3(1, 0.75f, 2f), 2f, 7);
            await Camera.main.transform.DOMove(new Vector3(Camera.main.transform.position.x, 16.5f, Camera.main.transform.position.z), 5).ToUniTask();
            redAboveTheForest.SetDialoguePopUpCentering(DialogueDisplayer.Centering.Center);
            silverAboveTheForest.SetDialoguePopUpCentering(DialogueDisplayer.Centering.Center);
            await silverAboveTheForest.Say("And the great and terrible Red isn't scared of a creepy forest like this?".Locailze());
            await redAboveTheForest.Say("A little.".Locailze());
            await redAboveTheForest.Say("But it's not as scary as turning back!".Locailze());
            await silverAboveTheForest.Say("I'm sure<pause:0.75> it's not as bad as it looks.".Locailze());
            await silverAboveTheForest.Say("One last push, and we'll be relaxing in Clifford after the trip.".Locailze());
            await redAboveTheForest.Say("And sipping cocktails with little umbrellas? (question)".Locailze());
            await silverAboveTheForest.Say("And sipping cocktails with little umbrellas.".Locailze());
            await redAboveTheForest.Say("Sweet.".Locailze());
            explorationAmbient.DOFade(0, 1.5f);
        }
    }
}