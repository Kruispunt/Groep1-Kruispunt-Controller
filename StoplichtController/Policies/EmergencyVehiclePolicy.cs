using StoplichtController.Crossings;
using StoplichtController.Crossings.Lanes.Implementations;

namespace StoplichtController.Policies;

public class EmergencyVehiclePolicy : Policy
{
    override public async Task<IPolicy?> Apply(Crossing crossing)
    {
        if (!crossing.HasPriorityVehicle())
            return await base.Apply(crossing);

        LetPriorityVehiclePass(crossing);
        OnPolicyApplied();
        return null;
    }

    static void LetPriorityVehiclePass(Crossing crossing)
    {
        foreach (var lane in crossing.Roads.SelectMany(road => road.Value.Lanes))
        {
            if (lane is CarLane { PrioCar: true })
            {
                lane.Light.Green();
                Task.Delay(TimeSpan.FromSeconds(10)).Wait();
            }
            else
            {
                lane.Light.Red();
            }
        }
    }
}