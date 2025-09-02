using System.Collections.Generic;
using UnityEngine;

public class Ticker : MonoBehaviour
{
    [SerializeField] private int sliceTimeCost;
    private List<ITickable> tickables = new List<ITickable>();
    private TickingMode tickingMode = TickingMode.regular;

    public void AddTickable(ITickable tickable)
    {
        tickables.Add(tickable);
    }

    public void TickAllTickables(float tick)
    {
        tickables.ForEach(tickable => tickable.Tick(tick));
    }

    public void TickOnSlice() => TickAllTickables(sliceTimeCost);

    private void Update()
    {
        if (tickingMode == TickingMode.pause)
            return;

        TickAllTickables(Time.deltaTime * (int)tickingMode);
    }

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
