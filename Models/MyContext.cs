#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace Productos_y_Categorias.Models;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions options) : base(options) { }

    public DbSet<Producto> Productos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Asociacion> Asociaciones { get; set; }
}