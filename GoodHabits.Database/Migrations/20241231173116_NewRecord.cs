using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodHabits.Database.Migrations
{
    /// <inheritdoc />
    public partial class NewRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenantName",
                table: "Habits",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Habits",
                keyColumn: "Id",
                keyValue: 1,
                column: "TenantName",
                value: "DefaultTenant");

            migrationBuilder.UpdateData(
                table: "Habits",
                keyColumn: "Id",
                keyValue: 2,
                column: "TenantName",
                value: "Bluewave");

            migrationBuilder.UpdateData(
                table: "Habits",
                keyColumn: "Id",
                keyValue: 3,
                column: "TenantName",
                value: "CloudSphere");

            migrationBuilder.UpdateData(
                table: "Habits",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Name", "TenantName" },
                values: new object[] { "Become a french speaker", "learn French", "DefaultTenant" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantName",
                table: "Habits");

            migrationBuilder.UpdateData(
                table: "Habits",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Write a blog post", "Write" });
        }
    }
}
