using StoplichtController.Messages;

namespace StoplichtController.Crossings.Lanes.Implementations;

public class BusLane : Lane, IHasPath
{
    List<int> BusNumbers { get; set; } = [];
    public Path Path { get; set; }
    
    public bool IntersectsWith(string roadId) { throw new NotImplementedException(); }

    public bool IntersectsWith(Lane lane) { throw new NotImplementedException(); }

    public void Update(List<int> message)
    {
        foreach (var busLine in message)
        {
            BusNumbers.Add(busLine);
        }
    }
    
    protected override bool UpdateImplementation(IUpdateMessage message)
    {
        if (message is not BusLaneMessage updateMessage) return false;
        
        BusNumbers.Sort();
        updateMessage.Sort();
        if (BusNumbers.SequenceEqual(updateMessage)) return false;
        
        Update(updateMessage);
        return true;
   }
    override public bool ShouldAddToWaitList() => BusNumbers.Count > 0;

}