using Cysharp.Threading.Tasks;
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
            Cursor.visible = false;
        }

        protected override async UniTask StartScenarioTyped(ListenDialoguesEncounter encounter)
        {
            await Speak(blackPopUpWhiteText);
            await Speak(whitePopUpBlackText);
        }

        private async UniTask Speak(Character character)
        {
            await SpeakRussian(character);
            await SpeakEnglish(character);
            await UniTask.Delay(1000);
        }

        private async UniTask SpeakRussian(Character character)
        {
            await UniTask.Delay(1000);
            await character.Say("Я справлюсь.");
            await UniTask.Delay(1000);
            await character.Say("Нужно всего лишь дождаться нужных ингредиентов.");
            await UniTask.Delay(1000);
            await character.Say("Нарезать.");
            await UniTask.Delay(1000);
            await character.Say("И смешать.");
        }

        private async UniTask SpeakEnglish(Character character)
        {
            await UniTask.Delay(1000);
            await character.Say("I've got this.");
            await UniTask.Delay(1000);
            await character.Say("Just need to wait for the right ingredients.");
            await UniTask.Delay(1000);
            await character.Say("Chop.");
            await UniTask.Delay(1000);
            await character.Say("And mix.");
        }
    }
}