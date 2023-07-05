using Microsoft.AspNetCore.Mvc;
using Product.API.RabbitMQs;
using Product.API.Services;

namespace Product.API.Controllers;
using Product.API.Models;
[Route("api/controller")]
[ApiController]
public class ProductController : ControllerBase
{
 private readonly IProductService _productService;
 private readonly IRabbitMQProducer _rabbitMqProducer;

 public ProductController(IProductService productService, IRabbitMQProducer rabbitMqProducer)
 {
     _rabbitMqProducer = rabbitMqProducer;
     _productService = productService;
 }
 [HttpGet("GetAll")]
 public async Task<IEnumerable<Product>> GetAll(CancellationToken cancellationToken)
 {
     return await _productService.GetAll(cancellationToken);
 }
 [HttpGet("GetById")]
 public async Task<Product> GetById(int id,CancellationToken cancellationToken)
 {
     return await _productService.GetById(id,cancellationToken);
 }

 [HttpPost]
 public async Task<Product> Add(Product product,CancellationToken cancellationToken)
 {
     var result = await _productService.Add(product, cancellationToken);
     _rabbitMqProducer.SendMessage(product);
     return result;
 }

 [HttpPut]
 public async Task<Product> Update(Product product,CancellationToken cancellationToken)
 {
     return await _productService.Update(product, cancellationToken);
 }

 [HttpDelete]
 public async Task<bool> Delete(int Id ,CancellationToken cancellationToken)
 {
     return await _productService.Delete(Id, cancellationToken);
 }
}