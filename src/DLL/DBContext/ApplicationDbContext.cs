using DLL.Model;
using Microsoft.EntityFrameworkCore;

namespace DLL.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Student> Students { get; set; }
    }
}