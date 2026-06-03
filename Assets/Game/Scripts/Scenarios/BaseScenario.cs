using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;


namespace foxRestaurant
{
    public abstract class BaseScenario : MonoBehaviour
    {
        public abstract UniTask StartScenario(Encounter encounter);

        public abstract void Init(Encounter encounter);
    }

    public abstract class BaseScenario<T> : BaseScenario where T : Encounter
    {
        public sealed override async UniTask StartScenario(Encounter encounter)
        {
            await StartScenarioTyped((T)encounter);
        }

        protected abstract UniTask StartScenarioTyped(T encounter);

        public sealed override void Init(Encounter encounter)
        {
            InitTyped((T)encounter);
        }

        protected abstract void InitTyped(T encounter);
    }
}