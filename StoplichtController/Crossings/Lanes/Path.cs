namespace StoplichtController.Crossings.Lanes;

public class Path
{
    private string From { get; set; }
    private string To { get; set; }
    
    

    public Path(string from, string to)
    {
        if (from == to)
        {
            throw new ArgumentException("Roads 'from' and 'to' cannot be the same");
        }

        From = from;
        To = to;
    }
}