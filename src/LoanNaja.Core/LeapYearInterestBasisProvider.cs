using System.Globalization;

namespace LoanNaja.Core;

public class LeapYearInterestBasisProvider : IInterestBasisProvider
{
    //private final Clock clock;

    public LeapYearInterestBasisProvider()
    {
        //this.clock = clock;
    }

    public int getCurrentInterestBasis(DateTime now)
    {
        //return LocalDate.now(clock).lengthOfYear();
        var calendar = new GregorianCalendar();
        return calendar.GetDaysInYear(now.Year);
    }
}
