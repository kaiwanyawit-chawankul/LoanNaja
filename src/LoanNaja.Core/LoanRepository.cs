namespace LoanNaja.Core;

public class LoanRepository : ILoanRepository
{
    List<Loan> items = new List<Loan>();

    public List<Loan> findAll()
    {
        return items;
    }

    public List<Loan> findAllByAccount(string accountId)
    {
        return items.Where(loan => loan.Account == accountId).ToList();
    }

    public Loan findByIdAndAccount(Guid id, string accountId)
    {
        return items.FirstOrDefault(loan => loan.Id == id && loan.Account == accountId);
    }

    public Loan Save(Loan t)
    {
        if (t.Id == Guid.Empty)
        {
            var loan = new Loan(Guid.NewGuid(), t.Account, t.Amount, t.TakenAt, t.DurationInDays);
            items.Add(loan);
            return loan;
        }
        else
        {
            items.Add(t);
            return t;
        }
    }
}
