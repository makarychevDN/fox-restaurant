using System.Collections.Generic;
using UnityEngine;

namespace foxRestaurant
{
    public class Ticker : MonoBehaviour
    {
        [SerializeField] private int sliceTimeCost;
        private List<ITickable> tickables = new List<ITickable>();
        private List<ITickable> toRemove = new List<ITickable>();
        private TickingMode tickingMode = TickingMode.regular;

        public void AddTickable(ITickable tickable) => tickables.Add(tickable);
        public void RemoveTickable(ITickable tickable) => toRemove.Add(tickable);

        public void TickAllTickables(float tick)
        {
            foreach (var tickable in tickables)
            {
                tickable.Tick(tick);
            }

            if (toRemove.Count > 0)
            {
                foreach (var r in toRemove)
                    tickables.Remove(r);
                toRemove.Clear();
            }
        }

        public void TickOnSlice() => TickAllTickables(sliceTimeCost);

        /*private void Update()
        {
            if (tickingMode == TickingMode.pause)
                return;

            TickAllTickables(Time.deltaTime * (int)tickingMode);
        }*/

        private enum TickingMode
        {
            pause = 0,
            regular = 1,
            speedX2 = 2,
            speedX4 = 4
        }

        public void Pause() => tickingMode = TickingMode.pause;
        public void SetRegularTickingSpeed() => tickingMode = TickingMode.regular;
        public void SetX2TickingSpeed() => tickingMode = TickingMode.speedX2;
        public void SetX4TickingSpeed() => tickingMode = TickingMode.speedX4;
    }
}