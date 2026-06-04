using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private DataBetweenScenesContainer dataBetweenScenesContainer;
        [SerializeField] private Image fading;
        [SerializeField] private Button pauseButton;
        [SerializeField] private GameplayPauseMenu pauseMenu;

        private List<Encounter> encounters = new();
        private int currentIndex = 0;
        private Encounter currentEncounter;

        public Encounter CurrentEncounter => currentEncounter;

        private async void Start()
        {
            fading.material = new Material(fading.material);

            pauseButton.onClick.AddListener(OpenPauseMenu);

            Init(dataBetweenScenesContainer.EncountersList);
            print(dataBetweenScenesContainer.EncountersList.name + " is loaded");
            await RunLevel();
        }

        public void Init(EncountersListAsset prefabsList)
        {
            foreach (var encounterPrefab in prefabsList.Encounters)
            {
                var spawnedEncounter = Instantiate(encounterPrefab);
                encounters.Add(spawnedEncounter);
                spawnedEncounter.gameObject.SetActive(false);
                spawnedEncounter.transform.parent = transform;
            }

            fading.GetComponentInParent<Canvas>().sortingOrder = 9999;
        }

        private async UniTask RunLevel()
        {
            while (currentIndex < encounters.Count)
            {
                currentEncounter = encounters[currentIndex];
                currentEncounter.gameObject.SetActive(true);
                currentEncounter.Init();
                currentEncounter.scenario.Init(currentEncounter);
                await fading.FadeOut();
                await currentEncounter.scenario.StartScenario(currentEncounter);
                await fading.FadeIn();
                currentEncounter.gameObject.SetActive(false);
                currentIndex++;
            }

            SceneManager.LoadScene("Main Menu");
        }

        public void OpenPauseMenu()
        {
            Time.timeScale = 0;
            pauseMenu.gameObject.SetActive(true);
        }

        public void ClosePauseMenu()
        {
            Time.timeScale = 1;
            pauseMenu.gameObject.SetActive(false);
        }
    }
}
