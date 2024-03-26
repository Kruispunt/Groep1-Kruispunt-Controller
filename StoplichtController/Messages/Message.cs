using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StoplichtController.Messages;

[Serializable]
public abstract class Message
{
    [JsonProperty("type")]
    private string Type { get; set; }

}