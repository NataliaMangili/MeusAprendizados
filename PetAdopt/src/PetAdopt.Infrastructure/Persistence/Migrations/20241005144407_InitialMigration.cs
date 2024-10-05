using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetAdopt.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ngos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    MainResponsibleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MainResponsibleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Apresentation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    History = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Excluded = table.Column<bool>(type: "bit", nullable: false),
                    Alteration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Inclusion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserAlteration = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserInclusion = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ngos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Species = table.Column<int>(type: "int", nullable: false),
                    Breed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: true),
                    IsNeutered = table.Column<bool>(type: "bit", nullable: false),
                    HasSpecialNeeds = table.Column<bool>(type: "bit", nullable: false),
                    StatusPet = table.Column<int>(type: "int", nullable: false),
                    ResponsibleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Excluded = table.Column<bool>(type: "bit", nullable: false),
                    Alteration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Inclusion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserAlteration = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserInclusion = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NgoAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    NgoId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    addressVO_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    addressVO_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    addressVO_State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    addressVO_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Excluded = table.Column<bool>(type: "bit", nullable: false),
                    Alteration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Inclusion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserAlteration = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserInclusion = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NgoAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NgoAddresses_Ngos_NgoId",
                        column: x => x.NgoId,
                        principalTable: "Ngos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VolunteersContacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    NgoId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    contactVO_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contactVO_Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contactVO_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Excluded = table.Column<bool>(type: "bit", nullable: false),
                    Alteration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Inclusion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserAlteration = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserInclusion = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteersContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteersContacts_Ngos_NgoId",
                        column: x => x.NgoId,
                        principalTable: "Ngos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdoptionForms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    PetId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    AdopterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdopterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    addressVO_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    addressVO_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    addressVO_State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    addressVO_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdopterContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReasonForAdoption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdopterHouseholdSize = table.Column<int>(type: "int", nullable: false),
                    AdopterHasOtherPets = table.Column<bool>(type: "bit", nullable: false),
                    FormStatus = table.Column<int>(type: "int", nullable: false),
                    Excluded = table.Column<bool>(type: "bit", nullable: false),
                    Alteration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Inclusion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserAlteration = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserInclusion = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdoptionForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdoptionForms_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PetImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PetId = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    Excluded = table.Column<bool>(type: "bit", nullable: false),
                    Alteration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Inclusion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserAlteration = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserInclusion = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PetImages_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id", 
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PetId = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    Excluded = table.Column<bool>(type: "bit", nullable: false),
                    Alteration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Inclusion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserAlteration = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserInclusion = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionForms_PetId",
                table: "AdoptionForms",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_NgoAddresses_NgoId",
                table: "NgoAddresses",
                column: "NgoId");

            migrationBuilder.CreateIndex(
                name: "IX_PetImages_PetId",
                table: "PetImages",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_PetId",
                table: "Tags",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteersContacts_NgoId",
                table: "VolunteersContacts",
                column: "NgoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdoptionForms");

            migrationBuilder.DropTable(
                name: "NgoAddresses");

            migrationBuilder.DropTable(
                name: "PetImages");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "VolunteersContacts");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Ngos");
        }
    }
}
