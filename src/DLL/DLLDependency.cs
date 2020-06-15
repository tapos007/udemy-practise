using DLL.DBContext;
using DLL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DLL
{
    public static class DLLDependency
    {
        public  static void AllDependency(IServiceCollection services,IConfiguration configuration)
        {
            var mm = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            
            
            // repository dependency

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            // services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            // services.AddTransient<IStudentRepository, StudentRepository>();
        }
    }
}