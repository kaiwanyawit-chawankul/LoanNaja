using Xunit;

namespace LoanNaja.Core.Test;
using System;
using System.ComponentModel.DataAnnotations;

public class LoanTest
{
    [Fact]
    public void Test1() { }

    [Fact]
    public void shouldNotBeAbleToCreateLoanWithNegativeAmount()
    {
        Assert.Throws<ValidationException>(() => new LoanBuilder().withAmount(-1).build());
    }

    [Fact]
    public void shouldNotBeAbleToCreateLoanWithNegativeDuration()
    {
        Assert.Throws<ValidationException>(() => new LoanBuilder().withDurationInDays(-1).build());
    }

    [Fact]
    public void shouldNotBeAbleToCreateLoanWithoutAccountNumber()
    {
        Assert.Throws<ArgumentNullException>(() => new LoanBuilder().withAccount(null).build());
    }

    [Fact]
    public void shouldNotBeAbleToCreateLoanWithoutTakenAt()
    {
        Assert.Throws<ArgumentNullException>(() => new LoanBuilder().withTakenOn(null).build());
    }
}
