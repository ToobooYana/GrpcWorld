using System;
using Server.GenerateProtobufs.Entities;

namespace Server.GenerateProtobufs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press key to continue...");
            Console.ReadKey();

            var builder = new ProtobufBuilder();

            var productProtobuf = builder.CreateProtobufFor<Product>();
            Console.WriteLine(productProtobuf);
        }
    }
}
