using StoplichtController.Messages;

namespace StoplichtController.Crossing.Lanes;

public interface IUpdateableLane
{
    public bool Update(ILaneMessage message);
}