using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KEShop_Api_N_Tier_Art.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class addCodeToApplictionUser1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodeResetPassword",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordResetCodeExpiry",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeResetPassword",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordResetCodeExpiry",
                table: "Users");
        }
    }
}
