using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class RiversideScenarioPart11 : BaseScenario<ListenDialoguesEncounter>
    {
        [SerializeField] private Transform background;
        [SerializeField] private Character bottomRed;
        [SerializeField] private Character bottomSilver;
        [SerializeField] private Character bottomHorse;
        [SerializeField] private Character topRed;
        [SerializeField] private Character topSilver;
        [SerializeField] private Character topHorse;
        [SerializeField] private AudioSource backgroundMusic;

        protected override void InitTyped(ListenDialoguesEncounter encounter)
        {
            backgroundMusic.DOFade(1, 10);
        }

        protected override async UniTask StartScenarioTyped(ListenDialoguesEncounter encounter)
        {
            await bottomHorse.Say("Wake up, little foxes!".Locailze());
            await bottomHorse.Say("We're almost there!".Locailze());
            await bottomRed.Say("Huh?<pause:0.5> What?".Locailze());
            await bottomSilver.Say("Open your eyes, Red!".Locailze());
            await bottomSilver.Say("We're here!".Locailze());
            await background.DOMove(new Vector3(0, 0, 0), 3).SetEase(Ease.InOutSine);

            await topRed.Say("Whoa! (ого)".Locailze());
            await topRed.Say("It really does look huge from up here!".Locailze());
            await topHorse.Say("See? I told you!".Locailze());
            await topHorse.Say("Someday I'll come here on my own!".Locailze());
            await topHorse.Say("And I'll explore every corner of Clifford!".Locailze());
            await topHorse.Say("...".Locailze());
            await topHorse.Say("But I have to head home now.".Locailze());
            await topHorse.Say("It'll be getting dark soon, and my dad will start worrying.".Locailze());
            await topHorse.Say("Sorry I can't take you any farther.".Locailze());
            await topSilver.Say("That's alright, sweetheart.<pause:0.5> You've already helped us more than enough.".Locailze());
            await topSilver.Say("We can find our way to our new place from here.".Locailze());
            await topRed.Say("Our new place?!".Locailze());
            await topRed.Say("Did you already buy us a building?!".Locailze());
            await topSilver.Say("Oh, no.<pause:0.5> Buying property online would be way too reckless.".Locailze());
            await topSilver.Say("But I did find a very promising place!".Locailze());
            await topSilver.Say("And I've got a really good feeling about it!".Locailze());
            await topSilver.Say("So we can head over there right now!".Locailze());

            await topRed.Say("Then what are we waiting for?!".Locailze());
            await topRed.Say("Lead the way!".Locailze());

            await topHorse.Say("Byyye, little foxes!".Locailze());
            await topRed.Say("Bye, Daisy!".Locailze());
            await topSilver.Say("Safe travels, Daisy!".Locailze());
            await topSilver.Say("Come on, Red.".Locailze());
            await topSilver.Say("That place isn't going to check itself out.".Locailze());
            await topRed.Say("You'd better start trembling, little house!".Locailze());
            await topRed.Say("You have no idea how hard I'm gonna inspect you!".Locailze());

            await background.DOMove(new Vector3(0, -22.5f, 0), 3).SetEase(Ease.InOutSine);
        }        
    }
}