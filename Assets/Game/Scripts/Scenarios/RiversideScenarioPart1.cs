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

        protected override async Task StartScenarioTyped(ListenDialoguesEncounter encounter)
        {
            await BusStopCutScene();
            await BusStopPanoramaCutScene();
            await PathCutScene();
            await BusStopPanoramaCutScene2();
        }

        private async Task BusStopCutScene()
        {
            redOnBusStop.LookAt(bus);
            await bus.DOMove(new Vector3(50, 0, 0), 2f).SetEase(Ease.InQuad).AsyncWaitForCompletion();
            await silverOnBusStop.Say("Ну и поездочка.");
            redOnBusStop.LookAt(silversEyes);
            await redOnBusStop.Say("И где мы теперь?");
            await silverOnBusStop.Say("Где-то недалеко от Клиффорда.");
            explorationAmbient.Play();
            await silverOnBusStop.Say("Тут очень плохо ловит связь.");
            await silverOnBusStop.Say("Так что дальше нам придется импровизировать.");
            await silverOnBusStop.Say("Найти другой автобус<pause:0.5> или поспрашивать у местных.");
            await silverOnBusStop.Say("<volume:0>...");
            busStopScene.gameObject.SetActive(false);
        }

        private async Task BusStopPanoramaCutScene()
        {
            busStopPanoramaScene.gameObject.SetActive(true);
            redOnPanoramaBusStop.LookAt(silversEyesPanorama);
            await silverOnPanoramaBusStop.Say("<volume:0>...");
            await silverOnPanoramaBusStop.Say("<volume:1>Хотя<pause:1> с этим могут возникнуть проблемы.");
            await redOnPanoramaBusStop.Say("Мы потерялись?");
            await silverOnPanoramaBusStop.Say("Ой, не сгущай краски.");
            await silverOnPanoramaBusStop.Say("В худшем случае посидим здесь до следующего автобуса и поедем обратно.");
            await redOnPanoramaBusStop.Say("Ну уж нет!");
            await redOnPanoramaBusStop.Say("Мы приехали к черту на кулички не для того, чтобы все так бросить!");
            redOnPanoramaBusStop.transform.rotation = Quaternion.Euler(0, 180, 0);
            redOnPanoramaBusStop.LookAt(rightBeyondTheScreenPosition);
            redOnPanoramaBusStop.transform.DotweenSteps(new Vector3(13.5f, -7.75f), new Vector3(1, 0.75f, 2f), 2f, 7);
            await Task.Delay(1000);
            await silverOnPanoramaBusStop.Say("Эй, ты куда собрался?");
            redOnPanoramaBusStop.LookAt(silversEyesPanorama);
            redOnPanoramaBusStop.transform.rotation = Quaternion.Euler(0, 0, 0);
            redOnPanoramaBusStop.SetDialoguePopUpCentering(DialogueDisplayer.Centering.Center);
            await redOnPanoramaBusStop.Say("Искать этот несчастный городишко.");
            await redOnPanoramaBusStop.Say("И, когда я туда дойду, я наварю там котел спагетти с фрикадельками!");
            await redOnPanoramaBusStop.Say("А местным жителям придется его есть!");
            await redOnPanoramaBusStop.Say("И они будут в восторге!");
            await redOnPanoramaBusStop.Say("!!!");
            redOnPanoramaBusStop.LookAt(rightBeyondTheScreenPosition);
            redOnPanoramaBusStop.transform.rotation = Quaternion.Euler(0, 180, 0);

            List<Task> tasks = new List<Task>()
            {
                redOnPanoramaBusStop.transform.DotweenSteps(new Vector3(21f, -7.75f), new Vector3(1, 0.75f, 2f), 1.5f, 5),
                silverOnPanoramaBusStop.Say("Я имею в виду, куда ты идешь? <pause:0.75> Ты прошел мимо какого-то указателя.")
            };
            await Task.WhenAll(tasks);

            redOnPanoramaBusStop.transform.rotation = Quaternion.Euler(0, 0, 0);
            redOnPanoramaBusStop.LookAt(signPosition);
            redOnPanoramaBusStop.SetDialoguePopUpCentering(DialogueDisplayer.Centering.Right);
            redOnPanoramaBusStop.SetDialoguePopUpLocalPosition(new Vector3(-880, 114));
            await redOnPanoramaBusStop.Say("Ой.");
            await redOnPanoramaBusStop.transform.DotweenSteps(new Vector3(13.5f, -7.75f), new Vector3(1, 0.75f, 2f), 1.5f, 5);
            await Task.Delay(500);
            busStopPanoramaScene.gameObject.SetActive(false);
        }

        private async Task PathCutScene()
        {
            pathScene.gameObject.SetActive(true);
            await Task.Delay(500);
            redOnPath.SetDialoguePopUpCentering(DialogueDisplayer.Centering.Center);
            silverOnPath.SetDialoguePopUpCentering(DialogueDisplayer.Centering.Center);
            await redOnPath.Say("Тут говорится, что Клиффорд прямо по тропинке.");
            await silverOnPath.Say("Так чего же мы ждем?");
            await redOnPath.Say("Трепещите, жители Клиффорда!");
            await redOnPath.Say("Рыжий идет!");
            pathScene.gameObject.SetActive(false);
        }

        private async Task BusStopPanoramaCutScene2()
        {
            busStopPanoramaScene.SetActive(true);
            silverOnPanoramaBusStop.transform.DotweenSteps(new Vector3(13.5f, -7.75f), new Vector3(1, 0.75f, 2f), 2f, 7);
            await Camera.main.transform.DOMove(new Vector3(Camera.main.transform.position.x, 16.5f, Camera.main.transform.position.z), 5).AsyncWaitForCompletion();
            redAboveTheForest.SetDialoguePopUpCentering(DialogueDisplayer.Centering.Center);
            silverAboveTheForest.SetDialoguePopUpCentering(DialogueDisplayer.Centering.Center);
            await silverAboveTheForest.Say("А сам великий и ужасный Рыжий не боится такого дремучего леса?");
            await redAboveTheForest.Say("Чуть-чуть.");
            await redAboveTheForest.Say("Но это не так страшно, как отступать!");
            await silverAboveTheForest.Say("Уверен,<pause:0.75> оно не так плохо, как выглядит.");
            await silverAboveTheForest.Say("Вот увидишь,<pause:0.75> последнее усилие и мы будем отдыхать после дороги в Клиффорде.");
            await redAboveTheForest.Say("И будем распивать те модные коктейли с маленькими зонтиками?");
            await silverAboveTheForest.Say("И будем распивать те модные коктейли с маленькими зонтиками.");
            await redAboveTheForest.Say("Круто.");
            explorationAmbient.DOFade(0, 1.5f);
        }
    }
}