using ProtoBuf;

namespace Server.GenerateProtobufs.Entities
{
    [ProtoContract]
    public class Product
    {
        [ProtoMember(1)]
        public int ProductId { get; set; }
        [ProtoMember(2)]
        public string ProductName { get; set; }
        [ProtoMember(3)] 
        public string QuantityPerUnit { get; set; }
        [ProtoMember(4)] 
        public decimal UnitPrice { get; set; }
        [ProtoMember(5)]
        public short UnitsInStock { get; set; }
        [ProtoMember(6)]
        public short UnitsOnOrder { get; set; }
        [ProtoMember(7)]
        public short ReorderLevel { get; set; }
        [ProtoMember(8)]
        public bool Discontinued { get; set; }
        [ProtoMember(9)]
        public string ProductImage { get; set; }
        [ProtoMember(10)]
        public Category Category { get; set; }
        [ProtoMember(11)]
        public Supplier Supplier { get; set; }
    }
}