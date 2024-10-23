using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Product.API.Entities;
using Product.API.Persistence;
using Product.API.Repositories.Interfaces;

namespace Product.API.Repositories;

public class ProductRepository : RepositoryBaseAsync<CatalogProduct, long, ProductContext>, IProductRepository
{
    public ProductRepository(ProductContext context, IUnitOfWork<ProductContext> unitOfWork) : base(context, unitOfWork)
    {
    }

    public async Task<IEnumerable<CatalogProduct>> GetProducts() => await FindAll().ToListAsync();

    public Task<CatalogProduct> GetProduct(long id) => GetByIdAsync(id);

    public Task<CatalogProduct> GetProductByNo(string productNo) =>
        FindByCondition(x => x.No.Equals(productNo)).SingleOrDefaultAsync();

    public Task CreateProduct(CatalogProduct product) => CreateAsync(product);

    public Task UpdateProduct(CatalogProduct product) => UpdateAsync(product);

    public async Task DeleteProduct(long id)
    {
        var product =  await GetProduct(id);
        if (product != null) DeleteAsync(product);
    }
}