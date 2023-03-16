using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Seed
{
    internal class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(new Product
            {
                Id = 1,
                Name = "Kalem1",
                Stock = 100,
                Price = 100,
                CategoryId = 1

            },
            new Product
            {
                Id = 2,
                Name = "Kalem2",
                Stock = 200,
                Price = 300,
                CategoryId = 1

            },
            new Product
            {
                Id = 3,
                Name = "Kitap1",
                Stock = 300,
                Price = 200,
                CategoryId = 2

            },
            new Product
            {
                Id = 4,
                Name = "Cetvel1",
                Stock = 100,
                Price = 100,
                CategoryId = 3

            });
        }
    }
}
