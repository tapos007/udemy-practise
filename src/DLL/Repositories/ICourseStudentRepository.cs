using System.Collections.Generic;
using System.Threading.Tasks;
using DLL.DBContext;
using DLL.Model;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories
{
    public interface ICourseStudentRepository: IRepositoryBase<CourseStudent>
    {
      
    }

    public class CourseStudentRepository : RepositoryBase<CourseStudent>, ICourseStudentRepository
    {
        public CourseStudentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}