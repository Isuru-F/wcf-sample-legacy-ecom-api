using System;
using System.Collections.Generic;
using System.Linq;
using SampleEcomStoreApi.DataAccess.Context;
using SampleEcomStoreApi.DataAccess.Entities;
using SampleEcomStoreApi.Common.Constants;

namespace SampleEcomStoreApi.DataAccess.Repositories
{
    public class SimpleOrderRepository : IOrderRepository
    {
        public List<Order> GetAll()
        {
            using (var context = new EcommerceDbContext())
            {
                return EcommerceDbContext.Orders.ToList();
            }
        }

        public Order GetById(int orderId)
        {
            using (var context = new EcommerceDbContext())
            {
                return EcommerceDbContext.Orders.FirstOrDefault(o => o.OrderId == orderId);
            }
        }

        public List<Order> GetByCustomerId(int customerId)
        {
            using (var context = new EcommerceDbContext())
            {
                return EcommerceDbContext.Orders
                    .Where(o => o.CustomerId == customerId)
                    .OrderByDescending(o => o.OrderDate)
                    .ToList();
            }
        }

        public List<Order> GetByStatus(string status)
        {
            using (var context = new EcommerceDbContext())
            {
                return EcommerceDbContext.Orders
                    .Where(o => o.Status.Equals(status, StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(o => o.OrderDate)
                    .ToList();
            }
        }

        public int Create(Order order)
        {
            using (var context = new EcommerceDbContext())
            {
                order.OrderId = EcommerceDbContext.Orders.Count + 1;
                order.OrderDate = DateTime.Now;
                order.CreatedDate = DateTime.Now;
                order.ModifiedDate = DateTime.Now;

                if (string.IsNullOrEmpty(order.Status))
                {
                    order.Status = OrderStatus.Pending;
                }

                EcommerceDbContext.Orders.Add(order);
                context.SaveChanges();
                return order.OrderId;
            }
        }

        public bool Update(Order order)
        {
            using (var context = new EcommerceDbContext())
            {
                var existingOrder = EcommerceDbContext.Orders.FirstOrDefault(o => o.OrderId == order.OrderId);
                if (existingOrder == null) return false;

                existingOrder.Status = order.Status;
                existingOrder.TotalAmount = order.TotalAmount;
                existingOrder.ShippingAddress = order.ShippingAddress;
                existingOrder.BillingAddress = order.BillingAddress;
                existingOrder.ModifiedDate = DateTime.Now;

                context.SaveChanges();
                return true;
            }
        }

        public bool UpdateStatus(int orderId, string status)
        {
            using (var context = new EcommerceDbContext())
            {
                var order = EcommerceDbContext.Orders.FirstOrDefault(o => o.OrderId == orderId);
                if (order == null) return false;

                order.Status = status;
                order.ModifiedDate = DateTime.Now;

                context.SaveChanges();
                return true;
            }
        }

        public bool Cancel(int orderId)
        {
            using (var context = new EcommerceDbContext())
            {
                var order = EcommerceDbContext.Orders.FirstOrDefault(o => o.OrderId == orderId);
                if (order == null) return false;

                order.Status = OrderStatus.Cancelled;
                order.ModifiedDate = DateTime.Now;

                context.SaveChanges();
                return true;
            }
        }
    }
}
