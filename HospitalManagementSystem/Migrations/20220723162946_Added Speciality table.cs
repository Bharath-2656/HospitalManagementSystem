using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalManagementSystem.Migrations
{
    public partial class AddedSpecialitytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Speciality",
                table: "Doctors");

            migrationBuilder.AddColumn<string>(
                name: "specialityName",
                table: "Doctors",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "specialityName",
                table: "Appoinments",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Speciality",
                columns: table => new
                {
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speciality", x => x.Name);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_specialityName",
                table: "Doctors",
                column: "specialityName");

            migrationBuilder.CreateIndex(
                name: "IX_Appoinments_specialityName",
                table: "Appoinments",
                column: "specialityName");

            migrationBuilder.AddForeignKey(
                name: "FK_Appoinments_Speciality_specialityName",
                table: "Appoinments",
                column: "specialityName",
                principalTable: "Speciality",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Speciality_specialityName",
                table: "Doctors",
                column: "specialityName",
                principalTable: "Speciality",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appoinments_Speciality_specialityName",
                table: "Appoinments");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Speciality_specialityName",
                table: "Doctors");

            migrationBuilder.DropTable(
                name: "Speciality");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_specialityName",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Appoinments_specialityName",
                table: "Appoinments");

            migrationBuilder.DropColumn(
                name: "specialityName",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "specialityName",
                table: "Appoinments");

            migrationBuilder.AddColumn<string>(
                name: "Speciality",
                table: "Doctors",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
