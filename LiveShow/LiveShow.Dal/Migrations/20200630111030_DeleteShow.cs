using Microsoft.EntityFrameworkCore.Migrations;

namespace LiveShow.Dal.Migrations
{
    public partial class DeleteShow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
               name: "FK_UserNotifications_Notifications_NotificationId",
               table: "UserNotifications");

            migrationBuilder.AddForeignKey(
              name: "FK_UserNotifications_Notifications_NotificationId",
              table: "UserNotifications",
              column: "NotificationId",
              principalTable: "Notifications",
              schema: null,
              principalSchema: null,
              principalColumn: "Id",
              onDelete: ReferentialAction.Cascade
              );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
             name: "FK_UserNotifications_Notifications_NotificationId",
             table: "UserNotifications");

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotifications_Notifications_NotificationId",
                table: "UserNotifications",
                column: "NotificationId",
                principalTable: "Notifications",
                schema: null,
                principalSchema: null,
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction
                );
        }
    }
}
