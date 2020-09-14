using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryAndOrderingSystem.Migrations
{
    public partial class RemoveOrderNumberAndAllowNullToDateProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderNumber",
                table: "Orders",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");
        }
    }
}
