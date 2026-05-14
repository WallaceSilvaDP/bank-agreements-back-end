using BankAgreements.Infrastructure.Entities.Agreements;
using BankAgreements.Infrastructure.Entities.Installments;
using BankAgreements.Infrastructure.Repositories.Agreements;
using BankAgreements.Infrastructure.Repositories.Contracts;
using BankAgreements.Services.Agreements.Calculators;
using BankAgreements.Shared.DTOs.Agreements;

namespace BankAgreements.Services.Agreements;

public class AgreementService : IAgreementService
{
    private readonly IAgreementRepository _agreementRepository;
    private readonly IContractRepository _contractRepository;

    public AgreementService(
        IAgreementRepository agreementRepository,
        IContractRepository contractRepository)
    {
        _agreementRepository = agreementRepository;
        _contractRepository = contractRepository;
    }

    public async Task<List<AgreementDTO>> GetAllAsync()
    {
        var agreements = await _agreementRepository.GetAllAsync();

        return agreements.Select(agreement =>
        {
            return new AgreementDTO
            {
                Id = agreement.Id,
                Number = agreement.Number,
                ContractId = agreement.ContractId,
                ContractNumber = agreement.Contract.ContractNumber,
                DueDate = agreement.DueDate,
                Amount = agreement.Amount,
                TotalInstallments = agreement.Installments?.Count ?? 0
            };
        }).ToList();
    }

    public async Task<AgreementDTO?> GetByIdAsync(Guid id)
    {
        var agreement = await _agreementRepository.GetByIdAsync(id);

        if (agreement is null)
            return null;

        return new AgreementDTO
        {
            Id = agreement.Id,
            Number = agreement.Number,
            ContractId = agreement.ContractId,
            ContractNumber = agreement.Contract.ContractNumber,
            DueDate = agreement.DueDate,
            Amount = agreement.Amount,
            TotalInstallments = agreement.Installments?.Count ?? 0
        };
    }

    public async Task<SimulatedAgreementDTO> SimulateAsync(CreateAgreementDTO dto)
    {
        // Validar número de parcelas
        //if (dto.NumberOfInstallments <= 0)
        //{
        //    return new SimulatedAgreementDTO
        //    {
        //        Id = Guid.NewGuid(),
        //        ContractId = dto.ContractId,
        //        NumberOfInstallments = dto.NumberOfInstallments,
        //        IsValid = false,
        //        ValidationMessage = "Número de parcelas deve ser maior que zero."
        //    };
        //}

        // Buscar o contrato com a instituição
        var contract = await _contractRepository.GetByIdAsync(dto.ContractId);

        if (contract is null)
        {
            return new SimulatedAgreementDTO
            {
                Id = Guid.NewGuid(),
                ContractId = dto.ContractId,
                IsValid = false,
                ValidationMessage = "Contrato não encontrado."
            };
        }

        // Calcular débito aberto (parcelas não pagas)
        var openInstallments = contract.Installments
            ?.Where(x => !x.Paid)
            .ToList() ?? [];

        var openDebtAmount = openInstallments.Sum(x => x.Amount);

        if (openDebtAmount <= 0)
        {
            return new SimulatedAgreementDTO
            {
                Id = Guid.NewGuid(),
                ContractId = dto.ContractId,
                IsValid = false,
                ValidationMessage = "Não há débito aberto neste contrato."
            };
        }

        // Validar quantidade de parcelas contra limite da instituição
        var maxInstallments = contract.Institution.MaxInstallments;
        
        //if (dto.NumberOfInstallments > maxInstallments)
        //{
        //    return new SimulatedAgreementDTO
        //    {
        //        Id = Guid.NewGuid(),
        //        ContractId = dto.ContractId,
        //        NumberOfInstallments = dto.NumberOfInstallments,
        //        InstitutionName = contract.Institution.Name,
        //        MaxInstallmentsAllowed = maxInstallments,
        //        ApplicableInterestRate = contract.Institution.AnnualInterestRate,
        //        OpenDebtAmount = openDebtAmount,
        //        IsValid = false,
        //        ValidationMessage = $"Número de parcelas ({dto.NumberOfInstallments}) excede o limite permitido pela instituição ({maxInstallments})."
        //    };
        //}

        // Calcular valor da parcela com juros
        var installmentAmount = InstallmentCalculator.CalculateInstallmentAmount(
            openDebtAmount,
			maxInstallments,
            contract.Institution.AnnualInterestRate);

        // Gerar datas e valores das parcelas
        var installmentDates = InstallmentCalculator.CalculateInstallmentDates(
            dto.FirstDueDate,
            maxInstallments,
            installmentAmount);

        // Ajustar última parcela para garantir o total exato
        InstallmentCalculator.AdjustLastInstallment(installmentDates, openDebtAmount);

        // Converter para DTO
        var simulatedInstallments = installmentDates
            .Select(x => new SimulatedInstallmentDTO
            {
                Number = x.Number,
                DueDate = x.DueDate,
                Amount = x.Amount
            })
            .ToList();

        // Calcular valor total com juros
        var totalAmountWithInterest = simulatedInstallments.Sum(x => x.Amount);

        return new SimulatedAgreementDTO
        {
            Id = Guid.NewGuid(),
            Number = 1, // Agreement sempre será o primeiro de cada ciclo
            ContractId = dto.ContractId,
            ContractNumber = contract.ContractNumber,
            FirstDueDate = dto.FirstDueDate,
            Amount = decimal.Round(totalAmountWithInterest, 2),
            TotalInstallments = maxInstallments,
            InstitutionName = contract.Institution.Name,
            MaxInstallmentsAllowed = maxInstallments,
            ApplicableInterestRate = contract.Institution.AnnualInterestRate,
            OpenDebtAmount = openDebtAmount,
            Installments = simulatedInstallments,
            IsValid = true,
            ValidationMessage = null
        };
    }

	public async Task<bool> CreateAsync(CreateAgreementDTO dto)
	{
		// Reaproveita simulação
		var simulation =
			await SimulateAsync(dto);

		if (!simulation.IsValid)
		{
			return false;
		}

		var agreement = new Agreement
		{
			Id = Guid.NewGuid(),

			Number = simulation.Number,

			ContractId = simulation.ContractId,

			DueDate = simulation.FirstDueDate,

			Amount = simulation.Amount,

			Installments = []
		};

		foreach (var installment in simulation.Installments)
		{
			agreement.Installments.Add(
				new Installment
				{
					Id = Guid.NewGuid(),

					Number =
						installment.Number.ToString(),

					AgreementId = agreement.Id,

					Amount = installment.Amount,

					DueDate = installment.DueDate,

					Paid = false
				});
		}

		await _agreementRepository.CreateAsync(
			agreement);

		return true;
	}
}