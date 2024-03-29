using StoplichtController.Models;

namespace StoplichtController.Crossing.Lanes;

public abstract class Lane : ICanIntersect
{
    int Id { get; set; }
    Light Light { get; set; }
    
    public Lane(int id)
    {
        Id = id;
        Light = new Light();
        if (!(this is ICrossesRoad) || !(this is IHasPath))
        {
            throw new InvalidOperationException("Lane must implement either ICrossesRoad or IHasPath interface");
        }
    }

    
    public bool IntersectsWith(char roadId)
    {
        if (roadId)
        {
            //todo
        }
        return 
    }
    public  bool IntersectsWith(Lane lane)
    {
        //todo
    }
}