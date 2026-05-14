using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankAgreements.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAnnualInterestRateToInstitution : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "annual_interest_rate",
                table: "institutions",
                type: "numeric(5,4)",
                nullable: false,
                defaultValue: 0.10m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "annual_interest_rate",
                table: "institutions");
        }
    }
}
