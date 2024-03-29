namespace StoplichtController.Crossing.Lanes;

public interface ICanIntersect
{
    public bool IntersectsWith(char roadId);
    public bool IntersectsWith(Lane lane);
}