using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class BasketInit2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1d709e8-d60a-4cf6-a0ed-97b8d949d29e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "db802b0b-46af-4934-b598-b45a6332164e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "04f7b4bf-a632-4a45-8b4a-c7d4e719b5bb", "6d3077fc-3ed0-415b-9202-ea3bae615302", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7794ed58-bd7e-4897-a38d-dea57041fcff", "49049df2-0ec2-4ae0-9f6c-4abb42f73cdf", "Member", "MEMBER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "04f7b4bf-a632-4a45-8b4a-c7d4e719b5bb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7794ed58-bd7e-4897-a38d-dea57041fcff");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d1d709e8-d60a-4cf6-a0ed-97b8d949d29e", "c1798d9b-2aee-4725-bed2-b4f938d19b46", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "db802b0b-46af-4934-b598-b45a6332164e", "0f048708-a2d2-4316-9a08-593f04d25c6e", "Admin", "ADMIN" });
        }
    }
}
