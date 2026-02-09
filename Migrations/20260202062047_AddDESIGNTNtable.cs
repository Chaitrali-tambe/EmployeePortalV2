using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeePortal.Migrations
{
    /// <inheritdoc />
    public partial class AddDESIGNTNtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DesignationTest",
                columns: table => new
                {
                    desnId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    desnName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    department = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignationTest", x => x.desnId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DesignationTest");
        }
    }
}
