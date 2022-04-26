using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECom.Services.Balance.Infrastructure.UserBalanceMigrations
{
    public partial class Initial_Oracle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "kafkaoffset",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Command_Offset = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    Persistent_Offset = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kafkaoffset", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "userbalance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    User_Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
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
                name: "kafkaoffset");

            migrationBuilder.DropTable(
                name: "userbalance");
        }
    }
}
