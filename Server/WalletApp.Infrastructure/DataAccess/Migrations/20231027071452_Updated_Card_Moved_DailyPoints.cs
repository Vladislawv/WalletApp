using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WalletApp.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCardMovedDailyPoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyPoints",
                table: "Cards");

            migrationBuilder.AddColumn<string>(
                name: "DailyPoints",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyPoints",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "DailyPoints",
                table: "Cards",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
