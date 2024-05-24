using StoplichtController.Crossings.Lanes.Implementations;

namespace StoplichtController.Crossings.Lanes;

public class LanePriority : IComparable<LanePriority>
{
    readonly Lane _lane;
    readonly DateTime _timeAdded;

    public LanePriority(Lane lane)
    {
        _lane = lane;
        _timeAdded = DateTime.Now;
    }
    TimeSpan TimeInQueue => DateTime.Now - _timeAdded;

    bool HasPriorityVehicle
    {
        get
        {
            if (_lane is CarLane carLane)
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
            if (_lane is CarLane carLane)
            {
                return carLane is { DetectNear: true, DetectFar: true };
            }

            return false;
        }
    }

    public int CompareTo(LanePriority? other)
    {
        const int thisLaneHasPriority = -1;
        const int otherLaneHasPriority = 1;
        const int noPriorityDifference = 0;
        var score = 0;

        if (other is null) return thisLaneHasPriority;

        // early return if lane has priorityVehicle
        switch (HasPriorityVehicle)
        {
            case true when other.HasPriorityVehicle:
                return noPriorityDifference;
            case true when !other.HasPriorityVehicle:
                return thisLaneHasPriority;
            case false when other.HasPriorityVehicle:
                return otherLaneHasPriority;
        }

        // Give bus lane higher priority
        switch (_lane is BusLane)
        {
            case true when other._lane is BusLane is false:
                score += thisLaneHasPriority;

                break;
            case false when other._lane is BusLane:
                score += otherLaneHasPriority;

                break;
        }

        switch (TimeInQueue > TimeSpan.FromMinutes(2))
        {
            case true when other.TimeInQueue > TimeSpan.FromMinutes(2):
                return noPriorityDifference;
            case true when other.TimeInQueue <= TimeSpan.FromMinutes(2):
                return thisLaneHasPriority;
            case false when other.TimeInQueue > TimeSpan.FromMinutes(2):
                return otherLaneHasPriority;
        }

        switch (TimeInQueue > TimeSpan.FromMinutes(1))
        {
            case true when other.TimeInQueue > TimeSpan.FromMinutes(1):
                score += noPriorityDifference;

                break;
            case true when other.TimeInQueue <= TimeSpan.FromMinutes(1):
                score += thisLaneHasPriority;

                break;
            case false when other.TimeInQueue > TimeSpan.FromMinutes(1):
                score += otherLaneHasPriority;

                break;
        }

        switch (HasManyWaitingVehicles)
        {
            case true when other.HasManyWaitingVehicles:
                score += noPriorityDifference;

                break;
            case true when !other.HasManyWaitingVehicles:
                score += thisLaneHasPriority;

                break;
            case false when other.HasManyWaitingVehicles:
                score += otherLaneHasPriority;

                break;
        }

        score += TimeInQueue.CompareTo(other.TimeInQueue);

        return score;
    }


    public static bool operator <=(LanePriority left, LanePriority right) =>
        left.CompareTo(right) <= 0;

    public static bool operator >=(LanePriority left, LanePriority right) =>
        left.CompareTo(right) >= 0;
}