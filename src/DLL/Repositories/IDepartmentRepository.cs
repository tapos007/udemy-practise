using System.Collections.Generic;
using System.Threading.Tasks;
using DLL.DBContext;
using DLL.Model;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories
{
    public interface IDepartmentRepository
    {
        Task<Department> InsertAsync(Department department);
        Task<List<Department>> GetAllAsync();
        Task<bool> UpdateAsync(Department department);
        Task<bool> DeleteAsync(Department department);
        Task<Department> GetAAsync(string code);
        Task<Department> FindByName(string name);
        Task<Department> FindByCode(string code);
    }

    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Department> InsertAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await _context.Departments.ToListAsync();
        }
        
        public async Task<bool> DeleteAsync(Department department)
        {
            
            _context.Departments.Remove(department);
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
            
        }
        
        public async Task<Department> GetAAsync(string code)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(x => x.Code == code);
            return department;
            
        }

        public async Task<Department> FindByName(string name)
        {
            return  await _context.Departments.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<Department> FindByCode(string code)
        {
            return  await _context.Departments.FirstOrDefaultAsync(x => x.Code == code);
        }

        public async Task<bool> UpdateAsync(Department department)
        {
           
            _context.Departments.Update(department);
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            };
            return false;
            
        }
    }
}