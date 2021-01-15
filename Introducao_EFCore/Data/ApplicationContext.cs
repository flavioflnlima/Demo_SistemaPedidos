using Microsoft.EntityFrameworkCore;

namespace EFcore.Data
{
    public class ApplicationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data source = (localdb)\\MSSQLLocalDB;Initial Catalog=CursoEFCora; Integrated Security=true");
        }
    }
}