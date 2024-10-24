using Microsoft.EntityFrameworkCore;
using Okul_Proje.Models;

namespace Okul_Proje.Services
{
    public class ApplicationDbContext : DbContext
    {
        public  ApplicationDbContext(DbContextOptions options) : base(options) 
        {

        }
        public DbSet<Student> Students { get; set; } 
    }
}
