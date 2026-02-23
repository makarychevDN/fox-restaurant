using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization;

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

        [SerializeField] List<LocalizedString> dialogueLines;
        private int stringsCounter = 0;
        private int Next => stringsCounter++;

        protected override void InitTyped(ListenDialoguesEncounter encounter) { }

        protected override async Task StartScenarioTyped(ListenDialoguesEncounter encounter)
        {
            red.LookAt(sandwich);
            await Task.Delay(1000);
            await red.Say(dialogueLines[Next]);
            red.LookAt(bossesEyes);
            redsParent.transform.DOMove(new Vector3(-7.19f, -7.94f), 0.2f);
            hands.transform.DOLocalMove(new Vector3(1.13999999f, -2.5f, 0), 0.2f);
            boss.gameObject.SetActive(true);
            poofSound.Play();
            bossAppearParticles.Play();
            await boss.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await boss.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await boss.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await boss.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            await boss.Say(dialogueLines[Next]);
            await boss.Say(dialogueLines[Next]);
            await boss.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            boss.gameObject.SetActive(false);
            poofSound.Play();
            bossAppearParticles.Play();
            await red.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
            red.LookAt(hornPosition);
            hornSound.Play();
            await Task.Delay(2000);
            await red.Say(dialogueLines[Next]);
            await red.Say(dialogueLines[Next]);
        }
    }
}