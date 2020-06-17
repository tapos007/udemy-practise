using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DLL.DBContext;
using DLL.Model;
using DLL.ResponseViewModel;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories
{
    public interface IStudentRepository : IRepositoryBase<Student>
    {
        Task<StudentCourseViewModel> GetSpecificStudentCourseListAsync(int studentId);
    }

    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<StudentCourseViewModel> GetSpecificStudentCourseListAsync(int studentId)
        {
            return await _context.Students
                .Include(x => x.CourseStudents).ThenInclude(x => x.Course)
                .Select(x => new StudentCourseViewModel()
                {
                    StudentId = x.StudentId,
                    Name = x.Name,
                    Email = x.Email,
                    Courses = x.CourseStudents.Select(x => x.Course).ToList()
                })
                .FirstOrDefaultAsync(x => x.StudentId == studentId);



        }
    }
}