using Microsoft.EntityFrameworkCore;

namespace ASP_proj.Models
{
    public class SiteContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Image> Images { get; set; }
        public SiteContext(DbContextOptions<SiteContext> options) : base(options) { }
        

    }
}
