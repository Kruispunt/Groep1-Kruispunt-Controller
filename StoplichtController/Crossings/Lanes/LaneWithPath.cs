namespace StoplichtController.Crossings.Lanes;

public abstract class LaneWithPath(string from, string to) : Lane, IHasPath
{
    public Path Path { get; } = new(from, to);

    override public bool IntersectsWith(Lane lane)
    {
        switch (lane)
        {
            case IHasPath other:
                if (Path.From == other.Path.From)
                    return false;

                return Path.From != other.Path.To || Path.To != other.Path.From;


            case ICrossesRoad crossesRoadLane:
                return Path.From == crossesRoadLane.CrossesRoad ||
                       Path.To == crossesRoadLane.CrossesRoad;
            default: return true;
        }
    }
}