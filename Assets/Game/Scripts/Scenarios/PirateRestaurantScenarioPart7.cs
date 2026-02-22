using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

namespace foxRestaurant
{
    public class PirateRestaurantScenarioPart7 : BaseScenario<ListenDialoguesEncounter>
    {
        [Header("turn tape scene")]
        [SerializeField] private GameObject parentTurnTapeObject;
        [SerializeField] private Transform paw;
        [SerializeField] private Transform tape;
        [SerializeField] private AudioSource squekSounds;
        [SerializeField] private ParticleSystem water;
        [SerializeField] private ParticleSystem steam;
        [SerializeField] private AudioSource newMessageSound;
        [SerializeField] private Character redInBathroomIntro;

        [Header("bathroom mobile phone scene")]
        [SerializeField] private GameObject bathroomPhoneScene;
        [SerializeField] private Transform phoneAndPawsParentInBathroom;
        [SerializeField] private Transform rightPaw;
        [SerializeField] private GameObject blockedScreen;
        [SerializeField] private Character redInBathroomWithPhone;
        [SerializeField] private AudioSource unlockPhoneSound;

        [Header("kitchen intro scene")]
        [SerializeField] private GameObject parentIntroKitchenScene;
        [SerializeField] private AudioSource switchSound;
        [SerializeField] private GameObject lightenKitchenBackground;
        [SerializeField] private GameObject darkKitchenBackground;
        [SerializeField] private Character silverOnIntroKitchen;
        [SerializeField] private Character redOnIntroKitchen;

        [Header("main kitchen scene")]
        [SerializeField] private GameObject parentMainKitchenScene;
        [SerializeField] private Character silverOnMainKitchen;
        [SerializeField] private Character redOnMainKitchen;
        [SerializeField] private AudioSource lookingForJarSounds;
        [SerializeField] private AudioSource typingSounds;
        [SerializeField] private AudioSource sadFoxSound;
        [SerializeField] private GameObject redsPawsEmpty;
        [SerializeField] private GameObject redsPawsWithJar;
        [SerializeField] private GameObject redsPawsWithPhone;
        [SerializeField] private GameObject jarOnTheTable;
        [SerializeField] private GameObject packOfMoney;
        [SerializeField] private Transform silversEyes;

        [Header("main kitchen scene (red's sprites setup)")]
        [SerializeField] SpriteRenderer body;
        [SerializeField] SpriteRenderer leftEye, rightEye;
        [SerializeField] SpriteMask leftEyeMask, rightEyeMask;
        [SerializeField] Sprite calmBody, calmLeftEye, calmRightEye;
        [SerializeField] Sprite sadBody, sadLeftEye, sadRightEye;

        [Header("show jar scene")]
        [SerializeField] private AudioSource music;
        [SerializeField] private GameObject showJarScene;
        [SerializeField] private Transform pawsAndJar;
        [SerializeField] private Character redOnShowingJarScene;


        protected override void InitTyped(ListenDialoguesEncounter encounter){}

        protected override async Task StartScenarioTyped(ListenDialoguesEncounter encounter)
        {
            await SceneInShower();
            await SceneWithMobilePhone();
            await IntroSceneInKitchen();
            await MainSceneInKitchen1();
            await ShowJar();
            await MainSceneInKitchen2();
        }

