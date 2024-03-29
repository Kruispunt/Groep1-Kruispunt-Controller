namespace StoplichtController.Crossing.Lanes;

public interface ICrossesRoad : ICanIntersect
{
    char CrossesRoad { get; }
}