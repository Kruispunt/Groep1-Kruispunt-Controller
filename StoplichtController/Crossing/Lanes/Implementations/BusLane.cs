namespace StoplichtController.Crossing.Lanes.Implementations;

public class BusLane : Lane, IHasPath
{
    int BusNumber { get; set; }

    public BusLane(int busNumber) : base()
    {
        BusNumber = busNumber;
    }

    public Path Path { get; }
    public bool IntersectsWith(char roadId)
    {
        throw new NotImplementedException();
    }

    public bool IntersectsWith(Lane lane)
    {
        throw new NotImplementedException();
    }
}