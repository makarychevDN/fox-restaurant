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
            await adele.Say("I have to admit, you two did an excellent job, little foxes.".Locailze());
            await adele.Say("The people of Riverside are feeling much better now.".Locailze());
            await adele.Say("And maybe now this witch hunt will finally come to an end.".Locailze());

            await red.Say("So you really are a witch after all!".Locailze());

            await adele.Say("I'm about as much of a witch as you are a Leshy.".Locailze());
            await adele.Say("I'm a fortune teller,<pause:0.5> and a terrible one at that.<pause:0.5> Then again, so are all fortune tellers.".Locailze());
            await adele.Say("But to the locals, it's all the same.".Locailze());
            await adele.Say("They'll gladly believe the most unbelievable explanation.".Locailze());
            await adele.Say("...".Locailze());
            await adele.Say("You distracted me.".Locailze());
            await adele.Say("You've both worked hard, and you've earned yourselves a reward.".Locailze());
            await adele.Say("I have a bit of influence both here and in Clifford.".Locailze());
            await adele.Say("And I also happen to own quite a few interesting little things.".Locailze());
            await adele.Say("Each of you gets one wish.".Locailze());
            await adele.Say("Choose wisely,<pause:0.5> but don't get greedy.".Locailze());

            await red.Say("...".Locailze());
            await red.Say("I want to keep that oven.".Locailze());

            await adele.Say("You don't ask for small favors, do you, Red?".Locailze());
            await adele.Say("But it's a fair price, considering what you've done.".Locailze());
            await adele.Say("Besides, it's been gathering dust here anyway.".Locailze());
            await adele.Say("I hate cooking.".Locailze());
            await adele.Say("It's yours.".Locailze());
            await adele.Say("And what about you, Silver?<pause:0.5> What do you want?".Locailze());

            await silver.Say("I want you to get us to Clifford as soon as possible.".Locailze());

            await adele.Say("t<volume:0>.<pause:0.5>.<pause:0.5>.<pause:0.5><volume:1> Fine.".Locailze());
            await adele.Say("We do have a backup plan for emergencies.".Locailze());
            await adele.Say("I suppose<pause:0.5> today qualifies.".Locailze());
            await adele.Say("But if she comes back complaining that you two gave her a hard time...".Locailze());

            await silver.Say("We'll be perfect gentlemen!".Locailze());

            await adele.Say("That's the right answer.".Locailze());
            await adele.Say("Pack your things,<pause:0.5> the next ride to Clifford leaves in ten minutes.".Locailze());
        }
    }
}