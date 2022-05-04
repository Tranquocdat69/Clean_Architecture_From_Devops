using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECom.Services.Balance.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Balance");

            migrationBuilder.CreateTable(
                name: "kafkaoffset",
                schema: "Balance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Command_Offset = table.Column<long>(type: "bigint", nullable: false),
                    Persistent_Offset = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kafkaoffset", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "userbalance",
                schema: "Balance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Credit_Limit = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userbalance", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "kafkaoffset",
                schema: "Balance");

            migrationBuilder.DropTable(
                name: "userbalance",
                schema: "Balance");
        }
    }
}
