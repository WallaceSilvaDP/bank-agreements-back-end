using BankAgreements.Infrastructure.Data;
using BankAgreements.Infrastructure.Repositories.Agreements;
using BankAgreements.Infrastructure.Repositories.Contracts;
using BankAgreements.Infrastructure.Seed;
using BankAgreements.Services.Agreements;
using BankAgreements.Services.Contracts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddScoped<IAgreementRepository, AgreementRepository>();
builder.Services.AddScoped<IAgreementService, AgreementService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura o DbContext com PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseNpgsql(
		builder.Configuration.GetConnectionString("DefaultConnection"))
		.UseSnakeCaseNamingConvention();

});

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAngular",
		policy =>
		{
			policy
				.AllowAnyOrigin()
				.AllowAnyHeader()
				.AllowAnyMethod();
		});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<AppDbContext>();

	if (!context.Institutions.Any())
	{
		context.Institutions.AddRange(
			InstitutionSeed.GetInstitutions());
	}

	if (!context.Debtors.Any())
	{
		context.Debtors.AddRange(
			DebtorSeed.GetDebtors());
	}

	if (!context.Contracts.Any())
    {
        var contracts = ContractSeed.GetContracts();

        context.Contracts.AddRange(contracts);
    }

	context.SaveChanges();

}

app.UseHttpsRedirection();

app.UseCors("AllowAngular");

app.UseAuthorization();

app.MapControllers();

app.Run();
