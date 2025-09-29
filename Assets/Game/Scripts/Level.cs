using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private DataBetweenScenesContainer dataBetweenScenesContainer;
        [SerializeField] private Image fading;

        private List<Encounter> encounters = new();
        private int currentIndex = 0;

        private async void Start()
        {
            Init(dataBetweenScenesContainer.EncountersList);
            print(dataBetweenScenesContainer.EncountersList.name + " is loaded");
            await RunLevel();
        }

        public void Init(EncountersListAsset prefabsList)
        {
            foreach (var encounterPrefab in prefabsList.Encounters)
            {
                var spawnedEncaunter = Instantiate(encounterPrefab);
                encounters.Add(spawnedEncaunter);
                spawnedEncaunter.gameObject.SetActive(false);
                spawnedEncaunter.transform.parent = transform;
            }

            fading.GetComponentInParent<Canvas>().sortingOrder = 9999;
        }

        private async Task RunLevel()
        {
            while (currentIndex < encounters.Count)
            {
                Encounter encounter = encounters[currentIndex];
                encounter.gameObject.SetActive(true);
                FadeOut();
                await encounter.StartEncounter();
                await FadeIn();
                encounter.gameObject.SetActive(false);
                currentIndex++;
            }
        }

        public async Task FadeIn()
        {
            Tween tween = fading.DOFade(1f, 0.4f);
            await tween.AsyncWaitForCompletion();
        }

        public async Task FadeOut()
        {
            Tween tween = fading.DOFade(0f, 0.4f);
            await tween.AsyncWaitForCompletion();
        }
    }
}
