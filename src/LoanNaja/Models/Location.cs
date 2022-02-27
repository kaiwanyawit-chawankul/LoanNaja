namespace LoanNaja.Core;

public class Location
{
    public string Url { get; internal set; }

    private Location() { }

    public Location(string url)
    {
        this.Url = url;
    }
}
