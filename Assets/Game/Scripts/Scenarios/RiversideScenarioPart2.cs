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
            await redInTheForest.Say("Когда ты успел сменить шапку?");
            await silverInTheForest.Say("Когда наше дорожное путешествие оказалось походом, дурик.");
            redInTheForest.LookAt(PointToLookAtItInTheForest);
            await redInTheForest.Say("Смотри.");
            silverInTheForest.LookAt(PointToLookAtItInTheForest);
            await redInTheForest.Say("Мы, кажется, пришли.");
            theForestScene.SetActive(false);
        }

        private async UniTask SceneOnTheEntrance()
        {
            theEntranceScene.SetActive(true);
            theEntranceScene.transform.position = new Vector3(theEntranceScene.transform.position.x, -17, theEntranceScene.transform.position.z);
            theEntranceScene.transform.localScale = Vector3.one * 1.5f;
            await UniTask.Delay(1000);
            await silverOnTheEntrance.Say("Нет,<pause:0.5> это не Клиффорд,<pause:0.5> это Риверсайд.<pause:0.5> Видишь?");
            await redOnTheEntrance.Say("Блин.");
            await silverOnTheEntrance.Say("Не переживай,<pause:0.75> зато мы теперь точно не потерялись!");
            await silverOnTheEntrance.Say("И нам есть у кого спросить дорогу!");
            await silverOnTheEntrance.Say("И посмотри-ка на это!");
            theEntranceScene.transform.DOScale(Vector3.one, 1);
            await theEntranceScene.transform.DOMove(Vector3.zero, 1).ToUniTask();
            await hogOnTheEntrance.DotweenSteps(new Vector3(5, hogOnTheEntrance.position.y), new Vector3(1.15f, 0.85f), 0.75f, 2);
            await hogOnTheEntrance.DOLocalRotate(new Vector3(0, 0, -5), 0.15f).ToUniTask();
            await silverOnTheEntrance.Say("Местный житель!");
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
            await redInTheCrowd.Say("Здрасти.");
            hogInTheCrowd.transform.DotweenStep(hogInTheCrowd.transform.position + Vector3.right, new Vector3(1.2f, 0.8f), 0.15f);
            await hogInTheCrowd.Say("ААА!");
            hogInTheCrowd.transform.DotweenStep(hogInTheCrowd.transform.position + Vector3.right, new Vector3(1.2f, 0.8f), 0.15f);
            await hogInTheCrowd.Say("Чертов клиффордец привел к нам лешего!");
            hogInTheCrowd.transform.DotweenStep(hogInTheCrowd.transform.position + Vector3.right, new Vector3(1.2f, 0.8f), 0.15f);
            await hogInTheCrowd.Say("Мы обречены!");
            hogInTheCrowd.gameObject.SetActive(false);
            poofSound.Play();
            hogDisappearParticles.Play();
            await UniTask.Delay(1000);
            await redInTheCrowd.Say("Это я то леший?");
            earthQuakeSounds.Play();
            var tweener = Camera.main.SetCameraShakingLoopAnimation(0.3f);
            await UniTask.Delay(3000);
            tweener.Kill();
            earthQuakeSounds.DOFade(0, 0.5f);
            percussion.DOFade(0.5f, 0.5f);
            percussion.Play();
            Camera.main.transform.position = new Vector3(0, 0, -10);
            crowd.gameObject.SetActive (true);
            poofSound.Play();
            crowdAppearParticles.ForEach(particles => particles.Play());
            someoneInTheCrowd.SetDialoguePopUpCentering(DialogueDisplayer.Centering.Right);
            await UniTask.Delay(1000);
            await someoneInTheCrowd.Say("Стоять на месте, лесное чудище!");
            await silverInTheCrowd.Say("Да,<pause:0.5> определенно ты.");
            await someoneInTheCrowd.Say("Молчать, гнусный Клиффордец!");
            await someoneInTheCrowd.Say("Сначала вы навлекли на нас болезни!");
            await someoneInTheCrowd.Say("А теперь решили натравить на нас лешего!");
            await someoneInTheCrowd.Say("Не бывать этому!");
            await silverInTheCrowd.Say("Мне кажется, здесь произошло большое недопонимание.");
            await silverInTheCrowd.Say("Мы не из Клиффорда.<pause:0.5> И мы как раз собирались уходить.");
            await someoneInTheCrowd.Say("Вы так легко не уйдете после своих злодеяний!");
            percussion.DOFade(1, 0.5f);
            await redInTheCrowd.Say("Ой-ей");
            await crowd.transform.DotweenSteps(new Vector3(-1.5f, 1.5f), new Vector3(1.05f, 0.95f), 2, 2);
            adeleInTheCrowd.gameObject.SetActive(true);
            poofSound.Play();
            await crowd.transform.DOMove(new Vector3(0, 0), 0.5f).ToUniTask();
            percussion.DOFade(0, 0.25f);
            await adeleInTheCrowd.Say("Так,<pause:0.5> расходимся,<pause:0.5> не на что тут смотреть.");
            await adeleInTheCrowd.Say("А эти двое идут со мной.");
            await someoneInTheCrowd.Say("Но они из Клиффорда!");
            await adeleInTheCrowd.Say("Откуда они - не твоя забота.<pause:0.75> У тебя еще полно работы на сегодня!");
            await adeleInTheCrowd.Say("У всех вас!");
            await someoneInTheCrowd.Say("Но!..");
            await adeleInTheCrowd.Say("Никаких но!<pause:0.75> На тебя порчу давно не накладывали?");
            await someoneInTheCrowd.Say("Тьфу ты!<pause:0.5> Ведьма!<pause:0.5> Пойдемте,<pause:0.5> карга нашла себе новую игрушку.");
            await adeleInTheCrowd.Say("ТЫ КОГО КАРГОЙ НАЗВАЛ, БОЛЕЗНЫЙ?");
            await someoneInTheCrowd.Say("Ой!");
            crowd.gameObject.SetActive(false);
            crowdAppearParticles.ForEach(particles => particles.Play());
            poofSound.Play();
            await UniTask.Delay(500);
            await adeleInTheCrowd.Say("<volume:0>...");
            await adeleInTheCrowd.Say("<volume:1>*вздох*<pause:0.75> За мной,<pause:0.5> пока они не передумали.");
        }
    }
}