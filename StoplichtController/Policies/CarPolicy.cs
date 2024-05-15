using StoplichtController.Crossings;

namespace StoplichtController.Policies;

public class CarPolicy : Policy
{
    override public async Task<IPolicy?> Apply(Crossing crossing)
    {
        if (crossing.WaitList.Count <= 0) return await base.Apply(crossing);

        // Turn the light green for the lane with the highest priority and red for all other lanes
        var highestPriorityLane = crossing.WaitList.Peek();
        foreach (var lane in crossing.Roads.SelectMany(road => road.Value.Lanes))
        {
            if (lane == highestPriorityLane)
            {
                lane.Light.Green();
                await Task.Delay(TimeSpan.FromSeconds(4));
                lane.Light.Orange();
                await Task.Delay(TimeSpan.FromSeconds(1));
                lane.Light.Red();
                await Task.Delay(TimeSpan.FromSeconds(2));
                crossing.WaitList.Dequeue();

                break;
            }
            lane.Light.Red();
        }

        OnPolicyApplied();

        return null;
    }
}