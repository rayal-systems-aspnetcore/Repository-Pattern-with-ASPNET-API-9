using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeManagement.Api.Migrations {
  /// <inheritdoc />
  public partial class Initial: Migration {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) {
      migrationBuilder.CreateTable(
          name: "Departments",
          columns: table => new {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
          },
          constraints: table => {
            table.PrimaryKey("PK_Departments", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Employees",
          columns: table => new {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
            DepartmentId = table.Column<int>(type: "int", nullable: false)
          },
          constraints: table => {
            table.PrimaryKey("PK_Employees", x => x.Id);
            table.ForeignKey(
                      name: "FK_Employees_Departments_DepartmentId",
                      column: x => x.DepartmentId,
                      principalTable: "Departments",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.InsertData(
          table: "Departments",
          columns: new[] { "Id", "Name" },
          values: new object[,]
          {
                    { 1, "Engineering" },
                    { 2, "Marketing" },
                    { 3, "Human Resources" }
          });

      migrationBuilder.CreateIndex(
          name: "IX_Employees_DepartmentId",
          table: "Employees",
          column: "DepartmentId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) {
      migrationBuilder.DropTable(
          name: "Employees");

      migrationBuilder.DropTable(
          name: "Departments");
    }
  }
}