        private async Task SceneInShower()
        {
            parentTurnTapeObject.gameObject.SetActive(true);
            var waterMain = water.main;
            var steamMain = steam.main;

            await paw.DOMove(new Vector3(0, -8), 0.5f).AsyncWaitForCompletion();
            squekSounds.Play();
            tape.DORotate(new Vector3(0, 0, 75), 0.5f);
            await paw.DOMove(new Vector3(0, -12.5f), 0.5f).AsyncWaitForCompletion();
            waterMain.maxParticles = 200;
            steamMain.maxParticles = 35;

            await paw.DOMove(new Vector3(0, -8), 0.5f).AsyncWaitForCompletion();
            tape.DORotate(new Vector3(0, 0, 75 * 2), 0.5f);
            await paw.DOMove(new Vector3(0, -12.5f), 0.5f).AsyncWaitForCompletion();
            waterMain.maxParticles = 100;
            steamMain.maxParticles = 20;

            await paw.DOMove(new Vector3(0, -8), 0.5f).AsyncWaitForCompletion();
            tape.DORotate(new Vector3(0, 0, 75 * 3), 0.5f);
            await paw.DOMove(new Vector3(0, -12.5f), 0.5f).AsyncWaitForCompletion();
            waterMain.maxParticles = 0;
            steamMain.maxParticles = 0;

            await Task.Delay(1500);
            await paw.DOMove(new Vector3(0, -30), 1f).AsyncWaitForCompletion();

            newMessageSound.Play();
            await Task.Delay(1500);
            await redInBathroomIntro.Say("А?");

            parentTurnTapeObject.gameObject.SetActive(false);
        }

        private async Task SceneWithMobilePhone()
        {
            bathroomPhoneScene.gameObject.SetActive(true);
            await phoneAndPawsParentInBathroom.DOMove(new Vector3(0, -18.8f), 0.75f).AsyncWaitForCompletion();
            await rightPaw.DOLocalMove(new Vector3(9f, 7f, 0), 0.5f).AsyncWaitForCompletion();
            blockedScreen.SetActive(false);
            unlockPhoneSound.Play();
            await rightPaw.DOLocalMove(new Vector3(11.85f, 0f), 0.5f).AsyncWaitForCompletion();
            await Task.Delay(500);
            await redInBathroomWithPhone.Say("Какого?..");
            await phoneAndPawsParentInBathroom.DOMove(new Vector3(0, -43f), 0.75f).AsyncWaitForCompletion();
            bathroomPhoneScene.gameObject.SetActive(false);
        }

        private async Task IntroSceneInKitchen()
        {
            parentIntroKitchenScene.SetActive(true);
            darkKitchenBackground.SetActive(true);
            lightenKitchenBackground.SetActive(false);
            await DotweenSteps(redOnIntroKitchen.transform, new Vector3(-8, -8), new Vector3(1.1f, 0.75f), 0.8f, 3);
            await Task.Delay(1000);
            switchSound.Play();
            darkKitchenBackground.SetActive(false);
            lightenKitchenBackground.SetActive(true);
            await Task.Delay(1000);
            await silverOnIntroKitchen.Say("Приветики!");
            await redOnIntroKitchen.Say("Ты же понимаешь, что ты не можешь просто так вламываться ко мне домой?");
            await silverOnIntroKitchen.Say("У тебя было открыто.");
            await redOnIntroKitchen.Say("И почему ты тут сидишь в темноте?");
            await silverOnIntroKitchen.Say("У меня игривое настроение.");
            await redOnIntroKitchen.Say("...");
            await silverOnIntroKitchen.Say("Ой, ну, не веди себя так, будто бы не рад меня видеть.");
            parentIntroKitchenScene.SetActive(false);
        }

