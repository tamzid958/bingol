﻿// <auto-generated />
using System;
using Bingol.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bingol.Migrations
{
    [DbContext(typeof(BingolContext))]
    [Migration("20210126172007_Initial_Auth_Create")]
    partial class Initial_Auth_Create
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Bingol.Areas.Identity.Data.BingolUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserAddress")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UserAddress2")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("UserCity")
                        .HasColumnType("varchar(90)");

                    b.Property<string>("UserCountry")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("UserFax")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("UserFirstName")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("UserIp")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("UserLastName")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("UserPhone")
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime?>("UserRegistrationDate")
                        .HasColumnType("datetime");

                    b.Property<string>("UserState")
                        .HasColumnType("varchar(20)");

                    b.Property<short>("UserType")
                        .HasColumnType("smallint");

                    b.Property<string>("UserZip")
                        .HasColumnType("varchar(12)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Bingol.Models.Option", b =>
                {
                    b.Property<int>("OptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OptionID")
                        .UseIdentityColumn();

                    b.Property<string>("OptionName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("OptionsGroupId")
                        .HasColumnType("int")
                        .HasColumnName("OptionsGroupID");

                    b.HasKey("OptionId");

                    b.HasIndex(new[] { "OptionsGroupId" }, "IX_options_OptionsGroupID");

                    b.ToTable("options", "bingol");
                });

            modelBuilder.Entity("Bingol.Models.Optiongroup", b =>
                {
                    b.Property<int>("OptionGroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OptionGroupID")
                        .UseIdentityColumn();

                    b.Property<string>("OptionGroupName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("OptionGroupId");

                    b.ToTable("optiongroups", "bingol");
                });

            modelBuilder.Entity("Bingol.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OrderID")
                        .UseIdentityColumn();

                    b.Property<float>("OrderAmount")
                        .HasColumnType("real");

                    b.Property<string>("OrderCity")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("OrderCountry")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("OrderDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("OrderEmail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("OrderFax")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("OrderPhone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("OrderShipAddress")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("OrderShipAddress2")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("OrderShipName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<short>("OrderShipped")
                        .HasColumnType("smallint");

                    b.Property<float>("OrderShipping")
                        .HasColumnType("real");

                    b.Property<string>("OrderState")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<float>("OrderTax")
                        .HasColumnType("real");

                    b.Property<string>("OrderTrackingNumber")
                        .HasMaxLength(80)
                        .IsUnicode(false)
                        .HasColumnType("varchar(80)");

                    b.Property<string>("OrderUserId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("OrderUserID");

                    b.Property<string>("OrderZip")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("UserId");

                    b.HasIndex(new[] { "OrderUserId" }, "IX_orders_OrderUserID");

                    b.ToTable("orders", "bingol");
                });

            modelBuilder.Entity("Bingol.Models.Orderdetail", b =>
                {
                    b.Property<int>("DetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("DetailID")
                        .UseIdentityColumn();

                    b.Property<string>("DetailName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.Property<int>("DetailOrderId")
                        .HasColumnType("int")
                        .HasColumnName("DetailOrderID");

                    b.Property<float>("DetailPrice")
                        .HasColumnType("real");

                    b.Property<int>("DetailProductId")
                        .HasColumnType("int")
                        .HasColumnName("DetailProductID");

                    b.Property<int>("DetailQuantity")
                        .HasColumnType("int");

                    b.Property<string>("DetailSku")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("DetailSKU");

                    b.HasKey("DetailId")
                        .HasName("PK_orderdetails_DetailID");

                    b.HasIndex(new[] { "DetailOrderId" }, "IX_orderdetails_DetailOrderID");

                    b.HasIndex(new[] { "DetailProductId" }, "IX_orderdetails_DetailProductID");

                    b.ToTable("orderdetails", "bingol");
                });

            modelBuilder.Entity("Bingol.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ProductID")
                        .UseIdentityColumn();

                    b.Property<string>("ProductCartDesc")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.Property<int?>("ProductCategoryId")
                        .HasColumnType("int")
                        .HasColumnName("ProductCategoryID");

                    b.Property<string>("ProductImage")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<short?>("ProductLive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("ProductLocation")
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("ProductLongDesc")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<float>("ProductPrice")
                        .HasColumnType("real");

                    b.Property<string>("ProductShortDesc")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .IsUnicode(false)
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("ProductSku")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("ProductSKU");

                    b.Property<float?>("ProductStock")
                        .HasColumnType("real");

                    b.Property<string>("ProductThumb")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<short?>("ProductUnlimited")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasDefaultValueSql("((1))");

                    b.Property<DateTime>("ProductUpdateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<float>("ProductWeight")
                        .HasColumnType("real");

                    b.HasKey("ProductId");

                    b.HasIndex(new[] { "ProductCategoryId" }, "IX_products_ProductCategoryID");

                    b.ToTable("products", "bingol");
                });

            modelBuilder.Entity("Bingol.Models.Productcategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CategoryID")
                        .UseIdentityColumn();

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("CategoryId")
                        .HasName("PK_productcategories_CategoryID");

                    b.ToTable("productcategories", "bingol");
                });

            modelBuilder.Entity("Bingol.Models.Productoption", b =>
                {
                    b.Property<long>("ProductOptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ProductOptionID")
                        .UseIdentityColumn();

                    b.Property<int>("OptionGroupId")
                        .HasColumnType("int")
                        .HasColumnName("OptionGroupID");

                    b.Property<int>("OptionId")
                        .HasColumnType("int")
                        .HasColumnName("OptionID");

                    b.Property<double?>("OptionPriceIncrement")
                        .HasColumnType("float");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    b.HasKey("ProductOptionId");

                    b.HasIndex(new[] { "OptionGroupId" }, "IX_productoptions_OptionGroupID");

                    b.HasIndex(new[] { "OptionId" }, "IX_productoptions_OptionID");

                    b.HasIndex(new[] { "ProductId" }, "IX_productoptions_ProductID");

                    b.ToTable("productoptions", "bingol");
                });

            modelBuilder.Entity("Bingol.Models.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .HasColumnType("int")
                        .HasColumnName("ReviewID");

                    b.Property<int>("ReviewProductId")
                        .HasColumnType("int")
                        .HasColumnName("ReviewProductID");

                    b.Property<int?>("ReviewRating")
                        .HasColumnType("int");

                    b.Property<string>("ReviewUserId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ReviewUserID");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ReviewId");

                    b.HasIndex("UserId");

                    b.HasIndex(new[] { "ReviewProductId" }, "IX_reviews_ReviewProductID");

                    b.HasIndex(new[] { "ReviewUserId" }, "IX_reviews_ReviewUserID");

                    b.ToTable("reviews", "bingol");
                });

            modelBuilder.Entity("Bingol.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserID")
                        .UseIdentityColumn();

                    b.Property<string>("UserAddress")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UserAddress2")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("UserCity")
                        .HasMaxLength(90)
                        .IsUnicode(false)
                        .HasColumnType("varchar(90)");

                    b.Property<string>("UserCountry")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("UserEmail")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.Property<short?>("UserEmailVerified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("UserFax")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("UserFirstName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("UserIp")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("UserIP");

                    b.Property<string>("UserLastName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("UserPassword")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("UserPhone")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime?>("UserRegistrationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("UserState")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<short>("UserType")
                        .HasColumnType("smallint");

                    b.Property<string>("UserVerificationCode")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("UserZip")
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("varchar(12)");

                    b.HasKey("UserId");

                    b.ToTable("users", "bingol");
                });

            modelBuilder.Entity("Bingol.Models.Wishlist", b =>
                {
                    b.Property<int>("WishlistId")
                        .HasColumnType("int")
                        .HasColumnName("WishlistID");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<short?>("Wishlist1")
                        .HasColumnType("smallint")
                        .HasColumnName("Wishlist");

                    b.Property<int>("WishlistProductId")
                        .HasColumnType("int")
                        .HasColumnName("WishlistProductID");

                    b.Property<string>("WishlistUserId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("WishlistUserID");

                    b.HasKey("WishlistId");

                    b.HasIndex("UserId");

                    b.HasIndex(new[] { "WishlistProductId" }, "IX_wishlists_WishlistProductID");

                    b.HasIndex(new[] { "WishlistUserId" }, "IX_wishlists_WishlistUserID");

                    b.ToTable("wishlists", "bingol");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Bingol.Models.Option", b =>
                {
                    b.HasOne("Bingol.Models.Optiongroup", "OptionsGroup")
                        .WithMany("Options")
                        .HasForeignKey("OptionsGroupId");

                    b.Navigation("OptionsGroup");
                });

            modelBuilder.Entity("Bingol.Models.Order", b =>
                {
                    b.HasOne("Bingol.Areas.Identity.Data.BingolUser", "OrderUser")
                        .WithMany("Orders")
                        .HasForeignKey("OrderUserId")
                        .HasConstraintName("FK_orders_OrderUserID");

                    b.HasOne("Bingol.Models.User", null)
                        .WithMany("Orders")
                        .HasForeignKey("UserId");

                    b.Navigation("OrderUser");
                });

            modelBuilder.Entity("Bingol.Models.Orderdetail", b =>
                {
                    b.HasOne("Bingol.Models.Order", "DetailOrder")
                        .WithMany("Orderdetails")
                        .HasForeignKey("DetailOrderId")
                        .HasConstraintName("FK_orderdetails_DetailOrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bingol.Models.Product", "DetailProduct")
                        .WithMany("Orderdetails")
                        .HasForeignKey("DetailProductId")
                        .HasConstraintName("FK_orderdetails_DetailProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DetailOrder");

                    b.Navigation("DetailProduct");
                });

            modelBuilder.Entity("Bingol.Models.Product", b =>
                {
                    b.HasOne("Bingol.Models.Productcategory", "ProductCategory")
                        .WithMany("Products")
                        .HasForeignKey("ProductCategoryId")
                        .HasConstraintName("FK_product_ProductCategoryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("ProductCategory");
                });

            modelBuilder.Entity("Bingol.Models.Productoption", b =>
                {
                    b.HasOne("Bingol.Models.Optiongroup", "OptionGroup")
                        .WithMany("Productoptions")
                        .HasForeignKey("OptionGroupId")
                        .HasConstraintName("FK_productoptions_OptionGroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bingol.Models.Option", "Option")
                        .WithMany("Productoptions")
                        .HasForeignKey("OptionId")
                        .HasConstraintName("FK_productoptions_optionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bingol.Models.Product", "Product")
                        .WithMany("Productoptions")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_productoptions_ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Option");

                    b.Navigation("OptionGroup");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Bingol.Models.Review", b =>
                {
                    b.HasOne("Bingol.Models.Product", "ReviewProduct")
                        .WithMany("Reviews")
                        .HasForeignKey("ReviewProductId")
                        .HasConstraintName("FK__reviews__ReviewP__7E37BEF6")
                        .IsRequired();

                    b.HasOne("Bingol.Areas.Identity.Data.BingolUser", "ReviewUser")
                        .WithMany("Reviews")
                        .HasForeignKey("ReviewUserId")
                        .HasConstraintName("FK__reviews__ReviewU__7F2BE32F");

                    b.HasOne("Bingol.Models.User", null)
                        .WithMany("Reviews")
                        .HasForeignKey("UserId");

                    b.Navigation("ReviewProduct");

                    b.Navigation("ReviewUser");
                });

            modelBuilder.Entity("Bingol.Models.Wishlist", b =>
                {
                    b.HasOne("Bingol.Models.User", null)
                        .WithMany("Wishlists")
                        .HasForeignKey("UserId");

                    b.HasOne("Bingol.Models.Product", "WishlistProduct")
                        .WithMany("Wishlists")
                        .HasForeignKey("WishlistProductId")
                        .HasConstraintName("FK__wishlists__Wishl__02084FDA")
                        .IsRequired();

                    b.HasOne("Bingol.Areas.Identity.Data.BingolUser", "WishlistUser")
                        .WithMany("Wishlists")
                        .HasForeignKey("WishlistUserId")
                        .HasConstraintName("FK__wishlists__Wishl__02FC7413");

                    b.Navigation("WishlistProduct");

                    b.Navigation("WishlistUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Bingol.Areas.Identity.Data.BingolUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Bingol.Areas.Identity.Data.BingolUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bingol.Areas.Identity.Data.BingolUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Bingol.Areas.Identity.Data.BingolUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Bingol.Areas.Identity.Data.BingolUser", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Reviews");

                    b.Navigation("Wishlists");
                });

            modelBuilder.Entity("Bingol.Models.Option", b =>
                {
                    b.Navigation("Productoptions");
                });

            modelBuilder.Entity("Bingol.Models.Optiongroup", b =>
                {
                    b.Navigation("Options");

                    b.Navigation("Productoptions");
                });

            modelBuilder.Entity("Bingol.Models.Order", b =>
                {
                    b.Navigation("Orderdetails");
                });

            modelBuilder.Entity("Bingol.Models.Product", b =>
                {
                    b.Navigation("Orderdetails");

                    b.Navigation("Productoptions");

                    b.Navigation("Reviews");

                    b.Navigation("Wishlists");
                });

            modelBuilder.Entity("Bingol.Models.Productcategory", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Bingol.Models.User", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Reviews");

                    b.Navigation("Wishlists");
                });
#pragma warning restore 612, 618
        }
    }
}