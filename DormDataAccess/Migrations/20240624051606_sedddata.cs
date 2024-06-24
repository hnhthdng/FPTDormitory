using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DormDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class sedddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Dorms",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Dorm A" },
                    { 2, "Dorm B" }
                });

            migrationBuilder.InsertData(
                table: "Floors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Floor 1" },
                    { 2, "Floor 2" },
                    { 3, "Floor 3" }
                });

            migrationBuilder.InsertData(
                table: "DormFloors",
                columns: new[] { "DormId", "FloorId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 2, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DormFloors",
                keyColumns: new[] { "DormId", "FloorId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "DormFloors",
                keyColumns: new[] { "DormId", "FloorId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "DormFloors",
                keyColumns: new[] { "DormId", "FloorId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "DormFloors",
                keyColumns: new[] { "DormId", "FloorId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "Dorms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Dorms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Floors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Floors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Floors",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
