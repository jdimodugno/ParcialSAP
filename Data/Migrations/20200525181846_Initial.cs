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
                columns: new[] { "Number", "DateCreated", "Overdraft", "Type" },
                values: new object[,]
                {
                    { new Guid("e34db995-bd5b-45f3-a85b-7e5ccfc5bfeb"), new DateTime(2020, 5, 25, 15, 18, 46, 270, DateTimeKind.Local).AddTicks(1900), 0.0, 0 },
                    { new Guid("b0979f9d-1c73-42c5-aeea-1740076bf5ab"), new DateTime(2020, 5, 25, 15, 18, 46, 281, DateTimeKind.Local).AddTicks(5050), 0.0, 0 },
                    { new Guid("787a369f-e48f-4e92-b760-5a9c919dcacf"), new DateTime(2020, 5, 25, 15, 18, 46, 281, DateTimeKind.Local).AddTicks(5080), 0.0, 0 },
                    { new Guid("df5ef595-d9d4-47a2-9962-fb69c06d9c78"), new DateTime(2020, 5, 25, 15, 18, 46, 281, DateTimeKind.Local).AddTicks(5080), 2000.0, 1 },
                    { new Guid("9d31da51-44a7-4737-8b3c-0675644f9ff3"), new DateTime(2020, 5, 25, 15, 18, 46, 281, DateTimeKind.Local).AddTicks(5090), 1000.0, 1 },
                    { new Guid("5aad84d1-f9f4-4815-8bbd-cd543ea2e3e3"), new DateTime(2020, 5, 25, 15, 18, 46, 281, DateTimeKind.Local).AddTicks(5090), 750.0, 1 }
                });

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
