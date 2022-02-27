using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;

namespace LoanNaja.Core;

public class Loan
{
    //   @Id
    //   @GeneratedValue(strategy = GenerationType.IDENTITY)
    // @JsonProperty
    public Guid Id { get; internal set; }

    public string Account { get; internal set; }
    public int Amount { get; internal set; }
    public DateTime? TakenAt { get; internal set; }
    public int DurationInDays { get; internal set; }
    public int InterestRate { get; internal set; }
    public int InterestBasis { get; internal set; }

    public Loan() { }

    public Loan(Guid id, string account, int amount, DateTime? takenAt, int durationInDays)
        : this(account, amount, takenAt, durationInDays)
    {
        this.Id = id;
    }

    public Loan(String account, int amount, DateTime? takenAt, int durationInDays)
    {
        validateLoan(account, amount, takenAt, durationInDays);
        this.Account = account;
        this.Amount = amount;
        this.TakenAt = takenAt;
        this.DurationInDays = durationInDays;
        this.InterestRate = interestRateFromDuration(durationInDays);
        this.InterestBasis = 365;
    }

    public override bool Equals(object? obj)
    {
        if (this == obj)
            return true;
        if (obj == null || GetType() != obj.GetType())
            return false;
        Loan loan = (Loan)obj;
        return Amount == loan.Amount
            && DurationInDays == loan.DurationInDays
            && InterestRate == loan.InterestRate
            && InterestBasis == loan.InterestBasis
            && Object.Equals(Id, loan.Id)
            && Object.Equals(Account, loan.Account)
            && Object.Equals(TakenAt, loan.TakenAt);
    }

    public override int GetHashCode()
    {
        return (
            Id,
            Account,
            Amount,
            TakenAt,
            DurationInDays,
            InterestRate,
            InterestBasis
        ).GetHashCode();
    }

    public override string ToString()
    {
        return "Loan{"
            + "id="
            + Id
            + ", account='"
            + Account
            + '\''
            + ", amount="
            + Amount
            + ", takenAt="
            + TakenAt
            + ", durationInDays="
            + DurationInDays
            + ", interestRate="
            + InterestRate
            + ", interestBasis="
            + InterestBasis
            + '}';
    }

    private int interestRateFromDuration(int durationInDays)
    {
        if (durationInDays < 30)
            return 20;
        if (durationInDays <= 180)
            return 15;
        return 10;
    }

    private void validateLoan(String account, int amount, DateTime? takenAt, int durationInDays)
    {
        Guard.Against.Null(account, "account can not be null");
        Guard.Against.Null(takenAt, "takenAt date can not be null");
        if (amount < 0 || durationInDays < 0)
        {
            throw new ValidationException("amount or duration or interest rate cannot be negative");
        }
    }
}
