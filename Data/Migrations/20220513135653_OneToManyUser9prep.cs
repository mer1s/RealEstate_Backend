using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class OneToManyUser9prep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51f5c689-5c7c-47d0-b5b3-e3d5523102fb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c98720b6-53f7-4eb0-8a90-cceedccc4c00");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Ads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4477d989-9739-4ef4-98a1-0b9772564a95", "8a11f40f-8660-4d5c-a190-2cc85d78e2ae", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a8a51a82-1f16-4936-9dde-24cbeacf3e5a", "a7b30e9c-fbc7-474a-8f98-dc2ffced66e1", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4477d989-9739-4ef4-98a1-0b9772564a95");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a8a51a82-1f16-4936-9dde-24cbeacf3e5a");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Ads");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "51f5c689-5c7c-47d0-b5b3-e3d5523102fb", "9117e14e-a5b0-4527-98f2-758ce33d2afe", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c98720b6-53f7-4eb0-8a90-cceedccc4c00", "df257764-d1c1-4898-9c9b-00d9f95f0f28", "Member", "MEMBER" });
        }
    }
}
