using Newtonsoft.Json;

namespace StoplichtController.Messages;

[JsonObject]
public class CarLaneMessage : IUpdateMessage
{
    public bool DetectNear { get; set; }
    public bool DetectFar { get; set; }
    public bool PrioCar { get; set; }
}