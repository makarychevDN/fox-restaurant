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
        [SerializeField] private Transform redsEyes;
        [SerializeField] private Transform herbs;
        [SerializeField] private Transform silversPaw;
        [SerializeField] private Transform silversEyes;
        [SerializeField] private Transform theCenterOfBoiler;
        [SerializeField] private Transform citizens;
        [SerializeField] private Animator redsHandsAnimator;
        [SerializeField] private AudioSource poofSound;
        [SerializeField] private AudioSource splashSound;
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
            silver.LookAt(citizens);
            red.LookAt(citizens);
            redsHandsAnimator.SetBool("isMixing", false);
            await hog.Say("Ты что совсем с дубу рухнул?");
            await hog.Say("Ты еще что-то Лешему предъявлять будешь?");
            await goat.Say("Да никакой он не Леший!<pause:0.5> Выглядит, как обычный лис,<pause:0.5> только замызганный.");

            await silver.Say("Мне кажется, или я сейчас услышал неуважение к великому Лешему?");

            await hog.Say("Это не я!<pause:0.5> Это все он!");
            await goat.Say("А я что?!<pause:0.5> А я ничего!");

            hog.gameObject.SetActive(false);
            hogsPoofEffect.Play();
            poofSound.Play();

            await UniTask.Delay(350);

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

        }
    }
}