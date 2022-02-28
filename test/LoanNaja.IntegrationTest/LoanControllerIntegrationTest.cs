using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Xunit;
using FluentAssertions;
using FluentAssertions.Web;
using System.Net;
using System;
using System.Text;
using System.Collections.Generic;
using LoanNaja.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LoanNaja.IntegrationTest;

public class LoanControllerIntegrationTest
{
    private string accountId = Guid.NewGuid().ToString();
    private DateTime takenAt = DateTime.Now;
    private int durationInDays = 10;
    private int amount = 200;

    [Fact]
    public async Task shouldReturnSuccessfulResultForRequestedLoanAsync()
    {
        //Arrange
        await using var application = new TestApiApplication();
        var client = application.CreateClient();

        var loanRequest = "{\"amount\": 200, \"durationInDays\": 10}";
        var content = new StringContent(loanRequest, Encoding.UTF8, "application/json");

        //Act
        var responseMessage = await client.PostAsync($"/api/accounts/{accountId}/loans/", content);

        //Assert
        responseMessage.StatusCode.Should().Be(HttpStatusCode.Accepted);
        // Assert.IsNotNull(notes);
        // Assert.IsTrue(notes.Count == 0);
    }

    [Fact]
    async Task shouldReturnAllLoansForAnAccountAsync()
    {
        //Arrange
        await using var application = new TestApiApplication();
        var client = application.CreateClient();

        var loanRequest = "{\"amount\": 200, \"durationInDays\": 10}";
        var content = new StringContent(loanRequest, Encoding.UTF8, "application/json");
        await client.PostAsync($"/api/accounts/{accountId}/loans/", content);

        //Act
        var responseMessage = await client.GetAsync($"api/accounts/{accountId}/loans");

        //Assert

        responseMessage
            .Should()
            .Satisfy<List<Loan>>(
                model =>
                {
                    model.Should().HaveCount(1);
                    //model.Should().OnlyHaveUniqueItems(c => c.CommentId);
                }
            );
        // assertThat(response.getBody()).hasOnlyOneElementSatisfying(loan ->
        //         assertThat(loan).isEqualToIgnoringGivenFields(new Loan(account, amount, takenAt, durationInDays), "id"));
    }

    [Fact]
    async Task ShouldFoundSwagger()
    {
        await using var application = new TestApiApplication();

        var client = application.CreateClient();
        var responseMessage = await client.GetAsync($"swagger");
        responseMessage.Should().Be200Ok();
    }

    [Fact]
    async Task shouldReturnLoanByAccountAndLoanIdAsync()
    {
        //Arrange
        await using var application = new TestApiApplication();
        var client = application.CreateClient();

        var loanRequest = "{\"amount\": 200, \"durationInDays\": 10}";
        var content = new StringContent(loanRequest, Encoding.UTF8, "application/json");
        var response = await client.PostAsync($"/api/accounts/{accountId}/loans/", content);

        var url = (await response.Content.ReadAsAsync<LoanStatus>()).Location.Url;

        //Act
        var loanResponse = await client.GetAsync(url);
        loanResponse.Should().Be200Ok();
    }
}

class TestApiApplication : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        //var root = new InMemoryDatabaseRoot();

        builder.ConfigureServices(
            services =>
            {
                services.RemoveAll(typeof(DbContextOptions<LoanContext>));
                services.AddDbContext<LoanContext>(
                    options => options.UseInMemoryDatabase("Testing")
                );
            }
        );

        return base.CreateHost(builder);
    }
}
