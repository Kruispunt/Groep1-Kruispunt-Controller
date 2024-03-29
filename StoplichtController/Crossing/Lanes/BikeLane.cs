namespace StoplichtController.Crossing.Lanes;

public class BikeLane : Lane, ICrossesRoad
{
    public BikeLane(int id, char crossesRoadId) : base(id)
    {
    }

    public override bool IntersectsWith(char roadId)
    {
        throw new NotImplementedException();
    }

    public override bool IntersectsWith(Lane lane)
    {
        throw new NotImplementedException();
    }

    public char CrossesRoad { get; }
}