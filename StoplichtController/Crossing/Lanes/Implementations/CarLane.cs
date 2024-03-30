namespace StoplichtController.Crossing.Lanes.Implementations;

public class CarLane : Lane, IHasPath
{
    public bool DetectNear { get; set; }
    public bool DetectFar { get; set; }
    public bool PrioCar { get; set; }
    public Path Path { get; }

    public CarLane(char from, char to) : base()
    {
        Path = new Path(from, to);
    }


    public bool IntersectsWith(char roadId)
    {
        throw new NotImplementedException();
    }

    public bool IntersectsWith(Lane lane)
    {
        throw new NotImplementedException();
    }
}