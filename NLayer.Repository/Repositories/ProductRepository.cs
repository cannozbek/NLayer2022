using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    //Burada sadece IProductRepository implemente edilseydi diğer tüm metotları da tekrar tanımlammız gerekirdi
    //Burada sadece genericrepository inherit alınarak halledeiliyor ve o metotları tekrar etmemize gerek olmuyor
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetProductsWithCategory()
        {
            //Eager Loding -> aşağıda kullanılan mantık
            //Lazy Loading
            return await _context.Products.Include(x=>x.Category).ToListAsync();
        }
    }
}
