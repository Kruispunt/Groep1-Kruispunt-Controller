namespace StoplichtController.Crossings.Lanes;

public abstract class LaneWithPath(string from, string to) : Lane, IHasPath
{
    public Path Path { get; } = new(from, to);

    public bool IntersectsWith(Lane lane)
    {
        return lane switch
        {
            IHasPath pathLane => Path.From != pathLane.Path.From &&
                                 Path.From == pathLane.Path.To &&
                                 Path.To == pathLane.Path.From,
            ICrossesRoad crossesRoadLane => Path.From == crossesRoadLane.CrossesRoad ||
                                            Path.To == crossesRoadLane.CrossesRoad,
            _ => false
        };
    }
}