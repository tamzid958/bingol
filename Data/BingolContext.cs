using System.Data;
using Bingol.Areas.Identity.Data;
using Bingol.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Bingol.Data
{
    public partial class BingolContext : IdentityDbContext<BingolUser>
    {
        private IDbConnection DbConnection { get; }
        public BingolContext()
        {
        }

        public BingolContext(DbContextOptions<BingolContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Option> Options { get; set; }
        public virtual DbSet<Optiongroup> Optiongroups { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Orderdetail> Orderdetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Productcategory> Productcategories { get; set; }
        public virtual DbSet<Productoption> Productoptions { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<BingolUser> BingolUsers { get; set; }
        public virtual DbSet<Wishlist> Wishlists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DbConnection.ToString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Option>(entity =>
            {
                entity.ToTable("options", "bingol");

                entity.HasIndex(e => e.OptionsGroupId, "IX_options_OptionsGroupID");

                entity.Property(e => e.OptionId).HasColumnName("OptionID");

                entity.Property(e => e.OptionName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OptionsGroupId).HasColumnName("OptionsGroupID");

                entity.HasOne(d => d.OptionsGroup)
                    .WithMany(p => p.Options)
                    .HasForeignKey(d => d.OptionsGroupId);
            });

            modelBuilder.Entity<Optiongroup>(entity =>
            {
                entity.ToTable("optiongroups", "bingol");

                entity.Property(e => e.OptionGroupId).HasColumnName("OptionGroupID");

                entity.Property(e => e.OptionGroupName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders", "bingol");

                entity.HasIndex(e => e.OrderUserId, "IX_orders_OrderUserID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.OrderCity)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderCountry)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OrderEmail)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OrderFax)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OrderPhone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OrderShipAddress)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OrderShipAddress2)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OrderShipName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OrderState)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderTrackingNumber)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.OrderUserId).HasColumnName("OrderUserID");

                entity.Property(e => e.OrderZip)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Orderdetail>(entity =>
            {
                entity.HasKey(e => e.DetailId)
                    .HasName("PK_orderdetails_DetailID");

                entity.ToTable("orderdetails", "bingol");

                entity.HasIndex(e => e.DetailOrderId, "IX_orderdetails_DetailOrderID");

                entity.HasIndex(e => e.DetailProductId, "IX_orderdetails_DetailProductID");

                entity.Property(e => e.DetailId).HasColumnName("DetailID");

                entity.Property(e => e.DetailName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DetailOrderId).HasColumnName("DetailOrderID");

                entity.Property(e => e.DetailProductId).HasColumnName("DetailProductID");

                entity.Property(e => e.DetailSku)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DetailSKU");

                entity.HasOne(d => d.DetailOrder)
                    .WithMany(p => p.Orderdetails)
                    .HasForeignKey(d => d.DetailOrderId)
                    .HasConstraintName("FK_orderdetails_DetailOrderID");

                entity.HasOne(d => d.DetailProduct)
                    .WithMany(p => p.Orderdetails)
                    .HasForeignKey(d => d.DetailProductId)
                    .HasConstraintName("FK_orderdetails_DetailProductID");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products", "bingol");

                entity.HasIndex(e => e.ProductCategoryId, "IX_products_ProductCategoryID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ProductCartDesc)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");

                entity.Property(e => e.ProductImage)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductLive).HasDefaultValueSql("((0))");

                entity.Property(e => e.ProductLocation)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ProductLongDesc)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductShortDesc)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ProductSku)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ProductSKU");

                entity.Property(e => e.ProductThumb)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductUnlimited).HasDefaultValueSql("((1))");

                entity.Property(e => e.ProductUpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductCategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_product_ProductCategoryID");
            });

            modelBuilder.Entity<Productcategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK_productcategories_CategoryID");

                entity.ToTable("productcategories", "bingol");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Productoption>(entity =>
            {
                entity.ToTable("productoptions", "bingol");

                entity.HasIndex(e => e.OptionGroupId, "IX_productoptions_OptionGroupID");

                entity.HasIndex(e => e.OptionId, "IX_productoptions_OptionID");

                entity.HasIndex(e => e.ProductId, "IX_productoptions_ProductID");

                entity.Property(e => e.ProductOptionId).HasColumnName("ProductOptionID");

                entity.Property(e => e.OptionGroupId).HasColumnName("OptionGroupID");

                entity.Property(e => e.OptionId).HasColumnName("OptionID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.OptionGroup)
                    .WithMany(p => p.Productoptions)
                    .HasForeignKey(d => d.OptionGroupId)
                    .HasConstraintName("FK_productoptions_OptionGroupID");

                entity.HasOne(d => d.Option)
                    .WithMany(p => p.Productoptions)
                    .HasForeignKey(d => d.OptionId)
                    .HasConstraintName("FK_productoptions_optionID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Productoptions)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_productoptions_ProductID");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("reviews", "bingol");

                entity.HasIndex(e => e.ReviewProductId, "IX_reviews_ReviewProductID");

                entity.HasIndex(e => e.ReviewUserId, "IX_reviews_ReviewUserID");

                entity.Property(e => e.ReviewId)
                    .ValueGeneratedNever()
                    .HasColumnName("ReviewID");

                entity.Property(e => e.ReviewProductId).HasColumnName("ReviewProductID");

                entity.Property(e => e.ReviewUserId).HasColumnName("ReviewUserID");

                entity.HasOne(d => d.ReviewProduct)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.ReviewProductId)
                    .HasConstraintName("FK__reviews__ReviewP__7E37BEF6");
            });

            modelBuilder.Entity<Wishlist>(entity =>
            {
                entity.ToTable("wishlists", "bingol");

                entity.HasIndex(e => e.WishlistProductId, "IX_wishlists_WishlistProductID");

                entity.HasIndex(e => e.WishlistUserId, "IX_wishlists_WishlistUserID");

                entity.Property(e => e.WishlistId)
                    .ValueGeneratedNever()
                    .HasColumnName("WishlistID");

                entity.Property(e => e.WishlistProductId).HasColumnName("WishlistProductID");

                entity.Property(e => e.WishlistUserId).HasColumnName("WishlistUserID");

                entity.HasOne(d => d.WishlistProduct)
                    .WithMany(p => p.Wishlists)
                    .HasForeignKey(d => d.WishlistProductId)
                    .HasConstraintName("FK__wishlists__Wishl__02084FDA");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
