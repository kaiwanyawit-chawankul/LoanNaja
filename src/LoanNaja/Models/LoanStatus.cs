using Newtonsoft.Json;

namespace LoanNaja.Core;

public class LoanStatus
{
    public string Status { get; internal set; }
    public Location Location { get; internal set; }

    private LoanStatus() { }

    public LoanStatus(String status, String urlLocation)
    {
        this.Status = status;
        this.Location = new Location(urlLocation);
    }
}
