using System;
using GrpcWorld.Models;

namespace GrpcWorld.Basic.Server.Extensions
{
    public static class EntitiesToProtoExtensions
    {
        public static Message.Product MapToMessage(this Product product)
        {
            return new Message.Product
            {
                Category = product.Category?.MapToMessage(),
                Supplier = product.Supplier?.MapToMessage(),
                ProductId = product.ProductId,
                Discontinued = product.Discontinued,
                ProductImage = product.ProductImage,
                ProductName = product.ProductName,
                QuantityPerUnit = product.QuantityPerUnit,
                ReorderLevel = product.ReorderLevel,
                UnitPrice = Convert.ToDouble(product.UnitPrice),
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder
            };
        }

        public static Entities.Category MapToMessage(this Category category)
        {
            return new Entities.Category
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                Description = category.Description
            };
        }

        public static Entities.Supplier MapToMessage(this Supplier supplier)
        {
            return new Entities.Supplier
            {
                SupplierId = supplier.SupplierId,
                Address = supplier.Address,
                City = supplier.City,
                CompanyName = supplier.CompanyName,
                ContactName = supplier.ContactName,
                ContactTitle = supplier.ContactTitle,
                Country = supplier.Country,
                Fax = supplier.Fax,
                HomePage = supplier.HomePage,
                Phone = supplier.Phone,
                PostalCode = supplier.PostalCode,
                Region = supplier.Region
            };
        }
    }
}