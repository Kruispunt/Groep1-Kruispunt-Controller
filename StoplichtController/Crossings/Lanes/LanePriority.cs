using StoplichtController.Crossings.Lanes.Implementations;

namespace StoplichtController.Crossings.Lanes;

public class LanePriority(Lane lane) : IComparable<LanePriority>
{
    DateTime TimeAdded { get; set; } = DateTime.Now;
    TimeSpan TimeInQueue => DateTime.Now - TimeAdded; 

    bool HasPriorityVehicle
    {
        get
        {
            if (lane is CarLane carLane)
            {
                return carLane.PrioCar;
            }

            return false;
        }
    }

    bool HasManyWaitingVehicles
    {
        get
        {
            if (lane is CarLane carLane)
            {
                return carLane is { DetectNear: true, DetectFar: true };
            }

            return false;
        }
    }

    public int CompareTo(LanePriority? other)
    {
        if (other is null) return 1;

        return HasPriorityVehicle switch
        {
            // Prioritize lanes with a priority vehicle
            true when !other.HasPriorityVehicle => -1,
            false when other.HasPriorityVehicle => 1,
            _ => HasManyWaitingVehicles switch
            {
                // Prioritize lanes with more waiting vehicles
                true when !other.HasManyWaitingVehicles => -1,
                false when other.HasManyWaitingVehicles => 1,
                // Prioritize lanes with longer wait times
                _ => TimeInQueue.CompareTo(other.TimeInQueue)
            }
        };
    }
}