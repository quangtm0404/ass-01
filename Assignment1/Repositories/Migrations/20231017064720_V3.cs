using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class V3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "OrderDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("bc92090f-034a-4d23-91e2-3b65689e8e96"), "Phone" },
                    { new Guid("fe2c5ab3-f878-476d-bf6d-07193210e941"), "Laptop" }
                });

            migrationBuilder.InsertData(
                table: "Member",
                columns: new[] { "Id", "City", "CompanyName", "Country", "Email", "Password", "RoleName" },
                values: new object[] { new Guid("3480903f-3f7c-42e7-b5e0-e450110ec548"), "HoChiMinh", "FPT", "VN", "admin@gmail.com", "sa123", "ADMIN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("bc92090f-034a-4d23-91e2-3b65689e8e96"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("fe2c5ab3-f878-476d-bf6d-07193210e941"));

            migrationBuilder.DeleteData(
                table: "Member",
                keyColumn: "Id",
                keyValue: new Guid("3480903f-3f7c-42e7-b5e0-e450110ec548"));

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "OrderDetail");

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
    }
}
