namespace LoanNaja.Core;

public class LoanRepository : ILoanRepository
{
    LoanContext _context;

    public LoanRepository(LoanContext context)
    {
        _context = context;
    }

    List<Loan> items = new List<Loan>();

    public List<Loan> findAll()
    {
        return _context.Loans.ToList();
    }

    public List<Loan> findAllByAccount(string accountId)
    {
        return _context.Loans.Where(loan => loan.Account == accountId).ToList();
    }

    public Loan findByIdAndAccount(Guid id, string accountId)
    {
        return _context.Loans.FirstOrDefault(loan => loan.Id == id && loan.Account == accountId);
    }

    public Loan Save(Loan t)
    {
        _context.Loans.Add(t);
        _context.SaveChanges();
        return t;
    }
}
