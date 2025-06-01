using System.Collections.Generic;
using SampleEcomStoreApi.Contracts.DataContracts;

namespace SampleEcomStoreApi.BusinessLogic.Managers
{
    public interface IOrderManager
    {
        List<OrderDto> GetAllOrders();
        OrderDto GetOrderById(int orderId);
        List<OrderDto> GetOrdersByCustomerId(int customerId);
        List<OrderDto> GetOrdersByStatus(string status);
        int CreateOrder(OrderDto order);
        bool UpdateOrder(OrderDto order);
        bool UpdateOrderStatus(int orderId, string status);
        bool CancelOrder(int orderId);
        decimal CalculateOrderTotal(List<OrderItemDto> orderItems);
    }
}
