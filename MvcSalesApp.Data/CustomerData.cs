using Microsoft.EntityFrameworkCore;
using MvcSalesApp.Domain;
using SharedKernel.Data;
using System.Collections.Generic;
using System.Linq;

namespace MvcSalesApp.Data
{
    public class CustomerData
    {
        private readonly OrderSystemContext _context;

        public CustomerData(OrderSystemContext context)
        {
            _context = context;
        }

        public List<Customer> GetAllCustomers()
        {
            return _context.Customers.AsNoTracking().ToList();
        }

        public Customer FindCustomer(int? id)
        {
            return _context.Customers
              .AsNoTracking()
              .SingleOrDefault(c => c.CustomerId == id);
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void UpdateCustomer(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void RemoveCustomer(int id)
        {
            _context.Customers.Remove(_context.Customers.Find(id));
            _context.SaveChanges();
        }
    }

    public class UOW
    {
        OrderSystemContext _context;
        public UOW(OrderSystemContext context)
        {
            _context = context;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }

    public class UOWWrappingGenericRepos
    {
        private GenericRepository<Customer> _custRepo;
        private GenericRepository<Order> _orderRepo;
        OrderSystemContext _context;

        public UOWWrappingGenericRepos(OrderSystemContext context)
        {
            _context = context;
        }
        public GenericRepository<Customer> CustomerRepository
        {
            get
            {
                if (_custRepo == null)
                {
                    _custRepo = new GenericRepository<Customer>(_context);
                }
                return _custRepo;
            }
        }

        public GenericRepository<Order> OrderRepository
        {
            get
            {
                if (this._orderRepo == null)
                {
                    this._orderRepo = new GenericRepository<Order>(_context);
                }
                return _orderRepo;
            }
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
