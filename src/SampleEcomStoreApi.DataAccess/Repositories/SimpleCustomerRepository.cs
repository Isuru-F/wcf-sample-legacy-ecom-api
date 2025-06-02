using System;
using System.Collections.Generic;
using System.Linq;
using SampleEcomStoreApi.DataAccess.Context;
using SampleEcomStoreApi.DataAccess.Entities;

namespace SampleEcomStoreApi.DataAccess.Repositories
{
    public class SimpleCustomerRepository : ICustomerRepository
    {
        public List<Customer> GetAll()
        {
            using (var context = new EcommerceDbContext())
            {
                return context.Customers.Where(c => c.IsActive).ToList();
            }
        }

        public Customer GetById(int customerId)
        {
            using (var context = new EcommerceDbContext())
            {
                return context.Customers.FirstOrDefault(c => c.CustomerId == customerId && c.IsActive);
            }
        }

        public Customer GetByEmail(string email)
        {
            using (var context = new EcommerceDbContext())
            {
                return context.Customers.FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && c.IsActive);
            }
        }

        public int Create(Customer customer)
        {
            using (var context = new EcommerceDbContext())
            {
                customer.CreatedDate = DateTime.Now;
                customer.ModifiedDate = DateTime.Now;
                customer.IsActive = true;

                context.Customers.Add(customer);
                context.SaveChanges();
                return customer.CustomerId;
            }
        }

        public bool Update(Customer customer)
        {
            using (var context = new EcommerceDbContext())
            {
                var existingCustomer = context.Customers.FirstOrDefault(c => c.CustomerId == customer.CustomerId);
                if (existingCustomer == null) return false;

                existingCustomer.FirstName = customer.FirstName;
                existingCustomer.LastName = customer.LastName;
                existingCustomer.Email = customer.Email;
                existingCustomer.Phone = customer.Phone;
                existingCustomer.Address = customer.Address;
                existingCustomer.City = customer.City;
                existingCustomer.State = customer.State;
                existingCustomer.ZipCode = customer.ZipCode;
                existingCustomer.Country = customer.Country;
                existingCustomer.ModifiedDate = DateTime.Now;

                context.SaveChanges();
                return true;
            }
        }

        public bool Delete(int customerId)
        {
            using (var context = new EcommerceDbContext())
            {
                var customer = context.Customers.FirstOrDefault(c => c.CustomerId == customerId);
                if (customer == null) return false;

                context.Customers.Remove(customer);
                context.SaveChanges();
                return true;
            }
        }

        public bool Deactivate(int customerId)
        {
            using (var context = new EcommerceDbContext())
            {
                var customer = context.Customers.FirstOrDefault(c => c.CustomerId == customerId);
                if (customer == null) return false;

                customer.IsActive = false;
                customer.ModifiedDate = DateTime.Now;

                context.SaveChanges();
                return true;
            }
        }
    }
}
