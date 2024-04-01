using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StoplichtController.Messages;

namespace StoplichtController.TcpServer;

public class MessageJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return (objectType == typeof(CrossingMessage));
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject jObject = JObject.Load(reader);

        CrossingMessage message = new CrossingMessage();
        message.Crossing.Add(jObject.Next.ToString());
     
        
        return message;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        // Customize the serialization process here
    }
}