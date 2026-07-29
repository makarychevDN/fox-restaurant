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
            await horse.Say("Hiii there, little foxy.".Locailze());
            red.LookAt(horsesEyes);
            await red.Say("Hi there, horsie.".Locailze());
            await horse.Say("Heading off to make it big in the big city, huh?".Locailze());
            await red.Say("Not really,<pause:0.5> quite the opposite.".Locailze());
            await red.Say("We got so tired of making it big in the city that we decided to start somewhere smaller.".Locailze());
            await horse.Say("Clifford?<pause:0.5> Small?!".Locailze());
            await red.Say("Yeah,<pause:0.5> always has been.".Locailze());
            await horse.Say("Oh...".Locailze());
            await horse.Say("I always thought it was huge.".Locailze());
            await red.Say("Silver said Clifford's about the size of a single district in Metropolis.".Locailze());
            await horse.Say("Wow.".Locailze());
            await horse.Say("I've always wanted to get there,<pause:0.5> to see a big city.".Locailze());
            await horse.Say("I knew it wasn't the biggest one out there.".Locailze());
            await horse.Say("But I was sure it had to be at least in the top ten.".Locailze());
            await horse.Say("And now you're telling me this.".Locailze());
            await horse.Say("That's actually kind of disappointing.".Locailze());
            await red.Say("So why don't you go?".Locailze());
            await horse.Say("I can't.".Locailze());
            await horse.Say("My dad takes people to Clifford and back every day.".Locailze());
            await horse.Say("Some of us live here but work in the city,<pause:0.5> like Auntie Adele.".Locailze());
            await horse.Say("And I'm supposed to stay in the village just in case.".Locailze());
            await horse.Say("While Dad's away, I'm the only one who knows how to handle the cart.".Locailze());
            await horse.Say("He's always telling me to forget about my responsibilities for a day.".Locailze());
            await horse.Say("To come into the city with him.".Locailze());
            await horse.Say("But I just can't.".Locailze());
            await horse.Say("What if something happens while I'm off having fun?".Locailze());
            await horse.Say("Like today, for example.<pause:0.5> You two showed up out of nowhere.".Locailze());
            await horse.Say("Who would've taken you to the city if I wasn't here?".Locailze());
            await red.Say("I guess we'd have had to wait until morning and go with your dad.".Locailze());
            await red.Say("And waiting is something we really can't afford right now.".Locailze());
            await red.Say("We've got a loan hanging over our heads, and we need to start paying it off as soon as possible.".Locailze());
            await red.Say("You've really helped us out.".Locailze());
            await horse.Say("...".Locailze());
            await horse.Say("Little fox,<pause:0.5> what's your name?".Locailze());         
            await red.Say("Red.<pause:0.5> And yours?".Locailze());
            await horse.Say("I'm Daisy.".Locailze());
            await red.Say("Nice to meet you, Daisy.".Locailze());
            await horse.Say("Red,<pause:0.5> what's it like living in a city that's even bigger than Clifford?".Locailze());
            await red.Say("It's... not the safest place.".Locailze());
            await horse.Say("Oh...".Locailze());
            await red.Say("...".Locailze());
            await red.Say("Daisy,<pause: 0.5> why do the people of Riverside dislike Clifford so much, <pause:0.5> but still trying to reach there?".Locailze());
            await horse.Say("I don't know.<pause:0.5> It's always been a complicated relationship.".Locailze());
            await horse.Say("And then there was that whole thing with the new road.".Locailze());
            await red.Say("What happened? (Что за случай?)".Locailze());
            await horse.Say("Not long ago, we found out they're building a road from Clifford to the main highway.".Locailze());
            await red.Say("Oh,<pause:0.5> is that the highway with the bus stop?".Locailze());
            await horse.Say("Excactly!".Locailze());
            await horse.Say("We were all excited, thinking it would pass through our village and finally give us an easy way into the city.".Locailze());
            await horse.Say("But it turned out they're building it through the desert instead of our forest to save a little money.".Locailze());
            await horse.Say("I don't let anyone see it,<pause:0.5> but<pause:0.5> between you and me,<pause:0.5> it really hurt.".Locailze());
            await horse.Say("If the village had a road and even just one car, I'd worry a lot less.".Locailze());
            await horse.Say("I'd know the villagers wouldn't be in danger without me around.".Locailze());
            await red.Say("Oh.".Locailze());
            await red.Say("I'm sorry.".Locailze());
            await red.Say("If you ever do make it to the city,<pause:0.5> come visit us.".Locailze());
            await red.Say("We're opening a restaurant.".Locailze());
            await red.Say("We haven't come up with a name yet, but you'll know it when you see it.".Locailze());
            await red.Say("It's going to be the coolest place in the world!<pause:0.5> You won't be able to miss it.".Locailze());
            await horse.Say("Wooow,<pause:0.5> that sounds amazing!".Locailze());
            await horse.Say("I'd love to!".Locailze());
            await horse.Say("And your soup was really delicious!".Locailze());
            await horse.Say("I'll definitely stop by if I get the chance!".Locailze());
            await red.Say("It's a deal.".Locailze());
        }
    }
}