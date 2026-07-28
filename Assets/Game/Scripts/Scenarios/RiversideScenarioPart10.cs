using Cysharp.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class RiversideScenarioPart10 : BaseScenario<ListenDialoguesEncounter>
    {
        [SerializeField] private Character red;
        [SerializeField] private Character horse;
        [SerializeField] private Transform horsesEyes;
        [SerializeField] private Transform pointToLookAt;

        protected override void InitTyped(ListenDialoguesEncounter encounter)
        {
            red.LookAt(pointToLookAt);
        }

        protected override async UniTask StartScenarioTyped(ListenDialoguesEncounter encounter)
        {
            await UniTask.Delay(1500);
            await horse.Say("Привеет, лисичка.");
            red.LookAt(horsesEyes);
            await red.Say("Привет, лошадка.");
            await horse.Say("Едете покорять большой город, а?");
            await red.Say("Да нет,<pause:0.5> наоборот.");
            await red.Say("Так устали покорять большой город, что решили начать с чего-то маленького.");
            await horse.Say("Это Клиффорд то маленький?!");
            await red.Say("Ну да,<pause:0.5> всегда был.");
            await horse.Say("Оу...");
            await horse.Say("Мне он всегда казался таким огромным.");
            await red.Say("Серый говорил, что Клиффорд размером примерно с один район Метрополиса.");
            await horse.Say("Ого.");
            await horse.Say("Я всегда хотела вырваться туда,<pause:0.5> посмотреть большой город.");
            await horse.Say("И я знала, что он не самый огромный.");
            await horse.Say("Но была уврена, что он входит хотя бы в топ 10.");
            await horse.Say("А ты мне тут говоришь такое.");
            await horse.Say("Даже немного обидно.");
            await red.Say("А почему не вырвешься?");
            await horse.Say("Я не могу,<pause:0.5> я еще маленькая.");
            await red.Say("Да посмотри на себя!<pause:0.5> Ты большая и сильная!");
            await red.Say("Ты в одиночку тянешь меня с братом и телегу!");
            await horse.Say("Нееет,<pause:0.5> я имею в виду, что я недостаточно взрослая.");
            await red.Say("А ты не пробовала напроситься в город с кем-то постарше?");
            await horse.Say("Я не могу.");
            await horse.Say("Папа возит жителей в Клиффорд и обратно каждый день.");
            await horse.Say("Некоторые живут здесь, а работают в городе,<pause:0.5> как тетушка Адель.");
            await horse.Say("А я должна оставаться в деревне на всякий случай.");
            await horse.Say("Пока папы дома нет, я - единственная, кто умеет обращаться с телегой.");
            await horse.Say("Он меня постоянно уговаривает забыть на денек об обязанностях.");
            await horse.Say("Погулять по городу с ним.");
            await horse.Say("А я не могу так.");
            await horse.Say("Вдруг что-нибудь случится, пока я буду там прохлаждаться?");
            await horse.Say("Сегодня, например, на нас свалились вы.");
            await horse.Say("Кто бы вас довез до города, если не я?");
            await red.Say("Полагаю, нам бы пришлось ждать до утра и отправляться в город с твоим отцом.");
            await red.Say("А ожидание для нас сейчас очень болезненно.");
            await red.Say("На нас висит кредит и нам нужно его начать выплачивать как можно скорее.");
            await red.Say("Ты нас очень выручаешь.");
            await horse.Say("...");
            await horse.Say("Лисичка,<pause:0.5> а как тебя зовут?");         
            await red.Say("Рыжий,<pause:0.5> а тебя?");
            await horse.Say("Меня зовут Ромашка.");
            await red.Say("Будем знакомы, Ромашка.");
            await horse.Say("Рыжий,<pause:0.5> а каково это жить в таком огромном городе, который даже больше Клиффорда?");
            await red.Say("Не совсем безопасно.");
            await horse.Say("Оу...");
            await red.Say("...");
            await red.Say("Ромашка,<pause:0.5> а как так вышло, что жители Риверсайда так недолюбливают Клиффорд,<pause:0.5> но все равно тянутся туда?");
            await horse.Say("Не знаю,<pause:0.5> у нас всегда были сложные отношения.");
            await horse.Say("А еще этот недавний случай с дорогой.");
            await red.Say("Что за случай?");
            await horse.Say("Не так давно узналось, что в Клиффорде прокладывают дорогу к большому шоссе.");
            await horse.Say("Мы уже обрадовались, что через деревню проложат дорогу и у нас появится удобный путь в город.");
            await horse.Say("Но оказалось, что дорогу прокладывают не через наш лес, а через пустыню, чтобы немного сэкономить.");
            await horse.Say("Я никому этого не показываю,<pause:0.5> но<pause:0.5> по секрету<pause:0.5> мне тоже обидно.");
            await horse.Say("Если бы у деревни появилась дорога и хотя бы одна машина, мне стало бы спокойней.");
            await horse.Say("Я бы знала, что без меня жители деревни не будут в опасности.");
            await red.Say("Оуу.");
            await red.Say("Мне жаль.");
            await red.Say("Если все-таки получится вырваться в город,<pause:0.5> приходи к нам.");
            await red.Say("Мы открываем ресторан.");
            await red.Say("Мы пока не придумали название, но ты его легко найдешь.");
            await red.Say("Это будет самое крутое заведение в мире!<pause:0.5> Ты такое точно не пропустишь.");
            await horse.Say("Огооо,<pause:0.5> как интересно!");
            await horse.Say("Я бы очень хотела!");
            await horse.Say("И твой супчик был очень вкусным!");
            await horse.Say("Я обязательно загляну, если получится!");
            await red.Say("Тогда договорились.");
        }
    }
}