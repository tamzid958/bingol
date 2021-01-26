using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bingol.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "bingol");

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
                name: "users",
                schema: "bingol",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserEmail = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    UserPassword = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    UserFirstName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    UserLastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    UserCity = table.Column<string>(type: "varchar(90)", unicode: false, maxLength: 90, nullable: true),
                    UserState = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    UserZip = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    UserEmailVerified = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    UserRegistrationDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UserVerificationCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    UserIP = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    UserPhone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    UserFax = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    UserCountry = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    UserAddress = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    UserAddress2 = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    UserType = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserID);
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
                    ProductCartDesc = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    ProductShortDesc = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false),
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
                name: "orders",
                schema: "bingol",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderUserID = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_orders_OrderUserID",
                        column: x => x.OrderUserID,
                        principalSchema: "bingol",
                        principalTable: "users",
                        principalColumn: "UserID",
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
                name: "reviews",
                schema: "bingol",
                columns: table => new
                {
                    ReviewID = table.Column<int>(type: "int", nullable: false),
                    ReviewRating = table.Column<int>(type: "int", nullable: true),
                    ReviewProductID = table.Column<int>(type: "int", nullable: false),
                    ReviewUserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reviews", x => x.ReviewID);
                    table.ForeignKey(
                        name: "FK__reviews__ReviewP__7E37BEF6",
                        column: x => x.ReviewProductID,
                        principalSchema: "bingol",
                        principalTable: "products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__reviews__ReviewU__7F2BE32F",
                        column: x => x.ReviewUserID,
                        principalSchema: "bingol",
                        principalTable: "users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "wishlists",
                schema: "bingol",
                columns: table => new
                {
                    WishlistID = table.Column<int>(type: "int", nullable: false),
                    Wishlist = table.Column<short>(type: "smallint", nullable: true),
                    WishlistProductID = table.Column<int>(type: "int", nullable: false),
                    WishlistUserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wishlists", x => x.WishlistID);
                    table.ForeignKey(
                        name: "FK__wishlists__Wishl__02084FDA",
                        column: x => x.WishlistProductID,
                        principalSchema: "bingol",
                        principalTable: "products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__wishlists__Wishl__02FC7413",
                        column: x => x.WishlistUserID,
                        principalSchema: "bingol",
                        principalTable: "users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
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
                    DetailQuantity = table.Column<int>(type: "int", nullable: false)
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
                name: "orderdetails",
                schema: "bingol");

            migrationBuilder.DropTable(
                name: "productoptions",
                schema: "bingol");

            migrationBuilder.DropTable(
                name: "reviews",
                schema: "bingol");

            migrationBuilder.DropTable(
                name: "wishlists",
                schema: "bingol");

            migrationBuilder.DropTable(
                name: "orders",
                schema: "bingol");

            migrationBuilder.DropTable(
                name: "options",
                schema: "bingol");

            migrationBuilder.DropTable(
                name: "products",
                schema: "bingol");

            migrationBuilder.DropTable(
                name: "users",
                schema: "bingol");

            migrationBuilder.DropTable(
                name: "optiongroups",
                schema: "bingol");

            migrationBuilder.DropTable(
                name: "productcategories",
                schema: "bingol");
        }
    }
}
