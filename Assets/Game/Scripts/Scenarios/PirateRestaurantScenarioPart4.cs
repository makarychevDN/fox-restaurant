using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class PirateRestaurantScenarioPart4 : BaseScenario<ListenDialoguesEncounter>
    {
        [SerializeField] private Character red;
        [SerializeField] private Character lameLarry;
        [SerializeField] private Transform stansEyes;
        [SerializeField] private Transform lameLarrysEyes;

        protected override void InitTyped(ListenDialoguesEncounter encounter) { }

        protected override async Task StartScenarioTyped(ListenDialoguesEncounter encounter)
        {
            await lameLarry.Say("Привет, Рыжий.");
            await red.Say("Привет, Стэн.");
            await lameLarry.Say("Мои глаза ниже.");
            red.LookAt(stansEyes);
            await red.Say("Ой.");
            await lameLarry.Say("Да,<pause:0.75> так лучше.");
            await red.Say("Так ты чего хотел?");
            await lameLarry.Say("<volume:0>.<pause:0.5>.<pause:0.5>.<pause:0.5><volume:1> Мне сейчас придется выйти в зал.");
            await red.Say("Что?!<pause:0.75> Нет!<pause:0.75> Я против!");
            await lameLarry.Say("Капитан сказал, что пустит меня на корм рыбам,<pause:0.5> если я этого не сделаю.");
            await lameLarry.Say("Я не знаю, что это именно значит в переводе на нормальный,<pause:0.5> но наверняка ничего хорошего.");
            await red.Say("Но дети без ума от Хромого Джо!");
            await red.Say("Они здесь все разнесут, если увидят тебя в костюме!");
            await red.Say("Я не смогу обслужить столько народу!");
            await red.Say("За кассой должно работать хотя бы трое, чтобы мы могли потянуть это!");
            await red.Say("А я сегодня один!<pause:0.75> Вспомни, что было в прошлый раз!");
            await lameLarry.Say("Я знаю.<pause:0.75> Поэтому я пришел обсудить, что делать.");
            await red.Say("И что,<pause:0.5> у тебя уже есть идеи?");
            await lameLarry.Say("Да,<pause:0.5> киновечер.<pause:0.5> Ты зашторишь окна,<pause:0.5> включишь проектор,<pause:0.5> а я буду убеждать прибывших вести себя потише.");
            await red.Say("Киновечер, говоришь.");
            await red.Say("Придется взять на себя вечернее меню с коктейлями.<pause:0.75> Будет трудновато.");
            await lameLarry.Say("Все еще проще, чем, если сюда разом сбежится вся ребятня со всей округи.");
            await red.Say("<volume:0>.<pause:0.5>.<pause:0.5>.<pause:0.5><volume:1> Да,<pause:0.5> ты прав.<pause:0.75> Да ты гений, Стэн.<pause:0.75> Так и сделаем.");
            await lameLarry.Say("Договорились.");
        }
    }
}