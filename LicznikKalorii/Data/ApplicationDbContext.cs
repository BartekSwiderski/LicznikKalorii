using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LicznikKalorii.Models;

namespace LicznikKalorii.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<LicznikKalorii.Models.MealEaten>? MealEaten { get; set; }
        public DbSet<LicznikKalorii.Models.Meal>? Meal { get; set; }
    }
}