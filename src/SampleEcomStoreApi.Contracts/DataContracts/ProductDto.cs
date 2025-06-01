using System;
using System.Runtime.Serialization;

namespace SampleEcomStoreApi.Contracts.DataContracts
{
    [DataContract]
    public class ProductDto
    {
        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public int StockQuantity { get; set; }

        [DataMember]
        public string ImageUrl { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public DateTime ModifiedDate { get; set; }

        [DataMember]
        public bool IsActive { get; set; }
    }
}
