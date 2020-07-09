using Microsoft.EntityFrameworkCore.Migrations;

namespace LiveShow.Dal.Migrations
{
    public partial class UpdatedAttendancesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Shows_ShowId",
                table: "Attendances");

            migrationBuilder.AddForeignKey(
              name: "FK_Attendances_Shows_ShowId",
              table: "Attendances",
              column: "ShowId",
              principalTable: "Shows",
              schema: null,
              principalSchema: null,
              principalColumn: "Id",
              onDelete: ReferentialAction.Cascade
              );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Shows_ShowId",
                table: "Attendances");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Shows_ShowId",
                table: "Attendances",
                column: "ShowId",
                principalTable: "Shows",
                schema: null,
                principalSchema: null,
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction
                );
        }
    }
}
