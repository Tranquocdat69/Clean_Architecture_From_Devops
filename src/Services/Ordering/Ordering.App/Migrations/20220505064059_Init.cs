using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FPTS.FIT.BDRD.Services.Ordering.App.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ORDERING");

            migrationBuilder.CreateTable(
                name: "orders",
                schema: "ORDERING",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    Order_Date = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Address_Street = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Address_City = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Address_OrderId = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    User_Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "orderItems",
                schema: "ORDERING",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ProductId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    OrderId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    Discount = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    PictureUrl = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ProductName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    Units = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orderItems_orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "ORDERING",
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orderItems_OrderId",
                schema: "ORDERING",
                table: "orderItems",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderItems",
                schema: "ORDERING");

            migrationBuilder.DropTable(
                name: "orders",
                schema: "ORDERING");
        }
    }
}
