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
            await red.Say($"<volume:0>.<pause:0.5>.<pause:0.5>.<pause:0.5><volume:1> Сейчас.");
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
            await silver.Say("Как думаешь,<pause:0.5> это считается за снадобье?");
            red.LookAt(silversEyes);
            await red.Say("Вряд-ли,<pause:0.5> Это просто супчик с травками.");
            await silver.Say("Да,<pause:0.5> но что тогда вообще считать снадобьем, если не супчик с травами?");
            redsHandsAnimator.SetBool("isMixing", false);
            await red.Say("Хм,<pause:0.5> а это<pause:0.75> - очень интересное наблюдение.");
            await silver.Say("Зельевар-подмастерье превзошел своего учителя!");
            redsHandsAnimator.SetBool("isMixing", true);
            await red.Say("Сосредоточься!");
            red.LookAt(theCenterOfBoiler);
            await silver.Say("Ой, да ладно тебе,<pause:0.5> было же забавно!");
            red.LookAt(silversEyes);
            await red.Say("Кухня - это не место для веселья!");
            await red.Say("Это наша мастерская и мы творим здесь великие дела!");
            red.LookAt(theCenterOfBoiler);
            await silver.Say($"<volume:0>.<pause:0.5>.<pause:0.5>.<pause:0.5><volume:1> И все-таки это было забавно.");
            redsHandsAnimator.SetBool("isMixing", false);
            red.LookAt(silversEyes);
            await red.Say("Ладно,<pause:0.5> это было слегка забавно.");
            await silver.Say($"Маленькая победа!");
            redsHandsAnimator.SetBool("isMixing", true);
            red.LookAt(theCenterOfBoiler);
            await red.Say("Ты - шалопай!");

            await citizens.DOMove(new Vector3(0, 3.65f), 2).AsyncWaitForCompletion().AsUniTask();
            goat.SetDialoguePopUpCentering(DialogueDisplayer.Centering.Right);
            await goat.Say("Ты слышал?");
            await goat.Say("Никакое это не снадобье от болезней,<pause:0.5> а обычный суп!");
            await hog.Say("Ну, не знаю, Вась.<pause:0.5> Мне от его варева стало лучше.");
            await hog.Say("А еще это самый вкусный супчик, который я когда-либо пробовал!");
            silver.LookAt(citizens);
            red.LookAt(citizens);
            await goat.Say("Радуйся, пока можешь.");
            await goat.Say("И так понятно, что он нас откармливает, чтобы мы сами стали вкуснее.");
            redsHandsAnimator.SetBool("isMixing", false);
            await hog.Say("Ты что совсем с дубу рухнул?");
            await hog.Say("Ты еще что-то Лешему предъявлять будешь?");
            await goat.Say("Да мы бы прогнали этого Лешего взашей, если бы не чертова ведьма!");

            await red.Say("Я не Ле...");
            await silver.Say("Мне кажется, или я сейчас услышал неуважение к великому Лешему?");

            await hog.Say("Это не я!<pause:0.5> Это все он!");
            hog.gameObject.SetActive(false);
            hogsPoofEffect.Play();
            poofSound.Play();
            await UniTask.Delay(350);

            await goat.Say("А я что?!<pause:0.5> А я ничего!");
            goat.gameObject.SetActive(false);
            goatsPoofEffect.Play();
            poofSound.Play();
            await UniTask.Delay(350);

            await silver.Say("Хе-хе.");
            await red.Say("*вздох*");

            red.LookAt(theCenterOfBoiler);
            silver.LookAt(theCenterOfBoiler);
            redsHandsAnimator.SetBool("isMixing", true);
            await UniTask.Delay(1500);

            farImpactSound.volume = 0.33f;
            farImpactSound.Play();
            await Camera.main.ShakeCamera(1);

            red.LookAt(peopleBeyondScreen[0].transform);
            silver.LookAt(peopleBeyondScreen[0].transform);
            await peopleBeyondScreen[0].Say("Он вырвался на свободу!");

            farImpactSound.volume = 0.67f;
            sneezeSound.volume = 0.25f;
            farImpactSound.Play();
            sneezeSound.Play();
            await Camera.main.ShakeCamera(1);
            await UniTask.Delay(1000);

            peopleBeyondScreen[1].SetDialoguePopUpCentering(DialogueDisplayer.Centering.Right);
            red.LookAt(peopleBeyondScreen[1].transform);
            silver.LookAt(peopleBeyondScreen[1].transform);
            await peopleBeyondScreen[1].Say("Спасайтесь, кто может!");

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