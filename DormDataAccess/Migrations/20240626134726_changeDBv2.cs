using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DormDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changeDBv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderSideServices_SideService_SideServiceId",
                table: "OrderSideServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SideService",
                table: "SideService");

            migrationBuilder.RenameTable(
                name: "SideService",
                newName: "SideServices");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SideServices",
                table: "SideServices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderSideServices_SideServices_SideServiceId",
                table: "OrderSideServices",
                column: "SideServiceId",
                principalTable: "SideServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderSideServices_SideServices_SideServiceId",
                table: "OrderSideServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SideServices",
                table: "SideServices");

            migrationBuilder.RenameTable(
                name: "SideServices",
                newName: "SideService");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SideService",
                table: "SideService",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderSideServices_SideService_SideServiceId",
                table: "OrderSideServices",
                column: "SideServiceId",
                principalTable: "SideService",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
