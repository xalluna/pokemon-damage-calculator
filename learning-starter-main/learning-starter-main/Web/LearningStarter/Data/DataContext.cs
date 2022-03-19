using LearningStarter.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningStarter.Data
{
    public sealed class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Ability> Abilities { get; set; }
        public DbSet<ExperienceCurve> ExperienceCurves { get; set; }
        public DbSet<Item> Items  { get; set; }
        public DbSet<Move> Moves  { get; set; }
        // public DbSet<MoveCategory> MoveCategories { get; set; }
        public DbSet<PokemonSpecies> PokemonSpeciesList { get; set; }
        public DbSet<PType> Types { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(x => x.FirstName)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.LastName)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.Username)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.Password)
                .IsRequired();

        }
    }
}
