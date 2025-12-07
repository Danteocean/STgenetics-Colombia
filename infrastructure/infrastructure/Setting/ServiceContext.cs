using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Setting;

public class ServiceContext: DbContext
{
    public ServiceContext(DbContextOptions<ServiceContext> options):base(options)
    {
    }

    public virtual DbSet<Sandwich>? sandwich { get; set; }
    public virtual DbSet<Order>? order { get; set; }

    public virtual DbSet<OrderItem>? order_item { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sandwich>();

        modelBuilder.Entity<Order>()
            .Property(x => x.createdDate).HasColumnName("created_date");
        modelBuilder.Entity<Order>()
            .Property(x => x.updatedDate).HasColumnName("updated_date");
        modelBuilder.Entity<Order>()
            .Property(x => x.discountRuleId).HasColumnName("discount_rule_id");
        modelBuilder.Entity<Order>()
           .Property(x => x.totalPrice).HasColumnName("total_price");
        modelBuilder.Entity<Order>()
          .Property(x => x.isActive).HasColumnName("is_active");
        modelBuilder.Entity<Order>()
         .Property(x => x.sandwichId).HasColumnName("sandwich_id");
        modelBuilder.Entity<Order>()
        .Property(x => x.orderId).HasColumnName("order_id");


        modelBuilder.Entity<OrderItem>()
             .Property(x => x.createdDate).HasColumnName("created_date");
        modelBuilder.Entity<OrderItem>()
            .Property(x => x.updatedDate).HasColumnName("updated_date");
        modelBuilder.Entity<OrderItem>()
             .Property(x => x.orderId).HasColumnName("order_id");
        modelBuilder.Entity<OrderItem>()
            .Property(x => x.extraId).HasColumnName("extra_id");
        modelBuilder.Entity<OrderItem>()
           .Property(x => x.orderItemId).HasColumnName("order_item_id");
    }
}
