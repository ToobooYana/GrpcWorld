using ProtoBuf.Meta;

namespace Server.GenerateProtobufs
{
    public class ProtobufBuilder
    {
        public string CreateProtobufFor<T>()
        {
            var msg = ProtoBuf.Serializer.GetProto<T>(ProtoSyntax.Proto3);
            return msg;
        }
    }
}
