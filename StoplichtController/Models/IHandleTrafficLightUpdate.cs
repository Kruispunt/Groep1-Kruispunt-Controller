namespace StoplichtController.Messages;

public interface IHandleTrafficLightUpdate
{
    void HandleUpdate(Message message);
}