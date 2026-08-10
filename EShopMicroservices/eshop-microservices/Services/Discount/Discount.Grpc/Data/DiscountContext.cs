using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public class DiscountContext(DbContextOptions<DiscountContext> options):DbContext(options)
    {
    public DbSet<Coupon> coupons {  get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon { Id =1 , Amount=150 , Description="IPhone x",ProductName="IPhone x"},
            new Coupon { Id = 2, Amount = 250, Description = "Samsung 10", ProductName = "Samsung 10" }
            );
        }
    }

