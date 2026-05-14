using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankAgreements.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DebtosNameUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_contracts_debtor_debtor_id",
                table: "contracts");

            migrationBuilder.DropPrimaryKey(
                name: "pk_debtor",
                table: "debtor");

            migrationBuilder.RenameTable(
                name: "debtor",
                newName: "debtors");

            migrationBuilder.AddPrimaryKey(
                name: "pk_debtors",
                table: "debtors",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_contracts_debtors_debtor_id",
                table: "contracts",
                column: "debtor_id",
                principalTable: "debtors",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_contracts_debtors_debtor_id",
                table: "contracts");

            migrationBuilder.DropPrimaryKey(
                name: "pk_debtors",
                table: "debtors");

            migrationBuilder.RenameTable(
                name: "debtors",
                newName: "debtor");

            migrationBuilder.AddPrimaryKey(
                name: "pk_debtor",
                table: "debtor",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_contracts_debtor_debtor_id",
                table: "contracts",
                column: "debtor_id",
                principalTable: "debtor",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
