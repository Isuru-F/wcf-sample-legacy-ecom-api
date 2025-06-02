using System.Runtime.Serialization;

namespace TestClient.ServiceProxies
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/SampleEcomStoreApi.Contracts.DataContracts")]
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
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public DateTime ModifiedDate { get; set; }

        [DataMember]
        public bool IsActive { get; set; }
    }
}
