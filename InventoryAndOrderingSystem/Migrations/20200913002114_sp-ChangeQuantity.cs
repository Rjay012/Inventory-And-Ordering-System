using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryAndOrderingSystem.Migrations
{
    public partial class spChangeQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"USE [InventoryAndOrderingDB]
                       GO
                       CREATE PROCEDURE [dbo].[ChangeQuantity]
                        (
                        @ProductID INT,
                        @Quantity INT
                        )
                       AS
                        BEGIN
                            UPDATE [Products]
                            SET [Quantity] = @Quantity
                            WHERE [ProductID] = @ProductID
                        END";
            migrationBuilder.Sql(sp);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryDate",
                table: "Orders",
                type: "Date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryDate",
                table: "Orders",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldNullable: true);
        }
    }
}
