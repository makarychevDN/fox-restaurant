using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization;

namespace foxRestaurant
{
    public class PirateRestaurantScenarioPart4 : BaseScenario<ListenDialoguesEncounter>
    {
        [SerializeField] private Character red;
        [SerializeField] private Character lameLarry;
        [SerializeField] private Transform stansEyes;
        [SerializeField] private Transform lameLarrysEyes;

        [SerializeField] List<LocalizedString> dialogueLines;
        private int stringsCounter = 0;
        private int Next => stringsCounter++;

        protected override void InitTyped(ListenDialoguesEncounter encounter) { }

        protected override async Task StartScenarioTyped(ListenDialoguesEncounter encounter)
        {
            await lameLarry.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await lameLarry.Say(dialogueLines[Next]);
            red.LookAt(stansEyes);
            await red.Say(dialogueLines[Next]);
            await lameLarry.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await lameLarry.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await lameLarry.Say(dialogueLines[Next]);
            await lameLarry.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await lameLarry.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await lameLarry.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await lameLarry.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await lameLarry.Say(dialogueLines[Next]);
        }
    }
}