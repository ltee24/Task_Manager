using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task_Manager.Migrations
{
    /// <inheritdoc />
    public partial class EditAppplicationUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Naame",
                table: "AspNetUsers",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AspNetUsers",
                newName: "Naame");
        }
    }
}
