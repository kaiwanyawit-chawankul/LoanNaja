using FluentAssertions;
using Xunit;

namespace LoanNaja.Core.Test;
using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

public class LoanRepositoryTest
{
    private LoanRepository loanRepository;
    private String account = Guid.NewGuid().ToString();
    private String otherAccount = Guid.NewGuid().ToString();

    public LoanRepositoryTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<LoanContext>();
        optionsBuilder.UseSqlite($"Data Source=loan.db");
        var context = new LoanContext(optionsBuilder.Options);

        loanRepository = new LoanRepository(context);
    }

    [Fact]
    void shouldPersistNewLoan()
    {
        Loan newLoan = new LoanBuilder().build();
        Loan saved = loanRepository.Save(newLoan);

        List<Loan> loans = loanRepository.findAll();

        loans.Should().Contain(saved);
    }

    [Fact]
    void shouldReturnEmptyListWhenThereIsNoLoanForThisAccount()
    {
        List<Loan> loans = loanRepository.findAllByAccount(account);

        loans.Should().BeEmpty();
    }

    [Fact]
    void shouldFindAllLoansAssignedToAccount()
    {
        Loan newLoan = new LoanBuilder().withAccount(account).build();
        Loan anotherLoan = new LoanBuilder().withAccount(account).build();
        Loan someOtherLoan = new LoanBuilder().withAccount(otherAccount).build();
        Loan newLoanSaved = loanRepository.Save(newLoan);
        Loan anotherLoanSaved = loanRepository.Save(anotherLoan);
        loanRepository.Save(someOtherLoan);

        List<Loan> loans = loanRepository.findAllByAccount(account);

        loans.Should().BeEquivalentTo(new List<Loan>() { newLoanSaved, anotherLoanSaved });
    }

    [Fact]
    void shouldNotFindLoanWhenIdIsNotAssignedToAccount()
    {
        Loan newLoan = new LoanBuilder().withAccount(account).build();
        Loan anotherLoan = new LoanBuilder().withAccount(otherAccount).build();
        loanRepository.Save(newLoan);
        Loan anotherLoanSaved = loanRepository.Save(anotherLoan);

        Loan optionalLoan = loanRepository.findByIdAndAccount(anotherLoanSaved.Id, account);

        optionalLoan.Should().BeNull();
    }

    [Fact]
    void shouldFindLoanByIdAndAccount()
    {
        Loan newLoan = new LoanBuilder().withAccount(account).build();
        Loan anotherLoan = new LoanBuilder().withAccount(account).build();
        Loan newLoanSaved = loanRepository.Save(newLoan);
        loanRepository.Save(anotherLoan);

        Loan optionalLoan = loanRepository.findByIdAndAccount(newLoanSaved.Id, account);

        optionalLoan.Should().Be(newLoanSaved);
    }
}
