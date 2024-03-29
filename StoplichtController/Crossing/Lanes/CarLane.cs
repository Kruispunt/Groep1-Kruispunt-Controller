namespace StoplichtController.Crossing.Lanes;

public class CarLane : Lane, IHasPath
{
    public bool DetectNear { get; set; }
    public bool DetectFar { get; set; }
    public bool PrioCar { get; set; }

    public CarLane(int id, char from, char to) : base(id)
    {
        Path = new Path(from, to);
    }

    public Path Path { get; }
}