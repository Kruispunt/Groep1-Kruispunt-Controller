using StoplichtController.Messages;

namespace StoplichtController.Updates;

public class HandlePedestrianUpdate : IHandleTrafficLightUpdate
{
    public void HandleUpdate(Message message)
    {
        Console.WriteLine($"This is a Pedestrian Message: {message}");
    }
}