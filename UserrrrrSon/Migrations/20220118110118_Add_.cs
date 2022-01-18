using Microsoft.EntityFrameworkCore.Migrations;

namespace UserrrrrSon.Migrations
{
    public partial class Add_ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BankId",
                table: "Invoices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_BankId",
                table: "Invoices",
                column: "BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Banks_BankId",
                table: "Invoices",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Banks_BankId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_BankId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "Invoices");
        }
    }
}
