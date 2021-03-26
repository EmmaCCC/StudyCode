using Microsoft.EntityFrameworkCore.Migrations;

namespace Ids4.Data.Migrations
{
    public partial class AddField_GrantTypes_ToTable_AppClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GrantTypes",
                table: "AppClients",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrantTypes",
                table: "AppClients");
        }
    }
}
