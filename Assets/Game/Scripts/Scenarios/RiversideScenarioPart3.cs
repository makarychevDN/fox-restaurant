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
            await silver.Say("Your place is so cozy!".Locailze());
            await red.Say("What's all this stuff on the shelves?".Locailze());
            await adele.Say("Don't touch the antiques.".Locailze());
            await red.Say("Wasn't planning to.".Locailze());
            await red.Say("...".Locailze());

            await silver.Say("Thank you for standing up for us.".Locailze());
            await red.Say("Yeah,<pause:0.5> if it weren't for you, they would've eaten us alive.".Locailze());
            await adele.Say("*sigh*<pause:0.5> They wouldn't have eaten anyone,<pause:0.5> they're not monsters.".Locailze());
            await adele.Say("They're just on edge.".Locailze());
            await adele.Say("Most likely, they would've just chased you out of Riverside.".Locailze());
            await adele.Say("Worst case, they would've shouted a few nasty things after you.".Locailze());

            await red.Say("...".Locailze());
            await red.Say("Actually, it suits us.".Locailze());
            List<UniTask> tasks = new List<UniTask>
            {
                red.Say("We were just passing through anyway. We weren't planning to stick around this godforsaken...".Locailze()),
                silversPaw.DOLocalMove(new Vector3(-3.75f, 4), 2).ToUniTask()
            };
            await UniTask.WhenAll(tasks);
            popSounds.Play();
            await silversPaw.DOLocalMove(new Vector3(-5.3f, 4.5f), 0.2f).ToUniTask();
            red.LookAt(pointToLookOnSilversPaw);
            await silver.Say("What a shame.".Locailze());
            await silver.Say("We'd hate to leave such a lovely place in such a hurry.".Locailze());
            await silver.Say("Especially after accidentally upsetting the locals.".Locailze());
            await adele.Say("You'd better listen to your elders, orange one.<pause:0.75> People value that around here.".Locailze());
            await adele.Say("And you shouldn't be in such a rush anyway.<pause:0.5> The road ahead is even tougher than the one that brought you here.".Locailze());
            silversPaw.DOLocalMove(new Vector3(1.2f, -0.5f), 2);
            await adele.Say("Am I right in assuming you're headed for Clifford?".Locailze());
            red.LookAt(adelesEyes);
            await silver.Say("Yes, ma'am!".Locailze());
            await adele.Say("Strange choice, but that's your business.".Locailze());
            await adele.Say("Our cart horse already left for Clifford to pick up medicine. He won't be back until late tonight.".Locailze());
            await adele.Say("The next trip to town is tomorrow morning.".Locailze());
            await silver.Say("So until then, we'd better stay put and keep a low profile?".Locailze());
            await adele.Say("Exactly.<pause:0.5> Unless the evil Leshy and the filthy Clifforder want to push the locals' patience even further.".Locailze());
            await silver.Say("Perfect!<pause:0.75> Then we'll sit here quietly and stay out of everyone's way.".Locailze());
            await adele.Say("Smart little fox.".Locailze());

            await UniTask.Delay(1000);
            await red.Say("...".Locailze());
            await red.Say("They are waiting for medicines from the city.<pause:0.5> Are they sick?".Locailze());
            await adele.Say("Yep.<pause:0.75> Those scatterbrains managed to catch some kind of illness, and now half the village's come down with it.".Locailze());
            await adele.Say("That's why they're so afraid.".Locailze());
            await adele.Say("They think it's some evil spirit, or the people of Clifford are behind it.".Locailze());
            await adele.Say("Or both.".Locailze());

            await red.Say("Actually,<pause:0.5> we could help.".Locailze());
            silver.LookAt(redsEyes);
            await silver.Say("Red,<pause:0.5> you've got a lot of talents,<pause:0.5> but you're not a doctor.".Locailze());
            red.LookAt(silversEyes);
            await silver.Say("And besides, we're not exactly welcome here.".Locailze());
            red.LookAt(adelesEyes);
            await adele.Say("No, no, gray one.<pause:0.5> Let him finish.".Locailze());
            await adele.Say("So, what exactly are you proposing, orange one?".Locailze());
            await red.Say("You've got all sorts of herbs here,<pause:0.5> and I've got a whole bunch of cooking gear with me!".Locailze());
            await red.Say("And I know the recipe for good health!".Locailze());
            await red.Say("We can make the tastiest, most healing chicken soup this village's ever had!".Locailze());
            silver.LookAt(adelesEyes);
            await adele.Say("That's...<pause:1> actually a pretty good idea.".Locailze());
            await adele.Say("A good bowl of soup really should help them feel better.".Locailze());
            await adele.Say("I'll see if I can convince the locals to put up with you a little longer.".Locailze());
            await silver.Say("Well,<pause:0.5> since we're already involved, I can help with the herbs.".Locailze());
            await adele.Say("Do you know anything about herbs?".Locailze());
            await silver.Say("Our mom's really into floristry, so she taught me a thing or two.".Locailze());
            await silver.Say("I don't know what they're used for medicinally, but I do know how to gather them.".Locailze());
            await silver.Say("And I can follow instructions without getting in the way.".Locailze());
            await adele.Say("That'll do.".Locailze());
            await adele.Say("And since we're working together now, and I won't be able to ignore you anymore, we should introduce ourselves.".Locailze());
            await adele.Say("Adele.".Locailze());
            await silver.Say("Silver.".Locailze());
            await red.Say("Red.".Locailze());
            await UniTask.Delay(1000);
            await adele.Say("How original.".Locailze());
            await silver.Say("Oh,<pause:0.5> trust me,<pause:0.5> it could've been much worse.".Locailze());
            impactSound.Play();
            await Camera.main.ShakeCamera(1);
            await UniTask.Delay(500);
            await red.Say("What was that?".Locailze());
            await adele.Say("Don't worry about it.".Locailze());
            await adele.Say("Come on, Red and Silver,<pause:0.5> we've still got plenty of work to do.".Locailze());
        }
    }
}