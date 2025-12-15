using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CA_School_Project.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_user_refresh_token : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpirDate",
                table: "UserRefreshTokens",
                newName: "ExpiryDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpiryDate",
                table: "UserRefreshTokens",
                newName: "ExpirDate");
        }
    }
}
