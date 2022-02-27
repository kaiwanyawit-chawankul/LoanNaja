using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LoanNaja.Models;
using LoanNaja.Core;
using System.ComponentModel.DataAnnotations;

namespace LoanNaja.Controllers;

[ApiController]
public class LoanController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;
    private ILoanRepository loanRepository;

    public LoanController(ILogger<HomeController> logger, ILoanRepository loanRepository)
    {
        _logger = logger;
        this.loanRepository = loanRepository;
    }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }

    [HttpPost]
    [Route("api/accounts/{accountId}/loans")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<LoanStatus> createNew(
        [FromRoute] string accountId,
        [FromBody] NewLoanCommand newLoanCommand
    )
    {
        try
        {
            var loan = new Loan(
                accountId,
                newLoanCommand.Amount,
                DateTime.Now,
                newLoanCommand.DurationInDays
            );
            var saved = loanRepository.Save(loan);
            var status = new LoanStatus(
                "ok",
                string.Format("/api/accounts/{0}/loans/{1}", accountId, saved.Id)
            );
            return Accepted(status);
        }
        catch (ValidationException e)
        {
            //return BadRequest(Map.of("status", "not ok", "msg", e.getMessage()));
            return BadRequest();
        }
    }

    [HttpGet]
    [Route("api/accounts/{accountId}/loans")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<List<Loan>> getAll([FromRoute] string accountId)
    {
        var loans = loanRepository.findAllByAccount(accountId);
        return Ok(loans);
    }

    [HttpGet]
    [Route("api/accounts/{accountId}/loans/{loanId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Loan> getLoan([FromRoute] string accountId, Guid loanId)
    {
        var optionalLoan = loanRepository.findByIdAndAccount(loanId, accountId);
        if (optionalLoan == null)
            return NotFound();
        return Ok(optionalLoan);
    }
}
