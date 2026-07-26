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
            await red.Say("It's just impossible!".Locailze());
            await red.Say("This patient of yours is the most stubborn mule in the world!".Locailze());
            await red.Say("Sometimes I think he asks for anything except what he actually needs just to be difficult.".Locailze());
            await red.Say("Adele,<pause:0.5> I need you to yell at him.".Locailze());
            await adele.Say("Now that's a brilliant idea.<pause:0.5> Anything else?".Locailze());
            await red.Say("I want you to hit him.".Locailze());
            await adele.Say("Listen here, orange one.".Locailze());
            await adele.Say("Vasya is a tough nut to crack,<pause:0.5> he's not easy.".Locailze());
            await adele.Say("But your brother and I already have our hands full.".Locailze());
            await adele.Say("We won't get very far if I have to help you with every difficult patient.".Locailze());
            await adele.Say("I have my own work,<pause:0.5> and you have yours.".Locailze());
            await adele.Say("So go and handle it.".Locailze());
            await red.Say("Hey,<pause:0.5> I'm the one saving your village from an epidemic here!".Locailze());
            await red.Say("I could've done exactly what you told me!".Locailze());
            await red.Say("Sit quietly in a corner,<pause:0.5> while the locals wait for medicine from the city and keep getting sicker!".Locailze());
            await adele.Say("t<volume:0>.<pause:0.5>.<pause:0.5>.<pause:0.5><volume:1> Alright, alright,<pause:0.5> you're right,<pause:0.5> I'm sorry.".Locailze());
            await adele.Say("But I really can't drop my work and keep running back and forth.".Locailze());
            await red.Say("Then tell me how else I'm supposed to get through to him.".Locailze());

            poofSounds.Play();
            oven.gameObject.SetActive(true);
            ovenParticles.Play();
            red.LookAt(oven);

            await UniTask.Delay(1500);
            red.LookAt(adelesEyes);
            await red.Say("What is this old wreck?".Locailze());
            await adele.Say("This is an oven.<pause:0.5> You know how to use one of these, right?".Locailze());
            await red.Say("Yeah, I do,<pause:0.5> but how is this supposed to help?".Locailze());
            red.LookAt(oven);
            await adele.Say("It has the gift of persuasion.".Locailze());
            await adele.Say("If someone tastes its baked goods, they'll immediately change their mind, no matter what the argument was about.".Locailze());
            await red.Say("...".Locailze());
            await adele.Say("What?".Locailze());
            red.LookAt(adelesEyes);
            await red.Say("If you just wanted to get rid of me, you could've said so.".Locailze());
            await adele.Say("I promise it'll work.<pause:0.5> Now go,<pause:0.5> your patients are waiting.".Locailze());
            await UniTask.Delay(500);
        }
    }
}