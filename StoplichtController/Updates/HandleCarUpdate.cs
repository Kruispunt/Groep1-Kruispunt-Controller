using StoplichtController.Messages;

namespace StoplichtController.Updates;

public class HandleCarUpdate: IHandleTrafficLightUpdate
{
    public void HandleUpdate(CrossingMessage simToControllerMessage)
    {
        Console.WriteLine("This is a Car Message");
    }
}