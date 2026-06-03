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

        private async Task RunLevel()
        {
            while (currentIndex < encounters.Count)
            {
                Encounter encounter = encounters[currentIndex];
                encounter.gameObject.SetActive(true);
                encounter.Init();
                encounter.scenario.Init(encounter);
                await fading.FadeOut();
                await encounter.scenario.StartScenario(encounter);
                await fading.FadeIn();
                encounter.gameObject.SetActive(false);
                currentIndex++;
            }

            SceneManager.LoadScene("Main Menu");
        }

        private void OpenPauseMenu()
        {
            print("sex");
            Time.timeScale = 0;
            pauseMenu.gameObject.SetActive(true);
        }

        private void ClosePauseMenu()
        {
            Time.timeScale = 1;
            pauseMenu.gameObject.SetActive(false);
        }
    }
}
