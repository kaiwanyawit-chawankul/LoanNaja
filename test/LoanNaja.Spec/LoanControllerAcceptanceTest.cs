using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Newtonsoft.Json;

namespace LoanNaja.Spec;

public class LoanControllerAcceptanceTest
{
    private string accountId = Guid.NewGuid().ToString();

    [Fact, Trait("Category", "Spec")]
    public async Task shouldRequestNewLoanAndSeeAmountAndInterestRateAsync()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("http://docker.local:8080/");
        client.DefaultRequestHeaders.Add("Accept", "text/plain");
        var loanRequest = "{\"amount\": 200, \"durationInDays\": 365}";
        var content = new StringContent(loanRequest, Encoding.UTF8, "application/json");
        var response = await client.PostAsync($"/api/accounts/{accountId}/loans/", content);
        response.Should().Be202Accepted();
        dynamic obj = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
        string url = obj.location.url;

        //Act
        var loanResponse = await client.GetAsync(url);
        loanResponse
            .Should()
            .Satisfy(
                givenModelStructure: new
                {
                    amount = default(double),
                    interestRate = default(double),
                    interestBasis = default(double),
                    durationInDays = default(double),
                    totalOutstanding = default(double)
                },
                assertion: model =>
                {
                    model.amount.Should().Be(200);
                    model.interestRate.Should().Be(10);
                    model.interestBasis.Should().Be(365);
                    model.durationInDays.Should().Be(365);
                    model.totalOutstanding.Should().Be(220.0F);
                }
            );
    }
}
