using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacySystem.InfastructureLayer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRepresentativePasswordResetFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordResetOTP",
                table: "Representatives",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordResetOTPExpiry",
                table: "Representatives",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordResetOTP",
                table: "Representatives");

            migrationBuilder.DropColumn(
                name: "PasswordResetOTPExpiry",
                table: "Representatives");
        }
    }
}
