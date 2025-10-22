using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class WaitingEncounter : Encounter
    {
        [SerializeField] private float duration = 3f;

        public override void Init() { }
    }
}