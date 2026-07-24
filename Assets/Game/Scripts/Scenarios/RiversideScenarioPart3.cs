using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class RiversideScenarioPart3 : BaseScenario<ListenDialoguesEncounter>
    {
        [SerializeField] private Character red;
        [SerializeField] private Character silver;
        [SerializeField] private Character adele;
        [SerializeField] private Transform adelesEyes;
        [SerializeField] private Transform silversEyes;
        [SerializeField] private Transform redsEyes;
        [SerializeField] private Transform silversPaw;
        [SerializeField] private Transform pointToLookOnSilversPaw;
        [SerializeField] private AudioSource popSounds;
        [SerializeField] private AudioSource impactSound;

        protected override void InitTyped(ListenDialoguesEncounter encounter) 
        {
            red.LookAt(adelesEyes);
            silver.LookAt(adelesEyes);
            red.SetDialoguePopUpCentering(DialogueDisplayer.Centering.Center);
            silver.SetDialoguePopUpCentering(DialogueDisplayer.Centering.Center);
        }

        protected override async UniTask StartScenarioTyped(ListenDialoguesEncounter encounter)
        {

            await UniTask.Delay(1000);
            await silver.Say("Как у вас уютно!");
            await red.Say("А что это у вас на полочках?");
            await adele.Say("Не трогать антиквариат.");
            await red.Say("Не очень то и хотелось.");
            await red.Say("...");

            await silver.Say("Спасибо, что заступились за нас.");
            await red.Say("Да,<pause:0.5> если бы не вы, они бы нас уже съели.");
            await adele.Say("*вздох*<pause:0.5> Они бы никого не съели,<pause:0.5> они не изверги.");
            await adele.Say("Они просто очень встревожены и напуганы.");
            await adele.Say("Скорее всего они бы вас просто прогнали за черту Риверcайда.");
            await adele.Say("В худшем случае покричали бы обидных вещей вслед.");

            await red.Say("...");
            await red.Say("Вообще, нам это подходит.");
            List<UniTask> tasks = new List<UniTask>
            {
                red.Say("Мы все равно шли мимо и даже не думали задерживаться в этой богом забытой..."),
                silversPaw.DOLocalMove(new Vector3(-3.75f, 4), 2).ToUniTask()
            };
            await UniTask.WhenAll(tasks);
            popSounds.Play();
            await silversPaw.DOLocalMove(new Vector3(-5.3f, 4.5f), 0.2f).ToUniTask();
            red.LookAt(pointToLookOnSilversPaw);
            await silver.Say("Какая жалость.");
            await silver.Say("Нам бы так не хотелось покидать это славное место в спешке.");
            await silver.Say("Еще и ненароком расстроив местных жителей.");
            await adele.Say("Лучше слушай старших, оранжевенький.<pause:0.75> Это тут в почете.");
            await adele.Say("А еще вам лучше не спешить, потому что путь дальше будет еще труднее, чем сюда.");
            silversPaw.DOLocalMove(new Vector3(1.2f, -0.5f), 2);
            await adele.Say("Я же правильно понимаю, что вы направлятесь в Клиффорд?");
            red.LookAt(adelesEyes);
            await silver.Say("Так точно, мэм!");
            await adele.Say("Странный выбор, но это ваше дело.");
            await adele.Say("Наша ездовая лошадка уже отправилась туда за лекарствами и приедет только поздно вечером.");
            await adele.Say("Следующая отправка в город завтра утром.");
            await silver.Say("Получается, нам до тех пор лучше сидеть и не отсвечивать?");
            await adele.Say("Именно,<pause:0.5> только, если злобный леший и гнусный клиффордец не захотят испытывать терпение местных дальше.");
            await silver.Say("Прекрасно!<pause:0.75> Тогда мы посидим здесь тихонько и не будем никому мешать.");
            await adele.Say("Умная лисичка.");

            await UniTask.Delay(1000);
            await red.Say("...");
            await red.Say("Им везут лекарства из города.<pause:0.5> Они что болеют?");
            await adele.Say("Дап.<pause:0.75> Эти растяпы умудрились подхватить какую-то болячку и теперь половина деревни простудилась.");
            await adele.Say("От того они и боятся.");
            await adele.Say("Думают, что это какая-то нечистая сила или происки клиффордцев.");
            await adele.Say("Или все вместе.");

            await red.Say("Вообще, <pause:0.5> мы можем помочь.");
            silver.LookAt(redsEyes);
            await silver.Say("Рыжий,<pause:0.5> у тебя, конечно, много талантов,<pause:0.5> но ты не доктор.");
            red.LookAt(silversEyes);
            await silver.Say("А еще нам здесь не очень рады.");
            red.LookAt(adelesEyes);
            await adele.Say("Нет-нет, серенький,<pause:0.5> пусть продолжает.");
            await adele.Say("Что ты конкретно предлагаешь, оранжевенький?");
            await red.Say("У вас есть всякие травки-муравки,<pause:0.5> а у меня есть с собой куча кухонной утвари!");
            await red.Say("А еще я знаю рецепт здоровья!");
            await red.Say("Мы можем наварить самого вкусного и самого лекарственного куриного супчика на всю деревню!");
            silver.LookAt(adelesEyes);
            await adele.Say("Это...<pause:1> довольно хорошая идея.");
            await adele.Say("Им и вправду должно стать полегче от хорошего супа.");
            await adele.Say("Я попробую убедить местных потерпеть вас чуть больше.");
            await silver.Say("Ну,<pause:0.5> раз мы уже ввязались, я могу помочь с травами.");
            await adele.Say("А ты умеешь?");
            await silver.Say("Наша мама увлекается флористикой и кое чему меня научила.");
            await silver.Say("Я не знаю их лечебных свойств, но я умею собирать.");
            await silver.Say("А еще я могу делать то, что вы скажете и не мешаться под рукой.");
            await adele.Say("Годится.");
            await adele.Say("И раз мы теперь сотрудничаем и я не смогу вас игнорировать, давайте знакомиться.");
            await adele.Say("Адель.");
            await silver.Say("Серый.");
            await red.Say("Рыжий.");
            await UniTask.Delay(1000);
            await adele.Say("Как оригинально.");
            await silver.Say("Ох,<pause:0.5> поверьте,<pause:0.5> могло быть гораздо хуже.");
            impactSound.Play();
            await Camera.main.ShakeCamera(1);
            await UniTask.Delay(500);
            await red.Say("Что это было?");
            await adele.Say("Не обращайте внимания.");
            await adele.Say("Пойдемте, Рыжий и Серый,<pause:0.5> у нас еще много работы.");
        }
    }
}