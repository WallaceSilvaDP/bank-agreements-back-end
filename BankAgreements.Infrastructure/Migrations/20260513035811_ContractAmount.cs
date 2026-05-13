using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankAgreements.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ContractAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "amount",
                table: "contracts",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "amount",
                table: "contracts");
        }
    }
}
