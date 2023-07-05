using System.Net.Sockets;

namespace Product.API.Services;
using Product.API.Models;
public interface IProductService
{
  Task <IEnumerable<Product>> GetAll(CancellationToken cancellationToken);
  Task<Product> GetById(int Id, CancellationToken cancellationToken);
  Task<Product>  Add(Product product, CancellationToken cancellationToken);
  Task<Product>  Update(Product product, CancellationToken cancellationToken);
  Task<bool> Delete(int Id, CancellationToken cancellationToken);
}