namespace LoanNaja.Core;

public interface ILoanRepository : ICrudRepository<Loan>
{
    List<Loan> findAllByAccount(String accountId);

    Loan findByIdAndAccount(Guid id, String accountId);
}
