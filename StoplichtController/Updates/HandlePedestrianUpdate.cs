using StoplichtController.Messages;

namespace StoplichtController.Updates;

public class HandlePedestrianUpdate : IHandleTrafficLightUpdate
{
    public void HandleUpdate(CrossingMessage simToControllerMessage)
    {
        Console.WriteLine($"This is a Pedestrian Message: {simToControllerMessage}");
    }
}