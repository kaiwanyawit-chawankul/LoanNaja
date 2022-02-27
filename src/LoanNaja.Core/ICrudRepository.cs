namespace LoanNaja.Core;

public interface ICrudRepository<T>
{
    T Save(T t);
    List<T> findAll();
}
