namespace StoplichtController.Crossing.Lanes.Implementations;

public class PedestrianLane : Lane, ICrossesRoad
{
    public PedestrianLane(int id) : base(id)
    {
    }

    public bool IntersectsWith(char roadId)
    {
        throw new NotImplementedException();
    }

    public bool IntersectsWith(Lane lane)
    {
        throw new NotImplementedException();
    }

    public char CrossesRoad { get; }
}