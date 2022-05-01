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
        public DbSet<PokemonSpecies> PokemonSpecies { get; set; }
        public DbSet<PType> Types { get; set; }
        public DbSet<Pokemon> Pokemon { get; set; }
        public DbSet<Nature> Natures { get; set; }

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

            modelBuilder.Entity<Pokemon>().HasMany(moves => moves.Moves);
            
            // modelBuilder.Entity<Move>().HasMany(move => move.Pokemon)
            //     .WithMany(pokemon => pokemon.MoveOne)
            //     .HasForeignKey(pokemon => pokemon.MoveOneId);
            //
            // modelBuilder.Entity<Move>().HasMany(move => move.Pokemon)
            //     .WithOne(pokemon => pokemon.MoveTwo)
            //     .HasForeignKey(pokemon => pokemon.MoveTwoId);
            //
            // modelBuilder.Entity<Move>().HasMany(move => move.Pokemon)
            //     .WithOne(pokemon => pokemon.MoveThree)
            //     .HasForeignKey(pokemon => pokemon.MoveThreeId);
            //
            // modelBuilder.Entity<Move>().HasMany(move => move.Pokemon)
            //     .WithOne(pokemon => pokemon.MoveFour)
            //     .HasForeignKey(pokemon => pokemon.MoveFourId);

        }
    }
}
