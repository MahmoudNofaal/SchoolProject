using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CA_School_Project.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_refresh_token2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TokenString",
                table: "UserRefreshTokens",
                newName: "RefreshToken");

            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "UserRefreshTokens",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "UserRefreshTokens");

            migrationBuilder.RenameColumn(
                name: "RefreshToken",
                table: "UserRefreshTokens",
                newName: "TokenString");
        }
    }
}
