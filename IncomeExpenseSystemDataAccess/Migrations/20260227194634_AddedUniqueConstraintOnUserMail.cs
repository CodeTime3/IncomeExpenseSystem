using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IncomeExpenseSystemDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedUniqueConstraintOnUserMail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_UserMail",
                table: "Users",
                column: "UserMail",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_UserMail",
                table: "Users");
        }
    }
}
