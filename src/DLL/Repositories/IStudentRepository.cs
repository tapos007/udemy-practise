using System.Collections.Generic;
using System.Threading.Tasks;
using DLL.DBContext;
using DLL.Model;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories
{
    public interface IStudentRepository
    {
        Task<Student> InsertAsync(Student student);
        Task<List<Student>> GetAllAsync();
        Task<Student> UpdateAsync(string code, Student student);
        Task<Student> DeleteAsync(string code);
        Task<Student> GetAAsync(string code);
    }

    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Student> InsertAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }
        
        public async Task<Student> DeleteAsync(string email)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Email == email);

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return student;
            
        }
        
        public async Task<Student> GetAAsync(string email)
        {
            var department = await _context.Students.FirstOrDefaultAsync(x => x.Email == email);
            return department;
            
        }
        
        public async Task<Student> UpdateAsync(string email,Student student)
        {
            var findStudent = await _context.Students.FirstOrDefaultAsync(x => x.Email == email);
            findStudent.Name = student.Name;
            _context.Students.Update(findStudent);
            await _context.SaveChangesAsync();
            return findStudent;
            
        }
    }
}