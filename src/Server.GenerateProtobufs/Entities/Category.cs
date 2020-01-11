using ProtoBuf;

namespace Server.GenerateProtobufs.Entities
{
    [ProtoContract]
    public class Category
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public string CategoryName { get; set; }
        [ProtoMember(3)]
        public string Description { get; set; }
    }
}