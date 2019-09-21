using Microsoft.EntityFrameworkCore;

namespace HealthCatalystUserSearchAPI.Context
{
    public class UserDbContext: DbContext
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Interests> Interests { get; set; }
        public DbSet<Addresses> Addresses { get; set; }
        public DbSet<UserToInterest> UserToInterest { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetUpModelRelationships(modelBuilder);
            modelBuilder.SeedDatabase();
        }

        /// <summary>
        /// Setup the One-to-Many and Many-To-Many relationships
        /// </summary>
        /// <param name="modelBuilder">The current modelBuilder</param>
        private static void SetUpModelRelationships(ModelBuilder modelBuilder)
        {
            // Many-To-Many Setup
            modelBuilder.Entity<UserToInterest>()
                .HasKey(u => new { u.UserId, u.InterestId });

            modelBuilder.Entity<UserToInterest>()
                .HasOne(u => u.User)
                .WithMany(b => b.MyInterests)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<UserToInterest>()
                .HasOne(i => i.Interest)
                .WithMany(c => c.Users)
                .HasForeignKey(i => i.InterestId);

            // One-To-Many Setup
            modelBuilder.Entity<Addresses>()
                .HasMany(c => c.Users)
                .WithOne(e => e.MyAddress);
        }
    }
}
