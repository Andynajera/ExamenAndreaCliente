using Microsoft.EntityFrameworkCore;
using PeliculaItem;
namespace Peliculas.Data
{

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public DbSet<PeliculaItems>? PeliculaItem { get; set; }
    }
}
