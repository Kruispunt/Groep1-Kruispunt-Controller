namespace StoplichtController.Crossing.Lanes;

public interface ICrossesRoad : ICanIntersect
{
    string CrossesRoad { get; }
}