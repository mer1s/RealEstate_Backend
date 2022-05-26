using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class OneToManyUser9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4477d989-9739-4ef4-98a1-0b9772564a95");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a8a51a82-1f16-4936-9dde-24cbeacf3e5a");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Ads",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "22bac382-1b7b-4903-9562-bc03c87c374e", "8711bb06-dc5f-44fb-af3e-27deb6366b67", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "df017323-e9e8-43bc-a590-663c4cfe7e1c", "6a77458c-1449-48f2-b613-92ae3e77fc50", "Member", "MEMBER" });

            migrationBuilder.CreateIndex(
                name: "IX_Ads_AppUserId",
                table: "Ads",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_AspNetUsers_AppUserId",
                table: "Ads",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ads_AspNetUsers_AppUserId",
                table: "Ads");

            migrationBuilder.DropIndex(
                name: "IX_Ads_AppUserId",
                table: "Ads");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22bac382-1b7b-4903-9562-bc03c87c374e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df017323-e9e8-43bc-a590-663c4cfe7e1c");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Ads",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4477d989-9739-4ef4-98a1-0b9772564a95", "8a11f40f-8660-4d5c-a190-2cc85d78e2ae", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a8a51a82-1f16-4936-9dde-24cbeacf3e5a", "a7b30e9c-fbc7-474a-8f98-dc2ffced66e1", "Admin", "ADMIN" });
        }
    }
}
