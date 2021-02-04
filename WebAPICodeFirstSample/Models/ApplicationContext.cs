using Microsoft.EntityFrameworkCore;


namespace WebAPICodeFirstSample.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
        }

        public ApplicationContext(DbContextOptions opts) : base(opts)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
