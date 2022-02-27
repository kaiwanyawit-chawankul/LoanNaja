using System;
using FluentAssertions;
using Xunit;

namespace LoanNaja.Core.Test;

public class LeapYearInterestBasisProviderTest
{
    [Fact]
    public void Test1() { }

    // public static final ZoneId UTC = ZoneId.of("UTC");

    [Fact]
    public void shouldReturnInterestBasisForLeapYear()
    {
        var clock = DateTime.Parse("2000-12-03T10:15:30.00Z");
        var basisProvider = new LeapYearInterestBasisProvider();

        var currentInterestBasis = basisProvider.getCurrentInterestBasis(clock);

        currentInterestBasis.Should().Be(366);
    }

    [Fact]
    public void shouldReturnInterestBasisForNonLeapYear()
    {
        var clock = DateTime.Parse("2001-12-03T10:15:30.00Z");
        var basisProvider = new LeapYearInterestBasisProvider();

        var currentInterestBasis = basisProvider.getCurrentInterestBasis(clock);

        currentInterestBasis.Should().Be(365);
    }
}
