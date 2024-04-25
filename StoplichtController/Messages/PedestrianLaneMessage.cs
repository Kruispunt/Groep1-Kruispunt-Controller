using Newtonsoft.Json;

namespace StoplichtController.Messages;

[JsonObject]
public class PedestrianLaneMessage : IUpdateMessage
{
    public bool Detected { get; set; }
}