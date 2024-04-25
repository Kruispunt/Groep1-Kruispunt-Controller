using Newtonsoft.Json;

namespace StoplichtController.Messages;

[JsonObject]
public class BikeLaneMessage: IUpdateMessage
{
    public bool Detected { get; set; }
}