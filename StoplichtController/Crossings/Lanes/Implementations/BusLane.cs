using StoplichtController.Messages;

namespace StoplichtController.Crossings.Lanes.Implementations;

public class BusLane(string from, string to) : LaneWithPath(from, to)
{
    List<int> BusNumbers { get; } = [];

    public void Update(List<int> message)
    {
        if (message.Count == 0)
        {
            BusNumbers.RemoveAll(_ => true);

            return;
        }
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