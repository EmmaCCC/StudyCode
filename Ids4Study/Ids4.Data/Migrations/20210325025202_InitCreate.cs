using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ids4.Data.Migrations
{
    public partial class InitCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppClients",
                columns: table => new
                {
                    ClientId = table.Column<string>(nullable: false),
                    ClientName = table.Column<string>(nullable: true),
                    Secret = table.Column<string>(nullable: true),
                    AllowedScopes = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppClients", x => x.ClientId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppClients");
        }
    }
}
