using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class RoleRepair2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_UserRoleRoleId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "UserRoleRoleId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_UserRoleRoleId",
                table: "Users",
                column: "UserRoleRoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_UserRoleRoleId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "UserRoleRoleId",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_UserRoleRoleId",
                table: "Users",
                column: "UserRoleRoleId",
                principalTable: "Roles",
                principalColumn: "RoleId");
        }
    }
}
