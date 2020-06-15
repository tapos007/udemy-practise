using System.Threading.Tasks;
using DLL.Model;
using DLL.Repositories;

namespace BLL.Services
{
    public interface ITestService
    {
        Task InsertData();
    }

    public class TestService : ITestService
    {
        private readonly IUnitOfWork _unitOfWork;


        public TestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task InsertData()
        {
            var department = new Department()
            {
                Code = "arts",
                Name = "art department"
            };
            
            var student = new Student()
            {
                Email = "art@gmail.com",
                Name = "mr arts"
            };

            await _unitOfWork.DepartmentRepository.CreateAsync(department);
            await _unitOfWork.StudentRepository.CreateAsync(student);
            
          await  _unitOfWork.SaveCompletedAsync();
            
            
        }
    }
}