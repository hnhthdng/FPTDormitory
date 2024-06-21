using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DormDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Transaction_TransactionId",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_TransactionId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "AppUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "AppUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_TransactionId",
                table: "AppUsers",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Transaction_TransactionId",
                table: "AppUsers",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