        private async Task MainSceneInKitchen1()
        {
            parentMainKitchenScene.SetActive(true);
            await DotweenSteps(redOnMainKitchen.transform, new Vector3(5.64f, -7.14f), new Vector3(1.25f, 0.75f), 1f, 3);
            await redOnMainKitchen.Say("Я рад.");
            //как-то наколхозить объятия
            await redOnMainKitchen.Say("И так.<pause:0.75> Зачем ты пришел?");
            await silverOnMainKitchen.Say("Я что не могу просто прийти проведать своего братишку?");
            await redOnMainKitchen.Say("Ты можешь,<pause:0.75>  но ты так не делаешь.");
            await silverOnMainKitchen.Say("Как плохо ты обо мне думаешь.");
            await silverOnMainKitchen.Say("Ладно,<pause:0.75> ты меня подловил.");
            await silverOnMainKitchen.Say("Я и вправду пришел не просто так.<pause:0.75>  У меня есть предложение.");
            await redOnMainKitchen.Say("НЕТ!");
            await silverOnMainKitchen.Say("Но ты даже не выслушал.");
            await redOnMainKitchen.Say("НЕТ!<pause:0.75> Я вышел из игры!<pause:0.75> Я теперь обычный повар, забыл?");
            await silverOnMainKitchen.Say("Ты накладываешь мороженое в детском заведении.<pause:0.75> Ты не повар.");
            await redOnMainKitchen.Say("Я много практикуюсь дома!");
            await silverOnMainKitchen.Say("И у я знаю, что у тебя хорошо получается.<pause:0.75> Но мы оба знаем, Повар - это не хобби,<pause:0.75> а профессия.");
            await redOnMainKitchen.Say("Ты пришел обзываться или что?");
            await silverOnMainKitchen.Say("Я на самом деле пришел тебе с этим помочь.");
            await redOnMainKitchen.Say("Во-первых мне не нравится.<pause:0.75> А во-вторых ты же ничего не понимаешь в готовке.");
            await silverOnMainKitchen.Say("Зато я понимаю, что ты идешь к своей цели не правильно.");
            await redOnMainKitchen.Say("Много ты понимаешь!");
            await silverOnMainKitchen.Say("Вообще-то да, не мало.<pause:0.75> Рыжий, ты не вывозишь.");
            await silverOnMainKitchen.Say("Более того, то, что через что ты проходишь тебя не приближает к цели.");
            await silverOnMainKitchen.Say("Скорее отдаляет.");
            await redOnMainKitchen.Say("А вот и нет!");
            await redOnMainKitchen.Say("Посмотри ка на это!");
            redOnMainKitchen.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            await DotweenSteps(redOnMainKitchen.transform, new Vector3(26f, -7.14f), new Vector3(1.25f, 0.75f), 1f, 3);
            redsPawsEmpty.SetActive(false);
            redsPawsWithJar.SetActive(true);
            lookingForJarSounds.Play();
            await Task.Delay(2500);
            redOnMainKitchen.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            await DotweenSteps(redOnMainKitchen.transform, new Vector3(5.64f, -7.14f), new Vector3(1.25f, 0.75f), 1f, 3);
            await redOnMainKitchen.Say("Узри!");
            parentMainKitchenScene.SetActive(false);
        }

        private async Task ShowJar()
        {
            showJarScene.SetActive(true);
            music.Play();
            await pawsAndJar.DOMove(new Vector3(0, -2f), 0.75f).AsyncWaitForCompletion();
            await pawsAndJar.DOMove(new Vector3(0, -3.5f), 0.15f).AsyncWaitForCompletion();
            await Task.Delay(1000);
            await redOnShowingJarScene.Say("Видишь?<pause:0.5> Я уже на пол пути к цели!");
            await redOnShowingJarScene.Say("Я каждый день хожу на эту чертову работу не просто так!");
            await redOnShowingJarScene.Say("С каждым месяцем я заполняю баночку все сильнее!");
            await redOnShowingJarScene.Say("А когда я ее заполню, я открою ресторан с самой вкусными блюдами!");
            await redOnShowingJarScene.Say("И в нем будут возмутительно долгие перерывы для сотрудников!");
            await redOnShowingJarScene.Say("Но работать в нем будет так классно, что никто не захочет ими пользоваться!");
            await redOnShowingJarScene.Say("И больше никто не скажет, что Рыжий - не повар!");
            await redOnShowingJarScene.Say("Никто!");
            await pawsAndJar.DOMove(new Vector3(0, -30f), 0.35f).AsyncWaitForCompletion();
            showJarScene.SetActive(false);
        }

