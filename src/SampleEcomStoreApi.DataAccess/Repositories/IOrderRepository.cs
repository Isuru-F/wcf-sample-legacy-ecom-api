using System.Collections.Generic;
using SampleEcomStoreApi.DataAccess.Entities;

namespace SampleEcomStoreApi.DataAccess.Repositories
{
    public interface IOrderRepository
    {
        List<Order> GetAll();
        Order GetById(int orderId);
        List<Order> GetByCustomerId(int customerId);
        List<Order> GetByStatus(string status);
        int Create(Order order);
        bool Update(Order order);
        bool UpdateStatus(int orderId, string status);
        bool Cancel(int orderId);
    }
}
