using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class WaitingEncounter : Encounter
    {
        [SerializeField] private float duration = 3f;

        public override async Task StartEncounter()
        {
            await Task.Delay((int)(duration * 1000));
        }
    }
}