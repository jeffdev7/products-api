﻿using Microsoft.EntityFrameworkCore;
using products.domain.Entities;
using products.domain.Repository;
using products.infra.Data;

namespace products.infra.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationContext context) : base(context)
        {
        }

        public IQueryable<Product> GetProducts()
        {
            return _context.Products;
        }

        public async Task<bool> RemoveProductAsync(string Id)
        {
            Product? product = await _context.Products.Where(_ => _.Id == Id).FirstOrDefaultAsync(CancellationToken.None);

            if (product == null)
                return false;
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
