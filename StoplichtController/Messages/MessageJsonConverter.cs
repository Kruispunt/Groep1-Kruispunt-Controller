using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StoplichtController.Messages;

public class MessageJsonConverter : JsonConverter<Message>
{
    private MessageFactory _factory;

    public MessageJsonConverter(MessageFactory factory)
    {
        _factory = factory;
    }

    public override Message ReadJson(JsonReader reader, Type objectType, Message existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject jObject = JObject.Load(reader);
        string type = (string)jObject["type"];

        Message message = _factory.Create(type);
        serializer.Populate(jObject.CreateReader(), message);

        return message;
    }

    public override void WriteJson(JsonWriter writer, Message value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override bool CanWrite => false;
}