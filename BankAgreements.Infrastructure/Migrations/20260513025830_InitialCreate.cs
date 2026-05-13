using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankAgreements.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "debtor",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    documents = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_debtor", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "institutions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    cnpj = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_institutions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "contracts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    contract_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    debtor_id = table.Column<Guid>(type: "uuid", nullable: false),
                    institution_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contracts", x => x.id);
                    table.ForeignKey(
                        name: "fk_contracts_debtor_debtor_id",
                        column: x => x.debtor_id,
                        principalTable: "debtor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_contracts_institutions_institution_id",
                        column: x => x.institution_id,
                        principalTable: "institutions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "agreements",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    number = table.Column<int>(type: "integer", nullable: false),
                    contract_id = table.Column<Guid>(type: "uuid", nullable: false),
                    due_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_agreements", x => x.id);
                    table.ForeignKey(
                        name: "fk_agreements_contracts_contract_id",
                        column: x => x.contract_id,
                        principalTable: "contracts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "installments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    number = table.Column<string>(type: "text", nullable: false),
                    contract_id = table.Column<Guid>(type: "uuid", nullable: true),
                    agreement_id = table.Column<Guid>(type: "uuid", nullable: true),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    due_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    paid = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_installments", x => x.id);
                    table.ForeignKey(
                        name: "fk_installments_agreements_agreement_id",
                        column: x => x.agreement_id,
                        principalTable: "agreements",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_installments_contracts_contract_id",
                        column: x => x.contract_id,
                        principalTable: "contracts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_agreements_contract_id",
                table: "agreements",
                column: "contract_id");

            migrationBuilder.CreateIndex(
                name: "ix_contracts_debtor_id",
                table: "contracts",
                column: "debtor_id");

            migrationBuilder.CreateIndex(
                name: "ix_contracts_institution_id",
                table: "contracts",
                column: "institution_id");

            migrationBuilder.CreateIndex(
                name: "ix_installments_agreement_id",
                table: "installments",
                column: "agreement_id");

            migrationBuilder.CreateIndex(
                name: "ix_installments_contract_id",
                table: "installments",
                column: "contract_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "installments");

            migrationBuilder.DropTable(
                name: "agreements");

            migrationBuilder.DropTable(
                name: "contracts");

            migrationBuilder.DropTable(
                name: "debtor");

            migrationBuilder.DropTable(
                name: "institutions");
        }
    }
}
