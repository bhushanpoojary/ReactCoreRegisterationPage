using Microsoft.EntityFrameworkCore;

namespace ReactCoreRegisterationPage.Server
{
    public class RegisterationDbContext : DbContext
    {
        public RegisterationDbContext(DbContextOptions<RegisterationDbContext> options)
            : base(options)
        {
        }

        public DbSet<RegisteredPerson> RegisteredPersonEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegisteredPerson>().ToTable("RegisteredPersons");
            modelBuilder.Entity<RegisteredPerson>().HasKey(rp => rp.PersonID);
        }
    }
}
