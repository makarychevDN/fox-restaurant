using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private DataBetweenScenesContainer dataBetweenScenesContainer;

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
            }
        }

        private async Task RunLevel()
        {
            while (currentIndex < encounters.Count)
            {
                Encounter encounter = encounters[currentIndex];
                await encounter.StartEncounter();
                currentIndex++;
            }
        }
    }
}
