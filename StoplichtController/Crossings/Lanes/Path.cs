namespace StoplichtController.Crossings.Lanes;

public class Path
{


    public Path(string from, string to)
    {
        if (from == to)
        {
            throw new ArgumentException("Roads 'from' and 'to' cannot be the same");
        }

        From = from;
        To = to;
    }
    public string From { get; set; }
    public string To { get; set; }
}