using System.Runtime.Serialization;

namespace SampleEcomStoreApi.Contracts.DataContracts
{
    [DataContract]
    public class OrderItemDto
    {
        [DataMember]
        public int OrderItemId { get; set; }

        [DataMember]
        public int OrderId { get; set; }

        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public ProductDto Product { get; set; }

        [DataMember]
        public int Quantity { get; set; }

        [DataMember]
        public decimal UnitPrice { get; set; }

        [DataMember]
        public decimal TotalPrice { get; set; }
    }
}
