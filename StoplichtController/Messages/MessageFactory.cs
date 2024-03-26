namespace StoplichtController.Messages;

public class MessageFactory
{
    private Dictionary<string, Func<Message>> _creators = new Dictionary<string, Func<Message>>();

    public void Register(string type, Func<Message> creator)
    {
        _creators[type] = creator;
    }

    public Message Create(string type)
    {
        if (_creators.TryGetValue(type, out var creator))
        {
            return creator();
        }

        throw new Exception($"Unknown message type: {type}");
    }
}