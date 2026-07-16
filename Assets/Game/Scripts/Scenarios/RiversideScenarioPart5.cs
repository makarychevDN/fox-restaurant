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

        protected override void InitTyped(ListenDialoguesEncounter encounter)
        {
            
        }

        protected override async UniTask StartScenarioTyped(ListenDialoguesEncounter encounter)
        {
            await UniTask.Delay(1000);
            await red.Say("Проверка.");
            await adele.Say("Проверка.");
            poofSounds.Play();
            await UniTask.Delay(1000);
        }
    }
}