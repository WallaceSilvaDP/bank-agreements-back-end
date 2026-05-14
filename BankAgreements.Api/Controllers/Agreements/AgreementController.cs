using BankAgreements.Services.Agreements;
using BankAgreements.Shared.DTOs.Agreements;
using Microsoft.AspNetCore.Mvc;

namespace BankAgreements.Api.Controllers.Agreements;

[ApiController]
[Route("api/agreements")]
public class AgreementController : ControllerBase
{
    private readonly IAgreementService _service;

    public AgreementController(IAgreementService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var agreements = await _service.GetAllAsync();

        return Ok(agreements);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var agreement = await _service.GetByIdAsync(id);

        if (agreement is null)
            return NotFound();

        return Ok(agreement);
    }

    [HttpPost("simulate")]
    public async Task<IActionResult> Simulate([FromBody] CreateAgreementDTO dto)
    {
        var simulatedAgreement = await _service.SimulateAsync(dto);

        return Ok(simulatedAgreement);
    }

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateAgreementDTO dto)
	{
		var created =
			await _service.CreateAsync(dto);

		if (!created)
		{
			return BadRequest(
				"Não foi possível criar o acordo.");
		}

		return Ok();
	}
}
