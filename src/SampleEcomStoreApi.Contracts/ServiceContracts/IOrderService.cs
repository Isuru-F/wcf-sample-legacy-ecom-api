using System.Collections.Generic;
using System.ServiceModel;
using SampleEcomStoreApi.Contracts.DataContracts;

namespace SampleEcomStoreApi.Contracts.ServiceContracts
{
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        List<OrderDto> GetAllOrders();

        [OperationContract]
        OrderDto GetOrderById(int orderId);

        [OperationContract]
        List<OrderDto> GetOrdersByCustomerId(int customerId);

        [OperationContract]
        List<OrderDto> GetOrdersByStatus(string status);

        [OperationContract]
        int CreateOrder(OrderDto order);

        [OperationContract]
        bool UpdateOrder(OrderDto order);

        [OperationContract]
        bool UpdateOrderStatus(int orderId, string status);

        [OperationContract]
        bool CancelOrder(int orderId);

        [OperationContract]
        decimal CalculateOrderTotal(List<OrderItemDto> orderItems);
    }
}
