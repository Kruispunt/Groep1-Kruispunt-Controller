namespace StoplichtController.Crossing.Lanes;

public interface ICanIntersect
{
    public bool IntersectsWith(string roadId);
    public bool IntersectsWith(Lane lane);
}