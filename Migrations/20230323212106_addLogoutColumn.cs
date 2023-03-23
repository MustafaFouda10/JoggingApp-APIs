using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoggingApp.Migrations
{
    public partial class addLogoutColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLogout",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLogout",
                table: "Users");
        }
    }
}
