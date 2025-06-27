using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacySystem.InfastructureLayer.Data.Migrations
{
    /// <inheritdoc />
    public partial class PharmacyPasswordResetOTP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordResetTokenExpiry",
                table: "Pharmacies",
                newName: "PasswordResetOTPExpiry");

            migrationBuilder.RenameColumn(
                name: "PasswordResetToken",
                table: "Pharmacies",
                newName: "PasswordResetOTP");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordResetOTPExpiry",
                table: "Pharmacies",
                newName: "PasswordResetTokenExpiry");

            migrationBuilder.RenameColumn(
                name: "PasswordResetOTP",
                table: "Pharmacies",
                newName: "PasswordResetToken");
        }
    }
}
