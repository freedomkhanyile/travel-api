using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Travel.Data.Access.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateUserId = table.Column<string>(maxLength: 256, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifyUserId = table.Column<string>(maxLength: 256, nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    AddressLine1 = table.Column<string>(nullable: true),
                    AddressLine2 = table.Column<string>(nullable: true),
                    AddressLine3 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    OtherId = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateUserId = table.Column<string>(maxLength: 256, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifyUserId = table.Column<string>(maxLength: 256, nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExperienceCatergory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateUserId = table.Column<string>(maxLength: 256, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifyUserId = table.Column<string>(maxLength: 256, nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    CategoryEntityId = table.Column<Guid>(nullable: false),
                    ExperienceEntityId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienceCatergory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateUserId = table.Column<string>(maxLength: 256, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifyUserId = table.Column<string>(maxLength: 256, nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceOnSpecial = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OnSpecialStartDate = table.Column<DateTime>(nullable: false),
                    OnSpecialEndDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateUserId = table.Column<string>(maxLength: 256, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifyUserId = table.Column<string>(maxLength: 256, nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OtherId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateUserId = table.Column<string>(maxLength: 256, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifyUserId = table.Column<string>(maxLength: 256, nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    RoleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateUserId = table.Column<string>(maxLength: 256, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifyUserId = table.Column<string>(maxLength: 256, nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateUserId = table.Column<string>(maxLength: 256, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifyUserId = table.Column<string>(maxLength: 256, nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserToken = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatergoryImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateUserId = table.Column<string>(maxLength: 256, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifyUserId = table.Column<string>(maxLength: 256, nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CategoryEntityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatergoryImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatergoryImages_Category_CategoryEntityId",
                        column: x => x.CategoryEntityId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExperienceImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateUserId = table.Column<string>(maxLength: 256, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifyUserId = table.Column<string>(maxLength: 256, nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ExperienceEntityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienceImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperienceImages_Experiences_ExperienceEntityId",
                        column: x => x.ExperienceEntityId,
                        principalTable: "Experiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateUserId = table.Column<string>(maxLength: 256, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifyUserId = table.Column<string>(maxLength: 256, nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    BookingStatusCode = table.Column<string>(nullable: true),
                    DateOfBooking = table.Column<DateTime>(nullable: false),
                    SelfBooked = table.Column<bool>(nullable: false),
                    OtherBookingDetails = table.Column<string>(nullable: true),
                    UserEntityId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_UserEntityId",
                        column: x => x.UserEntityId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateUserId = table.Column<string>(maxLength: 256, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifyUserId = table.Column<string>(maxLength: 256, nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    UserEntityId = table.Column<Guid>(nullable: false),
                    RoleEntityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleEntityId",
                        column: x => x.RoleEntityId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserEntityId",
                        column: x => x.UserEntityId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "CreateUserId", "ModifyDate", "ModifyUserId", "RoleName", "StatusId" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 12, 8, 15, 1, 50, 944, DateTimeKind.Local).AddTicks(1333), "sys", new DateTime(2020, 12, 8, 15, 1, 50, 946, DateTimeKind.Local).AddTicks(4286), "sys", "Admin", 1 },
                    { 2, new DateTime(2020, 12, 8, 15, 1, 50, 946, DateTimeKind.Local).AddTicks(6585), "sys", new DateTime(2020, 12, 8, 15, 1, 50, 946, DateTimeKind.Local).AddTicks(6617), "sys", "Staff", 1 },
                    { 3, new DateTime(2020, 12, 8, 15, 1, 50, 946, DateTimeKind.Local).AddTicks(6646), "sys", new DateTime(2020, 12, 8, 15, 1, 50, 946, DateTimeKind.Local).AddTicks(6647), "sys", "Traveler", 1 },
                    { 4, new DateTime(2020, 12, 8, 15, 1, 50, 946, DateTimeKind.Local).AddTicks(6650), "sys", new DateTime(2020, 12, 8, 15, 1, 50, 946, DateTimeKind.Local).AddTicks(6651), "sys", "Supplier", 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreateUserId", "Email", "FirstName", "IsDeleted", "ModifyDate", "ModifyUserId", "Password", "PhoneNumber", "StatusId", "Surname", "UserToken" },
                values: new object[] { new Guid("c6e641f7-ce8d-4de5-aef7-bc4237251335"), new DateTime(2020, 12, 8, 15, 1, 51, 160, DateTimeKind.Local).AddTicks(3947), "sys", "admin@mail.com", "john", false, new DateTime(2020, 12, 8, 15, 1, 51, 160, DateTimeKind.Local).AddTicks(5704), "sys", "$2a$11$TX.aIViATPUeRVE6MHJqWu9HgZc9bq92VG.BC6x7Ool8k/fCU0K3W", null, 1, "Doe", null });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreateDate", "CreateUserId", "ModifyDate", "ModifyUserId", "RoleEntityId", "StatusId", "UserEntityId" },
                values: new object[] { 1, new DateTime(2020, 12, 8, 15, 1, 51, 161, DateTimeKind.Local).AddTicks(4436), "sys", new DateTime(2020, 12, 8, 15, 1, 51, 161, DateTimeKind.Local).AddTicks(4502), "sys", 1, 1, new Guid("c6e641f7-ce8d-4de5-aef7-bc4237251335") });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserEntityId",
                table: "Bookings",
                column: "UserEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CatergoryImages_CategoryEntityId",
                table: "CatergoryImages",
                column: "CategoryEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceImages_ExperienceEntityId",
                table: "ExperienceImages",
                column: "ExperienceEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleEntityId",
                table: "UserRoles",
                column: "RoleEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserEntityId",
                table: "UserRoles",
                column: "UserEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "CatergoryImages");

            migrationBuilder.DropTable(
                name: "ExperienceCatergory");

            migrationBuilder.DropTable(
                name: "ExperienceImages");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
