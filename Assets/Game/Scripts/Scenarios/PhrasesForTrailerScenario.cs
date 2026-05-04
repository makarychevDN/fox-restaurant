using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class PhrasesForTrailerScenario : BaseScenario<ListenDialoguesEncounter>
    {
        [SerializeField] private Character blackPopUpWhiteText;
        [SerializeField] private Character whitePopUpBlackText;

        protected override void InitTyped(ListenDialoguesEncounter encounter)
        {
        }

        protected override async Task StartScenarioTyped(ListenDialoguesEncounter encounter)
        {
            await Speak(blackPopUpWhiteText);
            await Speak(whitePopUpBlackText);
        }

        private async Task Speak(Character character)
        {
            await SpeakRussian(character);
            await SpeakEnglish(character);
            await Task.Delay(1000);
        }

        private async Task SpeakRussian(Character character)
        {
            await Task.Delay(1000);
            await character.Say("Я справлюсь.");
            await Task.Delay(1000);
            await character.Say("Нужно всего лишь дождаться нужных ингредиентов.");
            await Task.Delay(1000);
            await character.Say("Нарезать.");
            await Task.Delay(1000);
            await character.Say("И смешать.");
        }

        private async Task SpeakEnglish(Character character)
        {
            await Task.Delay(1000);
            await character.Say("I've got this.");
            await Task.Delay(1000);
            await character.Say("Just need to wait for the right ingredients.");
            await Task.Delay(1000);
            await character.Say("Chop.");
            await Task.Delay(1000);
            await character.Say("And mix.");
        }
    }
}