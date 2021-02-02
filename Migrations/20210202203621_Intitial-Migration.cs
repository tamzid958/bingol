using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bingol.Migrations
{
    public partial class IntitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "bingol");

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
                    UserFax = table.Column<string>(type: "varchar(50)", nullable: true),
                    UserCountry = table.Column<string>(type: "varchar(20)", nullable: true),
                    UserAddress = table.Column<string>(type: "varchar(100)", nullable: true),
                    UserAddress2 = table.Column<string>(type: "varchar(50)", nullable: true),
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
                name: "optiongroups",
                schema: "bingol",
                columns: table => new
                {
                    OptionGroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionGroupName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_optiongroups", x => x.OptionGroupID);
                });

            migrationBuilder.CreateTable(
                name: "productcategories",
                schema: "bingol",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productcategories_CategoryID", x => x.CategoryID);
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

            migrationBuilder.CreateTable(
                name: "orders",
                schema: "bingol",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OrderAmount = table.Column<float>(type: "real", nullable: false),
                    OrderShipName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    OrderShipAddress = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    OrderShipAddress2 = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    OrderCity = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    OrderState = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    OrderZip = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    OrderCountry = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    OrderPhone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    OrderFax = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    OrderShipping = table.Column<float>(type: "real", nullable: false),
                    OrderTax = table.Column<float>(type: "real", nullable: false),
                    OrderEmail = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    OrderShipped = table.Column<short>(type: "smallint", nullable: false),
                    OrderTrackingNumber = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_orders_AspNetUsers_OrderUserID",
                        column: x => x.OrderUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "options",
                schema: "bingol",
                columns: table => new
                {
                    OptionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    OptionsGroupID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_options", x => x.OptionID);
                    table.ForeignKey(
                        name: "FK_options_optiongroups_OptionsGroupID",
                        column: x => x.OptionsGroupID,
                        principalSchema: "bingol",
                        principalTable: "optiongroups",
                        principalColumn: "OptionGroupID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "products",
                schema: "bingol",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductSKU = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ProductName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ProductPrice = table.Column<float>(type: "real", nullable: false),
                    ProductWeight = table.Column<float>(type: "real", nullable: false),
                    ProductCartDesc = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    ProductShortDesc = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    ProductLongDesc = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    ProductThumb = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ProductImage = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ProductCategoryID = table.Column<int>(type: "int", nullable: true),
                    ProductUpdateDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ProductStock = table.Column<float>(type: "real", nullable: true),
                    ProductLive = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    ProductUnlimited = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((1))"),
                    ProductLocation = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_product_ProductCategoryID",
                        column: x => x.ProductCategoryID,
                        principalSchema: "bingol",
                        principalTable: "productcategories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orderdetails",
                schema: "bingol",
                columns: table => new
                {
                    DetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DetailOrderID = table.Column<int>(type: "int", nullable: false),
                    DetailProductID = table.Column<int>(type: "int", nullable: false),
                    DetailName = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    DetailPrice = table.Column<float>(type: "real", nullable: false),
                    DetailSKU = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DetailQuantity = table.Column<int>(type: "int", nullable: false),
                    Reviewed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderdetails_DetailID", x => x.DetailID);
                    table.ForeignKey(
                        name: "FK_orderdetails_DetailOrderID",
                        column: x => x.DetailOrderID,
                        principalSchema: "bingol",
                        principalTable: "orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orderdetails_DetailProductID",
                        column: x => x.DetailProductID,
                        principalSchema: "bingol",
                        principalTable: "products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productoptions",
                schema: "bingol",
                columns: table => new
                {
                    ProductOptionID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    OptionID = table.Column<int>(type: "int", nullable: false),
                    OptionPriceIncrement = table.Column<double>(type: "float", nullable: true),
                    OptionGroupID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productoptions", x => x.ProductOptionID);
                    table.ForeignKey(
                        name: "FK_productoptions_OptionGroupID",
                        column: x => x.OptionGroupID,
                        principalSchema: "bingol",
                        principalTable: "optiongroups",
                        principalColumn: "OptionGroupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productoptions_optionID",
                        column: x => x.OptionID,
                        principalSchema: "bingol",
                        principalTable: "options",
                        principalColumn: "OptionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productoptions_ProductID",
                        column: x => x.ProductID,
                        principalSchema: "bingol",
                        principalTable: "products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TempCarts",
                columns: table => new
                {
                    TempCartID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempCarts", x => x.TempCartID);
                    table.ForeignKey(
                        name: "FK_TempCarts_products_ProductID",
                        column: x => x.ProductID,
                        principalSchema: "bingol",
                        principalTable: "products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "wishlists",
                schema: "bingol",
                columns: table => new
                {
                    WishlistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WishlistCondition = table.Column<short>(type: "smallint", nullable: true),
                    WishlistProductID = table.Column<int>(type: "int", nullable: false),
                    WishlistUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BingolUser = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wishlists", x => x.WishlistId);
                    table.ForeignKey(
                        name: "FK__wishlists__Wishl__02084FDA",
                        column: x => x.WishlistProductID,
                        principalSchema: "bingol",
                        principalTable: "products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_wishlists_AspNetUsers_WishlistUserID",
                        column: x => x.WishlistUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                schema: "bingol",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewRating = table.Column<int>(type: "int", nullable: true),
                    ReviewProductID = table.Column<int>(type: "int", nullable: false),
                    ReviewOrderId = table.Column<int>(type: "int", nullable: false),
                    ReviewUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReviewOrderdDeatilDetailId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK__reviews__ReviewP__7E37BEF6",
                        column: x => x.ReviewProductID,
                        principalSchema: "bingol",
                        principalTable: "products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reviews_AspNetUsers_ReviewUserID",
                        column: x => x.ReviewUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_reviews_orderdetails_ReviewOrderdDeatilDetailId",
                        column: x => x.ReviewOrderdDeatilDetailId,
                        principalSchema: "bingol",
                        principalTable: "orderdetails",
                        principalColumn: "DetailID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "bingol",
                table: "optiongroups",
                columns: new[] { "OptionGroupID", "OptionGroupName" },
                values: new object[] { 1, "color" });

            migrationBuilder.InsertData(
                schema: "bingol",
                table: "optiongroups",
                columns: new[] { "OptionGroupID", "OptionGroupName" },
                values: new object[] { 2, "size" });

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

            migrationBuilder.CreateIndex(
                name: "IX_options_OptionsGroupID",
                schema: "bingol",
                table: "options",
                column: "OptionsGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_orderdetails_DetailOrderID",
                schema: "bingol",
                table: "orderdetails",
                column: "DetailOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_orderdetails_DetailProductID",
                schema: "bingol",
                table: "orderdetails",
                column: "DetailProductID");

            migrationBuilder.CreateIndex(
                name: "IX_orders_OrderUserID",
                schema: "bingol",
                table: "orders",
                column: "OrderUserID");

            migrationBuilder.CreateIndex(
                name: "IX_productoptions_OptionGroupID",
                schema: "bingol",
                table: "productoptions",
                column: "OptionGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_productoptions_OptionID",
                schema: "bingol",
                table: "productoptions",
                column: "OptionID");

            migrationBuilder.CreateIndex(
                name: "IX_productoptions_ProductID",
                schema: "bingol",
                table: "productoptions",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_products_ProductCategoryID",
                schema: "bingol",
                table: "products",
                column: "ProductCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_ReviewOrderdDeatilDetailId",
                schema: "bingol",
                table: "reviews",
                column: "ReviewOrderdDeatilDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_ReviewProductID",
                schema: "bingol",
                table: "reviews",
                column: "ReviewProductID");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_ReviewUserID",
                schema: "bingol",
                table: "reviews",
                column: "ReviewUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TempCarts_ProductID",
                table: "TempCarts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_wishlists_WishlistProductID",
                schema: "bingol",
                table: "wishlists",
                column: "WishlistProductID");

            migrationBuilder.CreateIndex(
                name: "IX_wishlists_WishlistUserID",
                schema: "bingol",
                table: "wishlists",
                column: "WishlistUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "productoptions",
                schema: "bingol");

            migrationBuilder.DropTable(
                name: "reviews",
                schema: "bingol");

            migrationBuilder.DropTable(
                name: "TempCarts");

            migrationBuilder.DropTable(
                name: "wishlists",
                schema: "bingol");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "options",
                schema: "bingol");

            migrationBuilder.DropTable(
                name: "orderdetails",
                schema: "bingol");

            migrationBuilder.DropTable(
                name: "optiongroups",
                schema: "bingol");

            migrationBuilder.DropTable(
                name: "orders",
                schema: "bingol");

            migrationBuilder.DropTable(
                name: "products",
                schema: "bingol");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "productcategories",
                schema: "bingol");
        }
    }
}
