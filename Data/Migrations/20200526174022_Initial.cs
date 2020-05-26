using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankAccount",
                columns: table => new
                {
                    Number = table.Column<Guid>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Alias = table.Column<string>(nullable: false),
                    Overdraft = table.Column<double>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccount", x => x.Number);
                });

            migrationBuilder.CreateTable(
                name: "Deposit",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    TargetAccountId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deposit_BankAccount_TargetAccountId",
                        column: x => x.TargetAccountId,
                        principalTable: "BankAccount",
                        principalColumn: "Number");
                });

            migrationBuilder.CreateTable(
                name: "Transfer",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    SourceAccountId = table.Column<Guid>(nullable: false),
                    TargetAccountId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfer_BankAccount_SourceAccountId",
                        column: x => x.SourceAccountId,
                        principalTable: "BankAccount",
                        principalColumn: "Number");
                    table.ForeignKey(
                        name: "FK_Transfer_BankAccount_TargetAccountId",
                        column: x => x.TargetAccountId,
                        principalTable: "BankAccount",
                        principalColumn: "Number");
                });

            migrationBuilder.CreateTable(
                name: "Withdrawal",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    TargetAccountId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Withdrawal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Withdrawal_BankAccount_TargetAccountId",
                        column: x => x.TargetAccountId,
                        principalTable: "BankAccount",
                        principalColumn: "Number");
                });

            migrationBuilder.InsertData(
                table: "BankAccount",
                columns: new[] { "Number", "Alias", "DateCreated", "Overdraft", "Type" },
                values: new object[,]
                {
                    { new Guid("78bc3ff5-2b92-4d6e-8367-bc10b6e16d66"), "CA.SEED.ALPHA", new DateTime(2020, 5, 26, 14, 40, 22, 232, DateTimeKind.Local).AddTicks(4960), 0.0, 0 },
                    { new Guid("b3098986-7560-495c-893c-120a7a4108d9"), "CA.SEED.BETA", new DateTime(2020, 5, 26, 14, 40, 22, 243, DateTimeKind.Local).AddTicks(9390), 0.0, 0 },
                    { new Guid("991415f2-cb31-4d9d-a6a4-1058ae0b960b"), "CA.SEED.GAMMA", new DateTime(2020, 5, 26, 14, 40, 22, 243, DateTimeKind.Local).AddTicks(9430), 0.0, 0 },
                    { new Guid("ef308606-ed29-4ea3-8478-9015d97fce55"), "CC.SEED.RHO", new DateTime(2020, 5, 26, 14, 40, 22, 243, DateTimeKind.Local).AddTicks(9440), 2000.0, 1 },
                    { new Guid("e1743286-9b70-4dfa-b157-363bd33ece26"), "CC.SEED.EPSILON", new DateTime(2020, 5, 26, 14, 40, 22, 243, DateTimeKind.Local).AddTicks(9440), 1000.0, 1 },
                    { new Guid("75fc66c6-242f-455e-a6b9-682270331694"), "CC.SEED.OMEGA", new DateTime(2020, 5, 26, 14, 40, 22, 243, DateTimeKind.Local).AddTicks(9440), 750.0, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_Alias",
                table: "BankAccount",
                column: "Alias",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deposit_TargetAccountId",
                table: "Deposit",
                column: "TargetAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_SourceAccountId",
                table: "Transfer",
                column: "SourceAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_TargetAccountId",
                table: "Transfer",
                column: "TargetAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Withdrawal_TargetAccountId",
                table: "Withdrawal",
                column: "TargetAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deposit");

            migrationBuilder.DropTable(
                name: "Transfer");

            migrationBuilder.DropTable(
                name: "Withdrawal");

            migrationBuilder.DropTable(
                name: "BankAccount");
        }
    }
}
