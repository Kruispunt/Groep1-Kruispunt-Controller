namespace StoplichtController.Messages;

public interface IHandleTrafficLightUpdate
{
    void HandleUpdate(CrossingMessage simToControllerMessage);
}