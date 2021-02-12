using System;
using DLL.DBContext;
using DLL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;

namespace DLL
{
    public static class DLLDependency
    {
        public  static void AllDependency(IServiceCollection services,IConfiguration configuration)
        {

           
            
                // options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            
            
            // repository dependency

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            // services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            // services.AddTransient<IStudentRepository, StudentRepository>();
        }
    }
}