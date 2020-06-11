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
        Task<Department> UpdateAsync(string code, Department department);
        Task<Department> DeleteAsync(string code);
        Task<Department> GetAAsync(string code);
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
        
        public async Task<Department> DeleteAsync(string code)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(x => x.Code == code);

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return department;
            
        }
        
        public async Task<Department> GetAAsync(string code)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(x => x.Code == code);
            return department;
            
        }
        
        public async Task<Department> UpdateAsync(string code,Department department)
        {
            var findDepartment = await _context.Departments.FirstOrDefaultAsync(x => x.Code == code);
            findDepartment.Name = department.Name;
            _context.Departments.Update(findDepartment);
            await _context.SaveChangesAsync();
            return department;
            
        }
    }
}