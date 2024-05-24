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

        return (TimeInQueue.TotalSeconds, HasPriorityVehicle,
            HasManyWaitingVehicles) switch
            {
                (_, true, _) when !other.HasPriorityVehicle => -1,
                (_, false, _) when other.HasPriorityVehicle => 1,
                (_, _, true) when !other.HasManyWaitingVehicles => -1,
                (_, _, false) when other.HasManyWaitingVehicles => 1,
                (> 60, _, _) => -1,
                (<= 60, _, _) when other.TimeInQueue.TotalSeconds > 60 => 1,
                _ => TimeInQueue.CompareTo(other.TimeInQueue)
            };
    }


    public static bool operator <=(LanePriority left, LanePriority right) =>
        left.CompareTo(right) <= 0;

    public static bool operator >=(LanePriority left, LanePriority right) =>
        left.CompareTo(right) >= 0;
}