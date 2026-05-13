using Microsoft.EntityFrameworkCore;
using RoyalVilla_API.Models;

namespace RoyalVilla_API.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
       public DbSet<Villa> Villa { get; set; }
        public DbSet<User>Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name = "Royal Villa",
                    Details = "This is the Royal Villa with stuning ocean view",
                    Price = 200,
                    Color = "Red",
                    Occupancy =1,
                    CreatedDate = new DateTime(2024, 4, 26),
                    UpdatedDate = new DateTime(2024, 4, 26)
                },
                new Villa
                {
                    Id = 2,
                    Name = "Royal Villa",
                    Details = "This is the Royal Villa with pool and bar view",
                    Price = 200,
                    Color = "Black",
                    Occupancy = 2,
                    CreatedDate = new DateTime(2024, 4, 26),
                    UpdatedDate = new DateTime(2024, 4, 26)
                },
                 new Villa
                 {
                     Id = 3,
                     Name = "Royal Villa",
                     Details = "This is the Royal Villa with pool and bar view",
                     Price = 200,
                     Color = "White",
                     Occupancy = 3,
                     CreatedDate = new DateTime(2024, 4, 26),
                     UpdatedDate = new DateTime(2024, 4, 26)
                 },
                 new Villa
                 {
                     Id = 4,
                     Name = "Royal Villa",
                     Details = "This is the Royal Villa with football feild and view",
                     Price = 200,
                     Color = "Green",
                     Occupancy = 4,
                     CreatedDate = new DateTime(2024, 4, 26),
                     UpdatedDate = new DateTime(2024, 4, 26)
                 }
            );
        }



    }
}
