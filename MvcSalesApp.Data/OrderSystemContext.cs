using Microsoft.EntityFrameworkCore;
using MvcSalesApp.Domain;

namespace MvcSalesApp.Data
{
  public class OrderSystemContext : DbContext
  {
    public OrderSystemContext(DbContextOptions<OrderSystemContext> options): base(options) {
    }

    public virtual DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
  }
}