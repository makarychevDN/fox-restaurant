using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class PirateRestaurantScenarioPart7 : BaseScenario<ListenDialoguesEncounter>
    {
        [Header("turnTapeScene")]
        [SerializeField] private GameObject parentTurnTapeObject;
        [SerializeField] private Transform paw;
        [SerializeField] private Transform tape;
        [SerializeField] private AudioSource squekSounds;
        [SerializeField] private ParticleSystem water;

        protected override void InitTyped(ListenDialoguesEncounter encounter){}

        protected override async Task StartScenarioTyped(ListenDialoguesEncounter encounter)
        {
            await SceneInShower();
        }

        private async Task SceneInShower()
        {
            var waterMain = water.main;

            await paw.DOMove(new Vector3(0, -8), 0.5f).AsyncWaitForCompletion();
            squekSounds.Play();
            tape.DORotate(new Vector3(0, 0, 75), 0.5f);
            await paw.DOMove(new Vector3(0, -12.5f), 0.5f).AsyncWaitForCompletion();
            waterMain.maxParticles = 200;

            await paw.DOMove(new Vector3(0, -8), 0.5f).AsyncWaitForCompletion();
            tape.DORotate(new Vector3(0, 0, 75 * 2), 0.5f);
            await paw.DOMove(new Vector3(0, -12.5f), 0.5f).AsyncWaitForCompletion();
            waterMain.maxParticles = 100;

            await paw.DOMove(new Vector3(0, -8), 0.5f).AsyncWaitForCompletion();
            tape.DORotate(new Vector3(0, 0, 75 * 3), 0.5f);
            await paw.DOMove(new Vector3(0, -12.5f), 0.5f).AsyncWaitForCompletion();
            waterMain.maxParticles = 0;

            await Task.Delay(1500);
            await paw.DOMove(new Vector3(0, -30), 1f).AsyncWaitForCompletion();
        }
    }
}