using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public abstract class Encounter : MonoBehaviour
    {
        public abstract Task StartEncounter();
    }
}