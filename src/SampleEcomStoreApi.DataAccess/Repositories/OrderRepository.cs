using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using SampleEcomStoreApi.DataAccess.Context;
using SampleEcomStoreApi.DataAccess.Entities;
using SampleEcomStoreApi.Common.Constants;

namespace SampleEcomStoreApi.DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public List<Order> GetAll()
        {
            using (var context = new EcommerceDbContext())
            {
                return context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.OrderItems.Select(oi => oi.Product))
                    .ToList();
            }
        }

        public Order GetById(int orderId)
        {
            using (var context = new EcommerceDbContext())
            {
                return context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.OrderItems.Select(oi => oi.Product))
                    .FirstOrDefault(o => o.OrderId == orderId);
            }
        }

        public List<Order> GetByCustomerId(int customerId)
        {
            using (var context = new EcommerceDbContext())
            {
                return context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.OrderItems.Select(oi => oi.Product))
                    .Where(o => o.CustomerId == customerId)
                    .OrderByDescending(o => o.OrderDate)
                    .ToList();
            }
        }

        public List<Order> GetByStatus(string status)
        {
            using (var context = new EcommerceDbContext())
            {
                return context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.OrderItems.Select(oi => oi.Product))
                    .Where(o => o.Status.Equals(status, StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(o => o.OrderDate)
                    .ToList();
            }
        }

        public int Create(Order order)
        {
            using (var context = new EcommerceDbContext())
            {
                order.OrderDate = DateTime.Now;
                order.CreatedDate = DateTime.Now;
                order.ModifiedDate = DateTime.Now;

                if (string.IsNullOrEmpty(order.Status))
                {
                    order.Status = OrderStatus.Pending;
                }

                context.Orders.Add(order);
                context.SaveChanges();
                return order.OrderId;
            }
        }

        public bool Update(Order order)
        {
            using (var context = new EcommerceDbContext())
            {
                var existingOrder = context.Orders.Find(order.OrderId);
                if (existingOrder == null) return false;

                existingOrder.Status = order.Status;
                existingOrder.TotalAmount = order.TotalAmount;
                existingOrder.ShippingAddress = order.ShippingAddress;
                existingOrder.BillingAddress = order.BillingAddress;
                existingOrder.ModifiedDate = DateTime.Now;

                return context.SaveChanges() > 0;
            }
        }

        public bool UpdateStatus(int orderId, string status)
        {
            using (var context = new EcommerceDbContext())
            {
                var order = context.Orders.Find(orderId);
                if (order == null) return false;

                order.Status = status;
                order.ModifiedDate = DateTime.Now;

                return context.SaveChanges() > 0;
            }
        }

        public bool Cancel(int orderId)
        {
            using (var context = new EcommerceDbContext())
            {
                var order = context.Orders.Find(orderId);
                if (order == null) return false;

                order.Status = OrderStatus.Cancelled;
                order.ModifiedDate = DateTime.Now;

                return context.SaveChanges() > 0;
            }
        }
    }
}
