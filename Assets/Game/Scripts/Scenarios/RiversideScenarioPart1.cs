using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class RiversideScenarioPart1 : BaseScenario<ListenDialoguesEncounter>
    {
        [SerializeField] private Transform bus;
        [SerializeField] private Transform silversEyes;
        [SerializeField] private Character silverOnBusStop;
        [SerializeField] private Character redOnBusStop;
        [SerializeField] private Character silverOnPanaramaBusStop;
        [SerializeField] private Character redOnPanaramaBusStop;
        [SerializeField] private GameObject busStopScene;
        [SerializeField] private GameObject busStopPanoramaScene;
        [SerializeField] private Transform signPosition;
        [SerializeField] private AudioSource explorationAmbient;

        protected override void InitTyped(ListenDialoguesEncounter encounter) { }

        protected override async Task StartScenarioTyped(ListenDialoguesEncounter encounter)
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
            busStopPanoramaScene.gameObject.SetActive(true);
            redOnPanaramaBusStop.LookAt(signPosition);
            await silverOnPanaramaBusStop.Say("<volume:0>...");
            await silverOnPanaramaBusStop.Say("<volume:1>Хотя<pause:1> с этим могут возникнуть проблемы.");
            await redOnPanaramaBusStop.Say("Мы потерялись?");
            await silverOnPanaramaBusStop.Say("Ой, не сгущай краски.");
            await silverOnPanaramaBusStop.Say("В худшем случае посидим здесь до следующего автобуса и поедем обратно.");
            await redOnPanaramaBusStop.Say("Ну уж нет уж!");
            await redOnPanaramaBusStop.Say("Мы приехали к черту на кулички не для того, чтобы все так бросить!");
            redOnPanaramaBusStop.transform.rotation = Quaternion.Euler(0, 180, 0);
            redOnPanaramaBusStop.transform.DotweenSteps(new Vector3(13.5f, -7.75f), new Vector3(1, 0.75f, 2f), 2f, 7);
            await Task.Delay(1000);
            await silverOnPanaramaBusStop.Say("Эй, ты куда собрался?");
            redOnPanaramaBusStop.transform.rotation = Quaternion.Euler(0, 0, 0);
            await redOnPanaramaBusStop.Say("Искать этот несчастный городишко.");
            await redOnPanaramaBusStop.Say("И, когда я его найду, я наварю котел спагетти с фрикадельками!");
            await redOnPanaramaBusStop.Say("А им придется его есть!");
            await redOnPanaramaBusStop.Say("И они будут в восторге!");
            await redOnPanaramaBusStop.Say("!!!");
            redOnPanaramaBusStop.transform.rotation = Quaternion.Euler(0, 180, 0);
            redOnPanaramaBusStop.transform.DotweenSteps(new Vector3(21f, -7.75f), new Vector3(1, 0.75f, 2f), 1.5f, 5);
            await silverOnPanaramaBusStop.Say("Я имею в виду, куда ты идешь? <pause:0.75> Ты прошел мимо какого-то указателя.");
            await redOnPanaramaBusStop.Say("Ой");
            await Task.Delay(3000);
        }
    }
}