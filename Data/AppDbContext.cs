using Microsoft.EntityFrameworkCore;
using CoursesRelaxBack.Models; // Importer le modèle User

namespace CoursesRelaxBack.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Table pour les utilisateurs
        public required DbSet<User> Users { get; set; }
    }
}
