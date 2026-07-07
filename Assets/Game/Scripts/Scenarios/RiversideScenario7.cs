using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class RiversiwdeScenarioPart7 : BaseScenario<ListenDialoguesEncounter>
    {
        [SerializeField] private List<AudioSource> backgroundAmbients;

        protected override void InitTyped(ListenDialoguesEncounter encounter)
        {
            print("wow");
            foreach (AudioSource ambient in backgroundAmbients)
            {
                ambient.DOFade(1, 1);
            }
        }

        protected override async UniTask StartScenarioTyped(ListenDialoguesEncounter encounter)
        {
            await UniTask.Delay(30000);
        }
    }
}