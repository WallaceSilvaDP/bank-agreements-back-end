using BankAgreements.Infrastructure.Entities.Contracts;
using BankAgreements.Infrastructure.Entities.Installments;

namespace BankAgreements.Infrastructure.Seed;

public static class ContractSeed
{
    public static List<Contract> GetContracts()
    {
        var debtorId =
            Guid.Parse("c7106e39-2d81-491d-95bf-13fbfbaa703f");

        var institutionId =
            Guid.Parse("164d38ad-7482-4d6d-9996-1010a08a316d");

        return new List<Contract>
        {
            // =========================
            // CONTRATO 1
            // 2 parcelas vencidas
            // =========================
            new Contract
            {
                Id = Guid.NewGuid(),
                ContractNumber = "CTR-2026-0001",
                Amount = 4500m,

                DebtorId = debtorId,
                InstitutionId = institutionId,

                Installments = new List<Installment>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Number = "001",
                        Amount = 1500m,
                        DueDate = DateTime.UtcNow.AddDays(-60),
                        Paid = false
                    },

                    new()
                    {
                        Id = Guid.NewGuid(),
                        Number = "002",
                        Amount = 1500m,
                        DueDate = DateTime.UtcNow.AddDays(-30),
                        Paid = false
                    },

                    new()
                    {
                        Id = Guid.NewGuid(),
                        Number = "003",
                        Amount = 1500m,
                        DueDate = DateTime.UtcNow.AddDays(30),
                        Paid = false
                    }
                }
            },

            // =========================
            // CONTRATO 2
            // Totalmente pago
            // =========================
            new Contract
            {
                Id = Guid.NewGuid(),
                ContractNumber = "CTR-2026-0002",
                Amount = 3000m,

                DebtorId = debtorId,
                InstitutionId = institutionId,

                Installments = new List<Installment>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Number = "001",
                        Amount = 1000m,
                        DueDate = DateTime.UtcNow.AddDays(-90),
                        Paid = true
                    },

                    new()
                    {
                        Id = Guid.NewGuid(),
                        Number = "002",
                        Amount = 1000m,
                        DueDate = DateTime.UtcNow.AddDays(-60),
                        Paid = true
                    },

                    new()
                    {
                        Id = Guid.NewGuid(),
                        Number = "003",
                        Amount = 1000m,
                        DueDate = DateTime.UtcNow.AddDays(-30),
                        Paid = true
                    }
                }
            },

            // =========================
            // CONTRATO 3
            // Parcelas futuras
            // =========================
            new Contract
            {
                Id = Guid.NewGuid(),
                ContractNumber = "CTR-2026-0003",
                Amount = 8000m,

                DebtorId = debtorId,
                InstitutionId = institutionId,

                Installments = new List<Installment>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Number = "001",
                        Amount = 2000m,
                        DueDate = DateTime.UtcNow.AddDays(15),
                        Paid = false
                    },

                    new()
                    {
                        Id = Guid.NewGuid(),
                        Number = "002",
                        Amount = 2000m,
                        DueDate = DateTime.UtcNow.AddDays(45),
                        Paid = false
                    },

                    new()
                    {
                        Id = Guid.NewGuid(),
                        Number = "003",
                        Amount = 2000m,
                        DueDate = DateTime.UtcNow.AddDays(75),
                        Paid = false
                    },

                    new()
                    {
                        Id = Guid.NewGuid(),
                        Number = "004",
                        Amount = 2000m,
                        DueDate = DateTime.UtcNow.AddDays(105),
                        Paid = false
                    }
                }
            },

            // =========================
            // CONTRATO 4
            // Atraso severo
            // =========================
            new Contract
            {
                Id = Guid.NewGuid(),
                ContractNumber = "CTR-2026-0004",
                Amount = 12000m,

                DebtorId = debtorId,
                InstitutionId = institutionId,

                Installments = new List<Installment>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Number = "001",
                        Amount = 3000m,
                        DueDate = DateTime.UtcNow.AddDays(-120),
                        Paid = false
                    },

                    new()
                    {
                        Id = Guid.NewGuid(),
                        Number = "002",
                        Amount = 3000m,
                        DueDate = DateTime.UtcNow.AddDays(-90),
                        Paid = false
                    },

                    new()
                    {
                        Id = Guid.NewGuid(),
                        Number = "003",
                        Amount = 3000m,
                        DueDate = DateTime.UtcNow.AddDays(-60),
                        Paid = false
                    },

                    new()
                    {
                        Id = Guid.NewGuid(),
                        Number = "004",
                        Amount = 3000m,
                        DueDate = DateTime.UtcNow.AddDays(-30),
                        Paid = false
                    }
                }
            },

            // =========================
            // CONTRATO 5
            // Parcialmente pago
            // =========================
            new Contract
            {
                Id = Guid.NewGuid(),
                ContractNumber = "CTR-2026-0005",
                Amount = 6000m,

                DebtorId = debtorId,
                InstitutionId = institutionId,

                Installments = new List<Installment>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Number = "001",
                        Amount = 1500m,
                        DueDate = DateTime.UtcNow.AddDays(-90),
                        Paid = true
                    },

                    new()
                    {
                        Id = Guid.NewGuid(),
                        Number = "002",
                        Amount = 1500m,
                        DueDate = DateTime.UtcNow.AddDays(-60),
                        Paid = true
                    },

                    new()
                    {
                        Id = Guid.NewGuid(),
                        Number = "003",
                        Amount = 1500m,
                        DueDate = DateTime.UtcNow.AddDays(-30),
                        Paid = false
                    },

                    new()
                    {
                        Id = Guid.NewGuid(),
                        Number = "004",
                        Amount = 1500m,
                        DueDate = DateTime.UtcNow.AddDays(30),
                        Paid = false
                    }
                }
            }
        };
    }
}