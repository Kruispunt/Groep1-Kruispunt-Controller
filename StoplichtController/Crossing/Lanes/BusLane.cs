namespace StoplichtController.Crossing.Lanes;

public class BusLane : Lane, IHasPath
{
    int BusNumber { get; set; }

    public BusLane(int id) : base(id)
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

    public Path Path { get; }
}