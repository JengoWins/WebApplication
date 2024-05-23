using Microsoft.EntityFrameworkCore;

namespace WebAppLab11.Classes
{
    public class MySqlApplicationcs:DbContext
    {
        public DbSet<Crew> Crew { get; set; }
        public DbSet<Ships> Ships { get; set; }
        public DbSet<Weapons> Weapons { get; set; }
        public DbSet<Basket> Basket { get; set; }
        public DbSet<Formregistration> formregistration { get; set; }
        public MySqlApplicationcs(DbContextOptions option): base(option) {}

        public MySqlApplicationcs()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user=root;password=SaraParker206;database=dbkurenkov",
                new MySqlServerVersion(new Version(8, 0, 25)));
        }

    }
}
