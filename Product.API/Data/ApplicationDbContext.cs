using Microsoft.EntityFrameworkCore;

namespace Product.API.Data;
using Models;
public class ApplicationDbContext: DbContext
{
    private readonly IConfiguration _configuration;
    public ApplicationDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder option)
    {
        option.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
    }

    public DbSet<Product> Product { get; set; }

}