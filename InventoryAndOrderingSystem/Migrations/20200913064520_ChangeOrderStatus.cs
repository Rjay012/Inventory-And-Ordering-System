using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryAndOrderingSystem.Migrations
{
    public partial class ChangeOrderStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"USE [InventoryAndOrderingDB]
                       GO
                       CREATE PROCEDURE [dbo].[ChangeOrderStatus]
                        (
                        @OrderID INT,
                        @Status VARCHAR(20)
                        )
                       AS
                        BEGIN
                            UPDATE [Orders]
                            SET [Status] = @Status
                            WHERE [OrderID] = @OrderID
                        END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
