using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DormDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seeddataforDorm : Migration
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
                    { 2, "Floor 2" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "CurrentNumberOfPeople", "IsMaximum", "MaximumNumberOfPeople", "Name" },
                values: new object[,]
                {
                    { 1, 1, false, 2, "Room 101" },
                    { 2, 2, true, 2, "Room 102" },
                    { 3, 0, false, 1, "Room 201" }
                });

            migrationBuilder.InsertData(
                table: "DormFloors",
                columns: new[] { "DormId", "FloorId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "FloorRooms",
                columns: new[] { "FloorId", "RoomId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
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
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "FloorRooms",
                keyColumns: new[] { "FloorId", "RoomId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "FloorRooms",
                keyColumns: new[] { "FloorId", "RoomId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "FloorRooms",
                keyColumns: new[] { "FloorId", "RoomId" },
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
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
