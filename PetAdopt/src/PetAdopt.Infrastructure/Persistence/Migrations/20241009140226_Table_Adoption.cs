using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetAdopt.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Table_Adoption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponsibleId",
                table: "Pets");

            migrationBuilder.AddColumn<Guid>(
                name: "NgoId",
                table: "Pets",
                type: "UniqueIdentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Adoptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    PetId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    AdopterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdoptionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Excluded = table.Column<bool>(type: "bit", nullable: false),
                    Alteration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Inclusion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserAlteration = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserInclusion = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adoptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adoptions_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pets_NgoId",
                table: "Pets",
                column: "NgoId");

            migrationBuilder.CreateIndex(
                name: "IX_Adoptions_PetId",
                table: "Adoptions",
                column: "PetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Ngos_NgoId",
                table: "Pets",
                column: "NgoId",
                principalTable: "Ngos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Ngos_NgoId",
                table: "Pets");

            migrationBuilder.DropTable(
                name: "Adoptions");

            migrationBuilder.DropIndex(
                name: "IX_Pets_NgoId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "NgoId",
                table: "Pets");

            migrationBuilder.AddColumn<Guid>(
                name: "ResponsibleId",
                table: "Pets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