        private async Task MainSceneInKitchen2()
        {
            redsPawsEmpty.SetActive(true);
            redsPawsWithJar.SetActive(false);
            jarOnTheTable.SetActive(true);
            parentMainKitchenScene.SetActive(true);
            music.DOFade(0.25f, 1.5f);
            await silverOnMainKitchen.Say("Да ты моя ворчливая булочка.");
            await silverOnMainKitchen.Say("Я и не спорю, что ты трудишься очень усердно.");
            await silverOnMainKitchen.Say("Скорее наоборот, ты трудишься так усердно, что ты забыл держать нос по ветру.");
            await silverOnMainKitchen.Say("Рыжий,<pause:0.5> когда ты поставил себе эту цель?");
            await redOnMainKitchen.Say("Полтора года назад.");
            await silverOnMainKitchen.Say("Ты видел, как цены за это время подскочили?");
            await redOnMainKitchen.Say("Ну, цены всегда по-немногу растут.");
            await silverOnMainKitchen.Say("Просто посмотри, сколько стоит то оборудование, которое ты хотел купить тогда.");
            await redOnMainKitchen.Say("Ну не может же быть все настолько плохо.");
            await redsPawsEmpty.transform.DOScale(new Vector3(1, 0, 1), 0.5f).AsyncWaitForCompletion();
            redsPawsWithPhone.gameObject.SetActive(true);
            redOnMainKitchen.LookAt(redsPawsWithPhone.transform);
            await redsPawsWithPhone.transform.DOLocalMove(new Vector3(0, -1.35f, 0), 0.5f).AsyncWaitForCompletion();

            for(int i = 0; i < 2; i++)
            {
                typingSounds.Play();
                for (int j = 0; j < 20; j++)
                {
                    await redsPawsWithPhone.transform.DOShakeRotation(0.1f, 10).AsyncWaitForCompletion();
                }
                typingSounds.Pause();
                await Task.Delay(1000);
            }
            sadFoxSound.Play();
            ChangeRedsSprites(sadBody, sadLeftEye, sadRightEye);
            redOnMainKitchen.transform.DOShakeScale(0.1f, -0.1f);
            redOnMainKitchen.transform.DOMove(new Vector3(5.64f, -7.64f), 0.3f);

            await Task.Delay(2000);

            await silverOnMainKitchen.Say("Я уверен, что ты своим упрямоством можешь горы свернуть.");
            await silverOnMainKitchen.Say("Но, если продолжишь в том же духе, то еще через полтора года ты отдалишься от цели еще сильнее.");
            await silverOnMainKitchen.Say("Вещи дорожают быстрее, чем ты зарабатываешь.");
            redOnMainKitchen.LookAt(silversEyes);
            await redOnMainKitchen.Say("И что мне тогда делать?");
            await silverOnMainKitchen.Say("Действовать.");
            packOfMoney.SetActive(true);
            redOnMainKitchen.LookAt(packOfMoney.transform);
            await Task.Delay(1500);
            await silverOnMainKitchen.Say("Здесь все еще не хватит на то, чтобы покрыть все твою задумку в полной мере.");
            await silverOnMainKitchen.Say("Но это неплохой стартовый капитал, чтобы начать с чего-то более скромного.");

            ChangeRedsSprites(calmBody, sadLeftEye, sadRightEye);
            redOnMainKitchen.transform.DOShakeScale(0.1f, -0.1f);
            redOnMainKitchen.transform.DOMove(new Vector3(5.64f, -7.14f), 0.3f);

            await silverOnMainKitchen.Say("Например, с ресторанчика в городе поменьше.");
            await silverOnMainKitchen.Say("Я уже присмотрел одно неплохое местечко. Наших денег хватит на первый взнос.");
            redOnMainKitchen.LookAt(silversEyes);
            await redOnMainKitchen.Say("Первый взнос? Кредит звучит довольно рискованно");
            await silverOnMainKitchen.Say("Конечно, но ты же сам знаешь, как это работает. Кто не рискует, тот не пьет шампанское.");
            await redOnMainKitchen.Say("А вообще знаешь что?");
            ChangeRedsSprites(calmBody, calmLeftEye, calmRightEye);
            redOnMainKitchen.transform.DOShakeScale(0.1f, -0.1f);
            await redOnMainKitchen.Say("А ты прав!");
            await redOnMainKitchen.Say("К черту кафе мороженое!<pause:0.75> К черту их всех!");
            await redOnMainKitchen.Say("Если я еще хотя бы раз буду говорить как пират - я свихнусь!");
            await silverOnMainKitchen.Say("Вот это настрой!.");
            await redOnMainKitchen.Say("... А тебе оно зачем? Зачем помогать мне?");
            await silverOnMainKitchen.Say("Ну, во-первых, потому что я переживаю за мего младшего братишку.");
            await silverOnMainKitchen.Say("Который без меня обязательно наделает глупостей.");
            await silverOnMainKitchen.Say("А во-вторых я хочу в долю.");
            await redOnMainKitchen.Say("Я не верю тебе.");
            await redOnMainKitchen.Say("Ты буквально можешь делать деньги из воздуха.");
            await redOnMainKitchen.Say("Зачем тебе доля какой-то кафешки?");
            await silverOnMainKitchen.Say("Ой, ну ты правда думал, что я никогда не повзрослею?");
            await silverOnMainKitchen.Say("Не захочу остепениться? обзавестись легальным источником дохода?");
            await silverOnMainKitchen.Say("Ты меня обижаешь.");
            await redOnMainKitchen.Say("...");
            await silverOnMainKitchen.Say("Ну что такое? Ты правда думаешь, что я пытаюсь тебя обвести вокруг пальца?");
            await silverOnMainKitchen.Say("Сколько раз я пытался как-то навредить тебе? Обокрасть? Подставить");
            await redOnMainKitchen.Say("Ни разу.");
            await silverOnMainKitchen.Say("Тогда чего же ты ждешь?");
            await silverOnMainKitchen.Say("Партнеры?");
            await redOnMainKitchen.Say("Ладно, вытащи меня из этой дыры.");
        }

