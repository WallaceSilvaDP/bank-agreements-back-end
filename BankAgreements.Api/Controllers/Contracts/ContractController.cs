using BankAgreements.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BankAgreements.Api.Controllers.Contracts;

[ApiController]
[Route("api/contracts")]
public class ContractController : ControllerBase
{
    private readonly IContractService _service;

    public ContractController(
        IContractService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var contracts = await _service.GetAllAsync();

        return Ok(contracts);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var contract = await _service.GetByIdAsync(id);

        if (contract is null)
            return NotFound();

        return Ok(contract);
    }
}