using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class RiversideScenarioPart5 : BaseScenario<ListenDialoguesEncounter>
    {
        [SerializeField] private Character red;
        [SerializeField] private Character adele;
        [SerializeField] private Transform adelesEyes;
        [SerializeField] private AudioSource poofSounds;
        [SerializeField] private Transform oven;
        [SerializeField] private ParticleSystem ovenParticles;

        protected override void InitTyped(ListenDialoguesEncounter encounter)
        {
            red.LookAt(adelesEyes);
        }

        protected override async UniTask StartScenarioTyped(ListenDialoguesEncounter encounter)
        {
            await UniTask.Delay(1000);
            await red.Say("Это просто невозможно!");
            await red.Say("Этот ваш больной - самый упрямый баран в мире!");
            await red.Say("Мне иногда кажется, что он требует что угодно, кроме того, что ему действительно нужно просто из вредности.");
            await red.Say("Адель,<pause:0.5> мне нужно, чтобы вы на него накричали.");
            await adele.Say("А это ты здорово придумал.<pause:0.5> Еще что хочешь?");
            await red.Say("Чтобы вы ударили его.");
            await adele.Say("Слушай сюда, оранжевенький.");
            await adele.Say("Вася - крепкий орешек,<pause:0.5> с ним не просто.");
            await adele.Say("Но у нас с твоим братцем сейчас дел невпроворот.");
            await adele.Say("Мы далеко не уедем, если я буду помогать тебе с каждым капризным клиентом.");
            await adele.Say("У меня своя работа,<pause:0.5> у тебя своя.");
            await adele.Say("Так иди и справляйся с ней.");
            await red.Say("Эй,<pause:0.5> я тут вообще-то вашу деревню от эпидемии спасаю!");
            await red.Say("А мог бы сделать, как вы и сказали!");
            await red.Say("Сидеть и помалкивать в углу,<pause:0.5> пока местные ждут лекарства из города и заболевают все сильнее!");
            await adele.Say("<volume:0>.<pause:0.5>.<pause:0.5>.<pause:0.5><volume:1> Ладно-ладно,<pause:0.5> ты прав,<pause:0.5> извини.");
            await adele.Say("Но я правда не могу отрываться от работы, чтобы бегать туда-сюда.");
            await red.Say("Тогда скажите, как можно повлиять на него иначе.");

            poofSounds.Play();
            oven.gameObject.SetActive(true);
            ovenParticles.Play();
            red.LookAt(oven);

            await UniTask.Delay(1500);
            red.LookAt(adelesEyes);
            await red.Say("Это что за развалина?");
            await adele.Say("Это - печь.<pause:0.5> Ты же умеешь пользоваться такими?");
            await red.Say("Да, умею,<pause:0.5>  но как это должно помочь?");
            red.LookAt(oven);
            await adele.Say("Она обладает даром переубеждения.");
            await adele.Say("Если кто-то отведает ее выпечки, он тут же передумает, не важно о чем была речь.");
            await red.Say("...");
            await adele.Say("Что?");
            red.LookAt(adelesEyes);
            await red.Say("Если вы просто хотите от меня избавиться лучше бы так и сказали.");
            await adele.Say("Я обещаю, что оно сработает,<pause:0.5> а теперь иди, тебя ждут пациенты.");
            await UniTask.Delay(500);
        }
    }
}