        private async Task DotweenStep(Transform steppingTransform, Vector3 targetPosition, float time)
            => await DotweenStep(steppingTransform, targetPosition, new Vector3(1.1f, 0.75f), time);

        private async Task DotweenStep(Transform steppingTransform, Vector3 targetPosition, Vector3 squeezeValue, float time)
        {
            steppingTransform.DOMove(targetPosition, time);
            await steppingTransform.DOScale(squeezeValue, time * 0.5f).AsyncWaitForCompletion();
            await steppingTransform.DOScale(Vector3.one, time * 0.5f).AsyncWaitForCompletion();
        }

        private async Task DotweenSteps(Transform steppingTransform, Vector3 targetPosition, Vector3 squeezeValue, float time, int stepsAmount)
        {
            var startPosition = steppingTransform.position;

            for (int i = 1; i <= stepsAmount; i++)
            {
                var intermediateTargetPosition =
                    Vector3.Lerp(startPosition, targetPosition, (float)i / stepsAmount);

                await DotweenStep(
                    steppingTransform,
                    intermediateTargetPosition,
                    squeezeValue,
                    time / stepsAmount);
            }
        }

        private void ChangeRedsSprites(Sprite bodySprite, Sprite leftEyeSprite, Sprite rightEyeSprite)
        {
            body.sprite = bodySprite;
            leftEye.sprite = leftEyeSprite;
            leftEyeMask.sprite = leftEyeSprite;
            rightEye.sprite = rightEyeSprite;
            rightEyeMask.sprite = rightEyeSprite;
        }

        /*
         * things todo:
         * нарисовать сценку, где Рыжий в ванной смотрит в телефончик, там мессенджер, перепска с Серым и в TMP написано (выходи на кухню)  
         * придумать, как наколхозить объятия
         * нарисовать расстроенного Рыжего (будет классно, если получится через движение ушей)
         * нарисовать пачку деняк на столе
         * нарисовать сценку с рукопожатием
         */
    }
}