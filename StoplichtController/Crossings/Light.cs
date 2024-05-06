namespace StoplichtController.Crossings;

[Serializable]
public class Light
{
    public LightState State { get; private set; } = LightState.Red;

    public void Green()
    {
        State = LightState.Green;
    }
    
    public void Orange()
    {
        State = LightState.Orange;
    }
    
    public void Red()
    {
        State = LightState.Red;
    }
    
    public bool IsGreen() { return State == LightState.Green; }
    
    public int GetState() { return (int) State; }
}

public enum LightState
{
    Red = 0,
    Orange = 1,
    Green = 2
}