using Microsoft.EntityFrameworkCore;

namespace Services.Order.Infrastructure
{
    public class OrderDbContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "ordering";
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {

        }

        public DbSet<Domain.OrderAggregate.Order> Orders { get; set; }
        public DbSet<Domain.OrderAggregate.OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var order = modelBuilder.Entity<Domain.OrderAggregate.Order>().ToTable("Orders", DEFAULT_SCHEMA);
            order.OwnsOne(x => x.Address).WithOwner();

            var orderItem = modelBuilder.Entity<Domain.OrderAggregate.OrderItem>().ToTable("OrderItems", DEFAULT_SCHEMA);
            orderItem.Property(x => x.Price).HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
