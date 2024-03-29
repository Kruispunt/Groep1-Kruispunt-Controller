namespace StoplichtController.Crossing.Lanes.Implementations;

public class BikeLane : Lane, ICrossesRoad
{
    public BikeLane(int id, char crossesRoadId) : base(id)
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