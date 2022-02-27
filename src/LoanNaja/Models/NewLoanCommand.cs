namespace LoanNaja.Core;

public class NewLoanCommand
{
    // @Min(0)
    //   @NotNull
    public int Amount { get; set; }

    // @Min(0)
    // @NotNull
    public int DurationInDays { get; set; }

    public NewLoanCommand() { }

    public NewLoanCommand(int amount, int durationInDays)
    {
        this.Amount = amount;
        this.DurationInDays = durationInDays;
    }

    public override bool Equals(object? obj)
    {
        if (this == obj)
            return true;
        if (obj == null || GetType() != obj.GetType())
            return false;
        NewLoanCommand newLoanCommand = (NewLoanCommand)obj;
        return Amount == newLoanCommand.Amount && DurationInDays == newLoanCommand.DurationInDays;
    }

    public override int GetHashCode()
    {
        return (Amount, DurationInDays).GetHashCode();
    }
}
