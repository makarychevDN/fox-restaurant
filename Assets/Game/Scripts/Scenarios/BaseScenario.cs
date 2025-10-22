using System.Threading.Tasks;
using UnityEngine;


namespace foxRestaurant
{
    public abstract class BaseScenario : MonoBehaviour
    {
        public abstract Task StartScenario(Encounter encounter);
    }

    public abstract class BaseScenario<T> : BaseScenario where T : Encounter
    {
        public sealed override async Task StartScenario(Encounter encounter)
        {
            await StartScenarioTyped((T)encounter);
        }

        protected abstract Task StartScenarioTyped(T encounter);
    }
}