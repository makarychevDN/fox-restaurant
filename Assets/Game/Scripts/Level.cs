using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class Level : MonoBehaviour
    {
        private List<Encounter> encounters;
        private int currentIndex = 0;

        public void Init(EncountersList encountersList)
        {

        }

        private async void Start()
        {
            await RunLevel();
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
