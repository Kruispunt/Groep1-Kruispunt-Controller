using StoplichtController.Crossings;
using StoplichtController.Crossings.Lanes;
using StoplichtController.Crossings.Lanes.Implementations;

namespace StoplichtController.Policies;

public class PriorityPolicy : Policy
{
    override public async Task<IPolicy?> Apply(Crossing crossing)
    {
        if (crossing.WaitList.Count <= 0) return await base.Apply(crossing);

        // Turn the light green for the lane with the highest priority and red for all other lanes
        var highestPriorityLane = crossing.WaitList.First().Value;

        var goToGreenList = new List<Lane> { highestPriorityLane };
        var waitList = crossing.WaitList.Values;

        foreach (var lane in waitList)
            if (goToGreenList.All(greenLane => !greenLane.IntersectsWith(lane)))
                goToGreenList.Add(lane);

        var greenTime = TimeSpan.FromSeconds(4);
        var orangeTime = TimeSpan.FromSeconds(2);
        var evacuationTime = TimeSpan.FromSeconds(5);

        foreach (var lane in goToGreenList)
        {
            if (lane is BikeLane or PedestrianLane)
                evacuationTime = TimeSpan.FromSeconds(6);
            lane.Light.Green();
        }

        await Task.Delay(greenTime);
        foreach (var lane in goToGreenList)
            lane.Light.Orange();
        await Task.Delay(orangeTime);
        foreach (var lane in goToGreenList)
        {
            lane.Light.Red();
            crossing.WaitList.Remove(lane.GetPriority());
        }
        await Task.Delay(evacuationTime);

        OnPolicyApplied();

        return null;
    }
}