namespace Ordering.Infrastructure.Data
    {
    public interface IApplicationDbContext
        {
        DbSet<Customer> Customers { get; }
        DbSet<OrderItem> OrderItems { get; }
        DbSet<Order> Orders { get; }
        DbSet<Product> Products { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        }
    }