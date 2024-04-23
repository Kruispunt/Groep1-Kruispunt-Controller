using StoplichtController.Crossings;

namespace StoplichtController.Policies;

public class EmergencyVehiclePolicy : Policy
{
    override public IPolicy? Apply(Crossing crossing)
    {
        if (!crossing.HasPriorityVehicle())
            return base.Apply(crossing);
        
        crossing.LetPriorityVehiclePass();
        OnPolicyApplied();
        return null;
    }
}