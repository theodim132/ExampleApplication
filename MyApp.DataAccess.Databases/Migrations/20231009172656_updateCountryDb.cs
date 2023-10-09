using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp.DataAccess.Databases.Migrations
{
    /// <inheritdoc />
    public partial class updateCountryDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Countries",
                newName: "OfficialName");

            migrationBuilder.AddColumn<string>(
                name: "CommonName",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NativeNameSpaCommon",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NativeNameSpaOfficial",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommonName",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "NativeNameSpaCommon",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "NativeNameSpaOfficial",
                table: "Countries");

            migrationBuilder.RenameColumn(
                name: "OfficialName",
                table: "Countries",
                newName: "Name");
        }
    }
}
