using Microsoft.EntityFrameworkCore;

using Product.API.Data;
namespace Product.API.Services;
using Product.API.Models;
public class ProductService: IProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Product>> GetAll(CancellationToken cancellationToken= default)
    {
        return await _context.Product.ToListAsync(cancellationToken);
    }

    public async Task<Product> GetById(int Id, CancellationToken cancellationToken=default)
    {
        return await _context.Product.Where(x => x.Id == Id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Product> Add(Product product, CancellationToken cancellationToken=default)
    {
        var data = _context.Product.Add(product);
        await _context.SaveChangesAsync(cancellationToken);
        return  data.Entity;
    }

    public async Task<Product> Update(Product product, CancellationToken cancellationToken=default)
    {
        var data = _context.Product.Update(product);
        await _context.SaveChangesAsync(cancellationToken);
        return data.Entity;
    }

    public async Task<bool> Delete(int Id, CancellationToken cancellationToken=default)
    {
        var data = _context.Product.Where(x => x.Id == Id).FirstOrDefaultAsync(cancellationToken);
       var result= _context.Remove(data);
        await _context.SaveChangesAsync(cancellationToken);
        return result != null ? true : false;
    }


}