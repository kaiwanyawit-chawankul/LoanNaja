using Xunit;
using LoanNaja;
using Moq;
using LoanNaja.Controllers;
using Microsoft.Extensions.Logging;
using LoanNaja;
using LoanNaja.Core;
using LoanNaja.Core.Test;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using System.Net;

namespace LoanNaja.Test;

public class LoanControllerTest
{
    private LoanController _target;
    private Mock<ILoanRepository> mockLoanRepository;

    private string account = Guid.NewGuid().ToString();
    private Guid loanId = Guid.NewGuid();
    private DateTime takenAt = DateTime.Now;
    private int durationInDays = 10;
    private int amount = 200;

    public LoanControllerTest()
    {
        mockLoanRepository = new Mock<ILoanRepository>(MockBehavior.Strict);
        _target = new LoanNaja.Controllers.LoanController(
            Mock.Of<ILogger<HomeController>>(),
            mockLoanRepository.Object
        );
    }

    [Fact]
    void shouldReturnAllUsersLoans()
    {
        // Arrange

        Loan loan = new LoanBuilder().withAccount(account).build();

        mockLoanRepository
            .Setup(repo => repo.findAllByAccount(account))
            .Returns(new List<Loan>() { loan });

        // Act
        var response = _target.getAll(account);

        // Assert
        response.Result
            .Should()
            .BeOfType<OkObjectResult>()
            .Which.StatusCode.Should()
            .Be((int)HttpStatusCode.OK);
        response.Result
            .Should()
            .BeOfType<OkObjectResult>()
            .Which.Value.As<List<Loan>>()
            .Should()
            .Contain(loan);
    }

    [Fact]
    void shouldReturnUserLoanById()
    {
        Loan loan = new Loan(loanId, account, amount, takenAt, durationInDays);

        mockLoanRepository.Setup(repo => repo.findByIdAndAccount(loan.Id, account)).Returns(loan);

        var response = _target.getLoan(account, loan.Id);

        // Assert
        response.Result
            .Should()
            .BeOfType<OkObjectResult>()
            .Which.StatusCode.Should()
            .Be((int)HttpStatusCode.OK);
        response.Result
            .Should()
            .BeOfType<OkObjectResult>()
            .Which.Value.As<Loan>()
            .Should()
            .Be(loan);
    }

    [Fact]
    void shouldResponseNotFoundWhenUsersLoanCanNotBeFound()
    {
        mockLoanRepository
            .Setup(repo => repo.findByIdAndAccount(loanId, account))
            .Returns((Loan)null);

        var response = _target.getLoan(account, loanId);

        // Assert
        response.Result
            .Should()
            .BeOfType<NotFoundResult>()
            .Which.StatusCode.Should()
            .Be((int)HttpStatusCode.NotFound);
    }

    [Fact]
    void shouldRequestForANewLoan()
    {
        Loan loan = new Loan(account, amount, takenAt, durationInDays);
        Loan persisted = new Loan(Guid.NewGuid(), account, amount, takenAt, durationInDays);
        mockLoanRepository.Setup(repo => repo.Save(loan)).Verifiable();
        //TODO: remove any
        mockLoanRepository
            .As<ICrudRepository<Loan>>()
            .Setup(repo => repo.Save(It.IsAny<Loan>()))
            .Returns(persisted)
            .Verifiable();

        var loanRequest = new NewLoanCommand(amount: 200, durationInDays: 10);

        var response = _target.createNew(account, loanRequest);

        response.Result
            .Should()
            .BeOfType<AcceptedResult>()
            .Which.StatusCode.Should()
            .Be((int)HttpStatusCode.Accepted);
        response.Result
            .Should()
            .BeOfType<AcceptedResult>()
            .Which.Value.As<LoanStatus>()
            .Location.Url.Should()
            .Be($"/api/accounts/{account}/loans/{persisted.Id}");
        response.Result
            .Should()
            .BeOfType<AcceptedResult>()
            .Which.Value.As<LoanStatus>()
            .Status.Should()
            .Be("ok");
    }

    [Theory]
    [InlineData(-10, 10)]
    [InlineData(10, -10)]
    void shouldNotBeAbleToRequestForANewLoanWithoutAccount(int amount, int durationInDays)
    {
        var loanRequest = new NewLoanCommand(amount: amount, durationInDays: durationInDays);

        var response = _target.createNew(account, loanRequest);

        response.Result
            .Should()
            .BeOfType<BadRequestResult>()
            .Which.StatusCode.Should()
            .Be((int)HttpStatusCode.BadRequest);
    }
}
