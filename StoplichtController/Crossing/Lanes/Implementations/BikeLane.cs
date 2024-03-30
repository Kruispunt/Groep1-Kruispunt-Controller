namespace StoplichtController.Crossing.Lanes.Implementations;

public class BikeLane : Lane, ICrossesRoad
{
    public BikeLane(char crossesRoadId) : base()
    {
    }

    public char CrossesRoad { get; }
    public bool IntersectsWith(char roadId)
    {
        throw new NotImplementedException();
    }

    public bool IntersectsWith(Lane lane)
    {
        throw new NotImplementedException();
    }
}