using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bingol.Migrations
{
    public partial class Initial_Auth_Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_OrderUserID",
                schema: "bingol",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK__reviews__ReviewU__7F2BE32F",
                schema: "bingol",
                table: "reviews");

            migrationBuilder.DropForeignKey(
                name: "FK__wishlists__Wishl__02FC7413",
                schema: "bingol",
                table: "wishlists");

            migrationBuilder.AlterColumn<string>(
                name: "WishlistUserID",
                schema: "bingol",
                table: "wishlists",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                schema: "bingol",
                table: "wishlists",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReviewUserID",
                schema: "bingol",
                table: "reviews",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                schema: "bingol",
                table: "reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OrderUserID",
                schema: "bingol",
                table: "orders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                schema: "bingol",
                table: "orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserFirstName = table.Column<string>(type: "varchar(50)", nullable: true),
                    UserLastName = table.Column<string>(type: "varchar(50)", nullable: true),
                    UserCity = table.Column<string>(type: "varchar(90)", nullable: true),
                    UserState = table.Column<string>(type: "varchar(20)", nullable: true),
                    UserZip = table.Column<string>(type: "varchar(12)", nullable: true),
                    UserRegistrationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UserIp = table.Column<string>(type: "varchar(50)", nullable: true),
                    UserPhone = table.Column<string>(type: "varchar(20)", nullable: true),
                    UserFax = table.Column<string>(type: "varchar(50)", nullable: true),
                    UserCountry = table.Column<string>(type: "varchar(20)", nullable: true),
                    UserAddress = table.Column<string>(type: "varchar(100)", nullable: true),
                    UserAddress2 = table.Column<string>(type: "varchar(50)", nullable: true),
                    UserType = table.Column<short>(type: "smallint", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_wishlists_UserId",
                schema: "bingol",
                table: "wishlists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_UserId",
                schema: "bingol",
                table: "reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_UserId",
                schema: "bingol",
                table: "orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_OrderUserID",
                schema: "bingol",
                table: "orders",
                column: "OrderUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_users_UserId",
                schema: "bingol",
                table: "orders",
                column: "UserId",
                principalSchema: "bingol",
                principalTable: "users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__reviews__ReviewU__7F2BE32F",
                schema: "bingol",
                table: "reviews",
                column: "ReviewUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_users_UserId",
                schema: "bingol",
                table: "reviews",
                column: "UserId",
                principalSchema: "bingol",
                principalTable: "users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__wishlists__Wishl__02FC7413",
                schema: "bingol",
                table: "wishlists",
                column: "WishlistUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_wishlists_users_UserId",
                schema: "bingol",
                table: "wishlists",
                column: "UserId",
                principalSchema: "bingol",
                principalTable: "users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_OrderUserID",
                schema: "bingol",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_users_UserId",
                schema: "bingol",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK__reviews__ReviewU__7F2BE32F",
                schema: "bingol",
                table: "reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_reviews_users_UserId",
                schema: "bingol",
                table: "reviews");

            migrationBuilder.DropForeignKey(
                name: "FK__wishlists__Wishl__02FC7413",
                schema: "bingol",
                table: "wishlists");

            migrationBuilder.DropForeignKey(
                name: "FK_wishlists_users_UserId",
                schema: "bingol",
                table: "wishlists");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_wishlists_UserId",
                schema: "bingol",
                table: "wishlists");

            migrationBuilder.DropIndex(
                name: "IX_reviews_UserId",
                schema: "bingol",
                table: "reviews");

            migrationBuilder.DropIndex(
                name: "IX_orders_UserId",
                schema: "bingol",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "bingol",
                table: "wishlists");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "bingol",
                table: "reviews");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "bingol",
                table: "orders");

            migrationBuilder.AlterColumn<int>(
                name: "WishlistUserID",
                schema: "bingol",
                table: "wishlists",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReviewUserID",
                schema: "bingol",
                table: "reviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrderUserID",
                schema: "bingol",
                table: "orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_OrderUserID",
                schema: "bingol",
                table: "orders",
                column: "OrderUserID",
                principalSchema: "bingol",
                principalTable: "users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__reviews__ReviewU__7F2BE32F",
                schema: "bingol",
                table: "reviews",
                column: "ReviewUserID",
                principalSchema: "bingol",
                principalTable: "users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__wishlists__Wishl__02FC7413",
                schema: "bingol",
                table: "wishlists",
                column: "WishlistUserID",
                principalSchema: "bingol",
                principalTable: "users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
