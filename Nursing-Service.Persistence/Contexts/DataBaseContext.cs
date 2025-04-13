using Microsoft.EntityFrameworkCore;
using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Domain.Entities.User;

namespace Nursing_Service.Persistence.Contexts
{
    public class DataBaseContext : DbContext, IDataBaseContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Role>().HasData(new Role
            //{
            //    Id = 1,
            //    Name = nameof(UserRoles.Admin)
            //});

            //modelBuilder.Entity<Role>().HasData(new Role
            //{
            //    Id = 2,
            //    Name = nameof(UserRoles.Author)
            //});

            //modelBuilder.Entity<Role>().HasData(new Role
            //{
            //    Id = 3,
            //    Name = nameof(UserRoles.Customer)
            //});

            //modelBuilder.Entity<Role>().HasData(new Role
            //{
            //    Id = 4,
            //    Name = nameof(UserRoles.Operator)
            //});

            // Applying index on the User entity's email.
            // Making email unique in the User entity.
            modelBuilder.Entity<User>().HasIndex(u => new { u.Email, u.DeletedDateTime }).IsUnique();

            // Query only users which are not deleted 
            modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
        }

    }
}
