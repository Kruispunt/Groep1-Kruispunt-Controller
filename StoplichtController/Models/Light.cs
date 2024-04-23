namespace StoplichtController.Models;

[Serializable]
public class Light
{
    public LightState State { get; private set; }
    
    public Light()
    {
        State = LightState.Red;
    }
    
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
    
    public bool IsGreen()
    {
        if (State == LightState.Green)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public enum LightState
{
    Red = 0,
    Orange = 1,
    Green = 2
}