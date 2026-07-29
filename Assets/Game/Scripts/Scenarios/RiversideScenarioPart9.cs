using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class RiversideScenarioPart9 : BaseScenario<ListenDialoguesEncounter>
    {
        [SerializeField] private Character red;
        [SerializeField] private Character silver;
        [SerializeField] private Character adele;
        [SerializeField] private Transform adelesEyes;

        protected override void InitTyped(ListenDialoguesEncounter encounter)
        {
            red.LookAt(adelesEyes);
            silver.LookAt(adelesEyes);
        }

        protected override async UniTask StartScenarioTyped(ListenDialoguesEncounter encounter)
        {
            await adele.Say("Я должна признать, вы отлично справились, лисятки.");
            await adele.Say("Жителям Риверсайда стало получше и поспокойней.");
            await adele.Say("Наконец-то закончится охота на ведьм.");

            await red.Say("Так вы все-таки настоящая ведьма!");

            await adele.Say("Я настолько же ведьма, насколько ты леший.");
            await adele.Say("Я гадалка, причем паршивая.<pause:0.5> Впрочем, как и все гадалки.");
            await adele.Say("Хотя, для местных что в лоб, что по лбу.");
            await adele.Say("Они с радостью поверят в самый невероятный вариант.");
            await adele.Say("...");
            await adele.Say("Ты меня отвлек.");
            await adele.Say("Вы хорошо поработали и заслужили награду.");
            await adele.Say("У меня и здесь и в Клиффорде есть какое-никакое влияние.");
            await adele.Say("А еще у меня есть много занятных вещиц.");
            await adele.Say("Каждому из вас дается по желанию.");
            await adele.Say("Используйте их с умом, но не наглейте.");

            await red.Say("...");
            await red.Say("Я хочу себе оставить ту печь.");

            await adele.Say("А у тебя губа не дура, Рыжий.");
            await adele.Say("Хотя, цена справедливая, учитывая, что ты сделал.");
            await adele.Say("Да и все равно она тут пылится без дела.");
            await adele.Say("Терпеть не могу готовку.");
            await adele.Say("Она твоя.");
            await adele.Say("А ты чего хочешь, Серый?");

            await silver.Say("Я хочу, чтобы вы нас доставили в Клиффорд как можно скорее.");

            await adele.Say("<volume:0>.<pause:0.5>.<pause:0.5>.<pause:0.5><volume:1> Ладно");
            await adele.Say("У нас для план Б для крайних случаев.");
            await adele.Say("Думаю,<pause:0.5> можно им сегодня воспользоваться.");
            await adele.Say("Но, если она мне нажалуется, что вы ее обижали...");

            await silver.Say("Мы будем джентельменами!");

            await adele.Say("Правильный ответ.");
            await adele.Say("Собирайте вещи,<pause:0.5> ближайший рейс до Клиффорда через 10 минут.");
        }
    }
}