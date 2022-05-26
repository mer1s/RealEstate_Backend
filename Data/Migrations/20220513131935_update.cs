using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0218bdd-7ac3-4887-822a-bf62ed46c579");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fee19188-d8da-4fbb-b4e9-5ca39908695c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "51f5c689-5c7c-47d0-b5b3-e3d5523102fb", "9117e14e-a5b0-4527-98f2-758ce33d2afe", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c98720b6-53f7-4eb0-8a90-cceedccc4c00", "df257764-d1c1-4898-9c9b-00d9f95f0f28", "Member", "MEMBER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51f5c689-5c7c-47d0-b5b3-e3d5523102fb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c98720b6-53f7-4eb0-8a90-cceedccc4c00");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d0218bdd-7ac3-4887-822a-bf62ed46c579", "abe66cda-03d3-405e-ae19-7051086af76f", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fee19188-d8da-4fbb-b4e9-5ca39908695c", "649e3ce3-f964-4545-920c-1c287af9e63f", "Admin", "ADMIN" });
        }
    }
}
