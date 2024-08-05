using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurant.Models.Db;

namespace Restaurant.Contexts
{
    public class FFOEContext : IdentityDbContext<AppUser>
    {
        public FFOEContext(DbContextOptions<FFOEContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ComboDetail> ComboDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<CouponType> CouponTypes { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .HasIndex(p => p.Name)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name)
                .IsUnique();

            modelBuilder.Entity<Customer>()
                .HasIndex(p => p.Email)
                .IsUnique();

            modelBuilder.Entity<Customer>()
                .HasIndex(p => p.Phone)
                .IsUnique();


            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Product>()
                .HasMany(p => p.ComboDetails)
                .WithOne()
                .HasForeignKey(cd => cd.ComboId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<ComboDetail>()
                .HasOne(cd => cd.Product)
                .WithMany()
                .HasForeignKey(cd => cd.ProductId)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne()
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<CouponType>()
                .HasMany(cpt => cpt.Coupons)
                .WithOne()
                .HasForeignKey(cp => cp.CouponTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Coupon)
                .WithOne()
                .HasForeignKey<Order>(o => o.CouponId)
                .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<Customer>()
                .HasMany(ctm => ctm.Coupons)
                .WithOne()
                .HasForeignKey(cp => cp.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<Customer>()
                .HasMany(ctm => ctm.Orders)
                .WithOne()
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Product>(p =>
            {
                p.Property(e => e.UnitPrice).HasPrecision(18, 2);
                p.Property(e => e.HardDiscount).HasPrecision(18, 2);
            });

            modelBuilder.Entity<Order>(o =>
            {
                o.Property(e => e.SubTotal).HasPrecision(18, 2);
                o.Property(e => e.Discount).HasPrecision(18, 2);
                o.Property(e => e.Total).HasPrecision(18, 2);
            });

            modelBuilder.Entity<OrderDetail>(od =>
            {
                od.Property(e => e.UnitPrice).HasPrecision(18, 2);
            });

            modelBuilder.Entity<CouponType>(cpt =>
            {
                cpt.Property(e => e.HardValue).HasPrecision(18, 2);
                cpt.Property(e => e.MinOrderSubTotalCondition).HasPrecision(18, 2);
            });
        }
    }
}
