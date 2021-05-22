using Microsoft.EntityFrameworkCore;

#nullable disable

namespace api.Models
{
    public partial class ApiDbContext : DbContext
    {
        public ApiDbContext()
        {
        }

        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=local.db");
                optionsBuilder.EnableSensitiveDataLogging(true);
                optionsBuilder.EnableDetailedErrors(true);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Cidades>(entity =>
            {
                entity.HasMany<Fronteiras>(c => c.Fronteiras).WithOne(f => f.Cidade1);
            });

            modelBuilder.Entity<Fronteiras>(entity =>
            {
                entity.HasKey("CidadesId1", "CidadesId2");
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Cidades> Cidades { get; set; }

        public DbSet<Fronteiras> Fronteiras { get; set; }

        public DbSet<Users> Usuarios { get; set; }

    }

}
