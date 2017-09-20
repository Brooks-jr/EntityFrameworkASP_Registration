using Microsoft.EntityFrameworkCore;
 
namespace firstEntityASP.Models
{
    public class HomeContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public HomeContext(DbContextOptions<HomeContext> options) : base(options) { }
        public DbSet<Person> User { get; set; }
    }
}
