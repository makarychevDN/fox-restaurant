using UnityEngine;
using UnityEngine.SceneManagement;

namespace foxRestaurant
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private string gameplaySceneName = "Gameplay";
        [SerializeField] private DataBetweenScenesContainer dataBetweenScenesContainer;

        private EncountersListAsset encountersList;
        private int startEncounterIndex;

        public void SetupLevel(EncountersListAsset encountersList, int startEncounterIndex)
        {
            this.encountersList = encountersList;
            this.startEncounterIndex = startEncounterIndex;
        }

        public void LoadLevel()
        {
            print("load " + encountersList.name);
            dataBetweenScenesContainer.EncountersList = encountersList;
            dataBetweenScenesContainer.StartEncounterIndex = startEncounterIndex;
            SceneManager.LoadScene(gameplaySceneName);
        }
    }
}