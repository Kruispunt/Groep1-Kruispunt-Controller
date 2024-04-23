namespace StoplichtController.Crossings.Lanes;

public interface ICanIntersect
{
    public bool IntersectsWith(string roadId);
    public bool IntersectsWith(Lane lane);
}