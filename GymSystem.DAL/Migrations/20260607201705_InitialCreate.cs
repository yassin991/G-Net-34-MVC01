using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "DurationCheckValue",
                table: "Plans");

            migrationBuilder.AddCheckConstraint(
                name: "DurationCheckValue",
                table: "Plans",
                sql: "[DurationDays] BETWEEN 1 AND 365");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "DurationCheckValue",
                table: "Plans");

            migrationBuilder.AddCheckConstraint(
                name: "DurationCheckValue",
                table: "Plans",
                sql: "DurationDays BETWEEN 1 AND 365");
        }
    }
}
