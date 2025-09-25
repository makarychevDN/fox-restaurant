using UnityEngine;
using UnityEngine.SceneManagement;

namespace foxRestaurant
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private Object gameplayScene;
        [SerializeField] private DataBetweenScenesContainer dataBetweenScenesContainer;

        private EncountersListAsset encountersList;
        public void SetEncaunters(EncountersListAsset encountersList) => this.encountersList = encountersList;

        public void LoadLevel()
        {
            print("load " + encountersList.name);
            dataBetweenScenesContainer.EncountersList = encountersList;
            SceneManager.LoadScene(gameplayScene.name);
        }
    }
}