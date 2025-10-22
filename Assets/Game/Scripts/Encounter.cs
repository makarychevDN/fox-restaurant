using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public abstract class Encounter : MonoBehaviour
    {
        public BaseScenario scenario;

        public abstract void Init();
    }
}