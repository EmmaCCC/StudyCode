using Microsoft.EntityFrameworkCore.Migrations;

namespace Ids4.Data.Migrations
{
    public partial class AddCreateByField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreateBy",
                table: "AppClients",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "AppClients");
        }
    }
}
