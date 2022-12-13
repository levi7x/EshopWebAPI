using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EshopWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class addIsActiveOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOrderActive",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOrderActive",
                table: "Orders");
        }
    }
}
