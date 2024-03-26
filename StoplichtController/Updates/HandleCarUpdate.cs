using StoplichtController.Messages;

namespace StoplichtController.Updates;

public class HandleCarUpdate: IHandleTrafficLightUpdate
{
    public void HandleUpdate(Message message)
    {
        Console.WriteLine("This is a Car Message");
    }
}