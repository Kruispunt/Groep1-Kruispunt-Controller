using StoplichtController.Crossing.Lanes;

namespace StoplichtController.Crossing;

public class Road
{
    public char Id { get; set; }
    public List<Lane> Lanes { get; set; }
    public Direction Direction { get; set; }
}