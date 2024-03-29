namespace StoplichtController.Crossing;

public class Crossing(int id)
{
    private int Id { get; set; }
    public List<Road> Roads { get; set; }
}