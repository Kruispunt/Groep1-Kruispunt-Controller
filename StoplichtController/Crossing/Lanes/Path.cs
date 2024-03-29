namespace StoplichtController.Crossing.Lanes;

public class Path
{
    private char From { get; set; }
    private char To { get; set; }

    public Path(char from, char to)
    {
        if (from == to)
        {
            throw new ArgumentException("Roads 'from' and 'to' cannot be the same");
        }

        From = from;
        To = to;
    }
}