using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nursing_Service.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NurseReportField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NurseRepost",
                table: "PatientNeedService",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NurseRepost",
                table: "PatientNeedService");
        }
    }
}
