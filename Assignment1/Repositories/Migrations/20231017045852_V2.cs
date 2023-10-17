using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("c27c7e1d-e566-4f04-9b16-87963443b675"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("eed32fad-181f-44c4-9c15-57cb144c6628"));

            migrationBuilder.DeleteData(
                table: "Member",
                keyColumn: "Id",
                keyValue: new Guid("e6acee85-3b70-4047-a963-47ef7c23eb95"));

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("5c53124b-5641-4c7a-961e-7a30da947e1c"), "Laptop" },
                    { new Guid("6a599e78-f1bc-456b-b92a-47d00d9e064c"), "Phone" }
                });

            migrationBuilder.InsertData(
                table: "Member",
                columns: new[] { "Id", "City", "CompanyName", "Country", "Email", "Password", "RoleName" },
                values: new object[] { new Guid("76422d2c-db36-4eb1-87f6-63d451604f64"), "HoChiMinh", "FPT", "VN", "admin@gmail.com", "sa123", "ADMIN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("5c53124b-5641-4c7a-961e-7a30da947e1c"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("6a599e78-f1bc-456b-b92a-47d00d9e064c"));

            migrationBuilder.DeleteData(
                table: "Member",
                keyColumn: "Id",
                keyValue: new Guid("76422d2c-db36-4eb1-87f6-63d451604f64"));

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("c27c7e1d-e566-4f04-9b16-87963443b675"), "Laptop" },
                    { new Guid("eed32fad-181f-44c4-9c15-57cb144c6628"), "Phone" }
                });

            migrationBuilder.InsertData(
                table: "Member",
                columns: new[] { "Id", "City", "CompanyName", "Country", "Email", "Password", "RoleName" },
                values: new object[] { new Guid("e6acee85-3b70-4047-a963-47ef7c23eb95"), "HoChiMinh", "FPT", "VN", "admin@gmail.com", "sa123", "ADMIN" });
        }
    }
}
