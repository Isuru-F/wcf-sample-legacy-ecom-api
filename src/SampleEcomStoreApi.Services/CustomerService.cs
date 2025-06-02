using System.Collections.Generic;
using System.ServiceModel;
using System.Linq;
using SampleEcomStoreApi.Contracts.ServiceContracts;
using SampleEcomStoreApi.Contracts.DataContracts;
using SampleEcomStoreApi.DataAccess.Context;
using SampleEcomStoreApi.DataAccess.Entities;
using System;

namespace SampleEcomStoreApi.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CustomerService : ICustomerService
    {
        public List<CustomerDto> GetAllCustomers()
        {
            using (var context = new EcommerceDbContext())
            {
                var customers = context.Customers.Where(c => c.IsActive).ToList();
                return customers.Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    Phone = c.Phone,
                    Address = c.Address,
                    City = c.City,
                    State = c.State,
                    ZipCode = c.ZipCode,
                    Country = c.Country,
                    CreatedDate = c.CreatedDate,
                    ModifiedDate = c.ModifiedDate,
                    IsActive = c.IsActive
                }).ToList();
            }
        }

        public CustomerDto GetCustomerById(int customerId)
        {
            using (var context = new EcommerceDbContext())
            {
                var customer = context.Customers.FirstOrDefault(c => c.CustomerId == customerId && c.IsActive);
                if (customer == null)
                    return new CustomerDto();

                return new CustomerDto
                {
                    CustomerId = customer.CustomerId,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    Address = customer.Address,
                    City = customer.City,
                    State = customer.State,
                    ZipCode = customer.ZipCode,
                    Country = customer.Country,
                    CreatedDate = customer.CreatedDate,
                    ModifiedDate = customer.ModifiedDate,
                    IsActive = customer.IsActive
                };
            }
        }

        public CustomerDto GetCustomerByEmail(string email)
        {
            using (var context = new EcommerceDbContext())
            {
                var customer = context.Customers.FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && c.IsActive);
                if (customer == null)
                    return new CustomerDto();

                return new CustomerDto
                {
                    CustomerId = customer.CustomerId,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    Address = customer.Address,
                    City = customer.City,
                    State = customer.State,
                    ZipCode = customer.ZipCode,
                    Country = customer.Country,
                    CreatedDate = customer.CreatedDate,
                    ModifiedDate = customer.ModifiedDate,
                    IsActive = customer.IsActive
                };
            }
        }

        public int CreateCustomer(CustomerDto customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            using (var context = new EcommerceDbContext())
            {
                var customerEntity = new Customer
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    Address = customer.Address,
                    City = customer.City,
                    State = customer.State,
                    ZipCode = customer.ZipCode,
                    Country = customer.Country,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsActive = true
                };

                context.Customers.Add(customerEntity);
                context.SaveChanges();
                return customerEntity.CustomerId;
            }
        }

        public bool UpdateCustomer(CustomerDto customerDto)
        {
            using (var context = new EcommerceDbContext())
            {
                var existingCustomer = context.Customers.Find(customerDto.CustomerId);
                if (existingCustomer == null) return false;

                existingCustomer.FirstName = customerDto.FirstName;
                existingCustomer.LastName = customerDto.LastName;
                existingCustomer.Email = customerDto.Email;
                existingCustomer.Phone = customerDto.Phone;
                existingCustomer.Address = customerDto.Address;
                existingCustomer.City = customerDto.City;
                existingCustomer.State = customerDto.State;
                existingCustomer.ZipCode = customerDto.ZipCode;
                existingCustomer.Country = customerDto.Country;
                existingCustomer.ModifiedDate = DateTime.Now;

                return context.SaveChanges() > 0;
            }
        }

        public bool DeleteCustomer(int customerId)
        {
            using (var context = new EcommerceDbContext())
            {
                var customer = context.Customers.Find(customerId);
                if (customer == null) return false;

                context.Customers.Remove(customer);
                return context.SaveChanges() > 0;
            }
        }

        public bool DeactivateCustomer(int customerId)
        {
            using (var context = new EcommerceDbContext())
            {
                var customer = context.Customers.Find(customerId);
                if (customer == null) return false;

                customer.IsActive = false;
                customer.ModifiedDate = DateTime.Now;

                return context.SaveChanges() > 0;
            }
        }
    }
}
