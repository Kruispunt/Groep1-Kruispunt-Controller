using Newtonsoft.Json;

namespace StoplichtController.Messages;

public class PedestrianMessage : Message
{
    [JsonProperty("HasPedestrianWaiting")]
    public bool HasPedestrianWaiting { get; set; }
}