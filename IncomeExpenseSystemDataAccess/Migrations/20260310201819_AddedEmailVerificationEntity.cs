using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IncomeExpenseSystemDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedEmailVerificationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailVerifications",
                columns: table => new
                {
                    EmailVerificationId = table.Column<Guid>(type: "char(36)", nullable: false),
                    EmailVerificationToken = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    EmailVerificationExpiresAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    EmailVerifiedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailVerifications", x => x.EmailVerificationId);
                    table.ForeignKey(
                        name: "FK_EmailVerifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EmailVerifications_UserId",
                table: "EmailVerifications",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailVerifications");
        }
    }
}
