using foxRestaurant;
using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class PirateRestaurantScenarioPart3 : BaseScenario<RestaurantEncounter>
    {
        [SerializeField] private Character red;

        protected override async Task StartScenarioTyped(RestaurantEncounter encounter)
        {
            await IntroSpeech();
        }

        private async Task IntroSpeech()
        {
            await red.Say("я еще ему покажу.<pause:1> ¬сем им покажу.");
            await red.Say("ќткрою ресторан,<pause:0.5> возьму всех на работу,<pause:0.5> а потом сделаю им возмутительно короткие перерывы.");
            await red.Say("ј себе дам возмутительно длинный перерыв!");
            await red.Say("Ќо € не захочу им пользоватьс€, таким классным будет мое заведение!");
            await red.Say("<volume:0>.<pause:0.5>.<pause:0.5>.<pause:0.5><volume:1> Ќаверное стоит сделать мой ресторан чуть менее классным");
            await red.Say("ј то они сами не захот€т пользоватьс€ перерывами.");
        }
    }
}