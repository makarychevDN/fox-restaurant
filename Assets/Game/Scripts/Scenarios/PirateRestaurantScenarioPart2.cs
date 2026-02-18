using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class PirateRestaurantScenarioPart2 : BaseScenario<ListenDialoguesEncounter>
    {
        [SerializeField] private Character red;
        [SerializeField] private Character boss;
        [SerializeField] private Transform sandwich;
        [SerializeField] private Transform bossesEyes;
        [SerializeField] private Transform redsParent;
        [SerializeField] private Transform hands;
        [SerializeField] private Transform hornPosition;
        [SerializeField] private ParticleSystem bossAppearParticles;
        [SerializeField] private AudioSource poofSound;
        [SerializeField] private AudioSource hornSound;

        protected override void InitTyped(ListenDialoguesEncounter encounter) { }

        protected override async Task StartScenarioTyped(ListenDialoguesEncounter encounter)
        {
            red.LookAt(sandwich);
            await Task.Delay(1000);
            await red.Say("Наконец-то минута покоя.");
            red.LookAt(bossesEyes);
            redsParent.transform.DOMove(new Vector3(-7.19f, -7.94f), 0.2f);
            hands.transform.DOLocalMove(new Vector3(1.13999999f, -2.5f, 0), 0.2f);
            boss.gameObject.SetActive(true);
            poofSound.Play();
            bossAppearParticles.Play();
            await boss.Say("Йарр! <pause:0.75> Ну что, салага, думал, что первый день будет простым?");
            await red.Say("Я тут работаю больше года.");
            await boss.Say("Йарр! <pause:0.75> Я ни одного слова не понял!<pause:0.75> Ты говоришь, как сухопутная крыса!");
            await red.Say("Я не хочу говорить так, пока я не за кассой.<pause:0.75> Это<pause:0.5> глупо.");
            await red.Say("Мы же не пираты в обычный жизни.");
            await boss.Say("<volume:0>...");
            await red.Say("<volume:0>.<pause:0.5>.<pause:0.5>.<pause:0.5><volume:1> *вздох*");
            await red.Say("Йарр!<pause:0.75> Капитан,<pause:0.75> я в этом плаванье уже больше года!");
            await boss.Say("<volume:1>Йарр!<pause:0.75> Вот так бы сразу!<pause:0.75> Продолжай в том же духе и сокровище не заставит себя долго ждать!");
            await red.Say("К слову, о сокровищах.<pause:0.75> Я как раз хотел обсудить мое повышение и ... ");
            await boss.Say("И выше нос, юнга!");
            await boss.Say("Не знаю что там у тебя случилось такого на суше.");
            await boss.Say("Но, если будешь дальше ходить таким угрюмым, распугаешь мне всех клиентов!<pause:0.75> Йарр! ");
            await red.Say("Это просто...");
            boss.gameObject.SetActive(false);
            poofSound.Play();
            bossAppearParticles.Play();
            await red.Say("...Мое лицо.");
            await red.Say("<volume:0>.<pause:0.5>.<pause:0.5>.<pause:0.5><volume:1> Полоумная псина.");
            red.LookAt(hornPosition);
            hornSound.Play();
            await Task.Delay(2000);
            await red.Say("Уже?!");
            await red.Say("Я готов поклясться, что с каждым днем перерыв становится короче!");
        }
    }
}