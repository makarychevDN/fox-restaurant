using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class RiversideScenarioPart2 : BaseScenario<ListenDialoguesEncounter>
    {
        [Header("the forest scene")]
        [SerializeField] private Character silverInTheForest;
        [SerializeField] private Character redInTheForest;
        [SerializeField] private Transform pointOnTheRightBeyondOfScreen;
        [SerializeField] private Transform silversEyesInTheForest;
        [SerializeField] private Transform redsEyesInTheForest;

        protected override void InitTyped(ListenDialoguesEncounter encounter)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z);
        }

        protected override async Task StartScenarioTyped(ListenDialoguesEncounter encounter)
        {
            await redInTheForest.transform.DotweenSteps(new Vector3(7.25f, redInTheForest.transform.position.y), new Vector3(1.2f, 0.8f), 2, 5);
            redInTheForest.LookAt(silversEyesInTheForest);
            await silverInTheForest.transform.DotweenSteps(new Vector3(-7.25f, redInTheForest.transform.position.y), new Vector3(1.15f, 0.85f), 2, 5);
            silverInTheForest.LookAt(redsEyesInTheForest);
            await Task.Delay(1000);
            await redInTheForest.Say("Когда ты успел сменить шапку?");
            await silverInTheForest.Say("Когда наше дорожное путешествие оказалось лесным походом, дурилка.");

            await Task.Delay(10000);
        }
    }
}