using UnityEngine;

namespace foxRestaurant
{
    public class LevelLoader : MonoBehaviour
    {
        private EncountersList encountersList;
        public void SetEncaunters(EncountersList encountersList) => this.encountersList = encountersList;

        public void LoadLevel()
        {
            print(encountersList.name);
        }
    }
}