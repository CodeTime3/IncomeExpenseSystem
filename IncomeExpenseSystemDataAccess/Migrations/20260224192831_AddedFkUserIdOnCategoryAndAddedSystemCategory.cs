using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IncomeExpenseSystemDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedFkUserIdOnCategoryAndAddedSystemCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SystemCategoryId",
                table: "Transactions",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Categories",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "SystemCategories",
                columns: table => new
                {
                    SystemCategoryId = table.Column<Guid>(type: "char(36)", nullable: false),
                    SystemCategoryName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemCategories", x => x.SystemCategoryId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SystemCategoryId",
                table: "Transactions",
                column: "SystemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserId",
                table: "Categories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_UserId",
                table: "Categories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_SystemCategories_SystemCategoryId",
                table: "Transactions",
                column: "SystemCategoryId",
                principalTable: "SystemCategories",
                principalColumn: "SystemCategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_UserId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_SystemCategories_SystemCategoryId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "SystemCategories");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_SystemCategoryId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Categories_UserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "SystemCategoryId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Categories");
        }
    }
}
