using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryAndOrderingSystem.Migrations
{
    public partial class AddDeliveryDateAndAcceptOrderStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"USE [InventoryAndOrderingDB]
                       GO
                       CREATE PROCEDURE [dbo].[SetDeliveryDate]
                        (
                        @OrderID INT,
                        @DeliveryDate Date
                        )
                       AS
                        BEGIN
                            UPDATE [Orders]
                            SET [DeliveryDate] = @DeliveryDate
                            WHERE [OrderID] = @OrderID
                        END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
