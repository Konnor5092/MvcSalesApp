using Microsoft.EntityFrameworkCore;
using MvcSalesApp.Domain;
using System.Collections.Generic;
using System.Linq;

namespace MvcSalesApp.Data
{
    public class ProductData
    {
        private readonly OrderSystemContext _context;

        public ProductData(OrderSystemContext context)
        {
            _context = context;
        }

        public List<Product> GetAllCustomers()
        {
            return _context.Products.OrderBy(p => p.Name).ToList();
        }

        public Product FindProduct(int? id)
        {
            return _context.Products.SingleOrDefault(p => p.ProductId == id);
        }

        public void AddProduct(Product product)
        {
            product.IsAvailable = true;
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void RemoveProduct(int id)
        {
			using (var transaction = _context.Database.BeginTransaction())
    		{
				_context.Database.ExecuteSqlInterpolated($"update products set isavailable=0 where productid={id}");
			}
        }
    }
}
