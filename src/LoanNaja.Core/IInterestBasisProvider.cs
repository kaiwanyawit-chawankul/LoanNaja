namespace LoanNaja.Core;

public interface IInterestBasisProvider
{
    int getCurrentInterestBasis(DateTime now);
}
