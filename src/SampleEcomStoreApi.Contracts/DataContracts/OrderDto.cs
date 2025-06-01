using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SampleEcomStoreApi.Contracts.DataContracts
{
    [DataContract]
    public class OrderDto
    {
        [DataMember]
        public int OrderId { get; set; }

        [DataMember]
        public int CustomerId { get; set; }

        [DataMember]
        public CustomerDto Customer { get; set; }

        [DataMember]
        public DateTime OrderDate { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public decimal TotalAmount { get; set; }

        [DataMember]
        public string ShippingAddress { get; set; }

        [DataMember]
        public string BillingAddress { get; set; }

        [DataMember]
        public List<OrderItemDto> OrderItems { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public DateTime ModifiedDate { get; set; }
    }
}
