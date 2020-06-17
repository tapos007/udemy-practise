using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Request;
using DLL.Model;
using DLL.Repositories;
using Utility.Exceptions;

namespace BLL.Services
{
    public interface ICourseService
    {
        Task<Course> InsertAsync(CourseInsertRequestViewModel request);
        Task<List<Course>> GetAllAsync();
        Task<Course> UpdateAsync(string code, Course department);
        Task<Course> DeleteAsync(string code);
        Task<Course> GetAAsync(string code);

        Task<bool> IsCodeExists(string code);
        Task<bool> IsNameExists(string name);
        Task<bool> IsIdExists(int id);

    }

    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;


        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }

        public async Task<Course> InsertAsync(CourseInsertRequestViewModel request)
        {
            var course = new Course();
            course.Code = request.Code;
            course.Name = request.Name;
            course.Credit = request.Credit;
            await _unitOfWork.CourseRepository.CreateAsync(course);

            if (await _unitOfWork.SaveCompletedAsync())
            {
                return course;
            }

            throw new ApplicationValidationException("course insert has some problem");
        }

        public async Task<List<Course>> GetAllAsync()
        {
            return await _unitOfWork.CourseRepository.GetList();
        }

        public async Task<Course> UpdateAsync(string code, Course aCourse)
        {
            var course = await _unitOfWork.CourseRepository.FindSingleAsync(x => x.Code == code);

            if (course == null)
            {
                throw new ApplicationValidationException("course not found");
            }

            if (!string.IsNullOrWhiteSpace(aCourse.Code))
            {
                var existsAlreadyCode = await _unitOfWork.CourseRepository.FindSingleAsync(x => x.Code == code);
                if (existsAlreadyCode != null)
                {
                    throw new ApplicationValidationException("your updated code already present in our system");
                }

                course.Code = aCourse.Code;
            }

            if (!string.IsNullOrWhiteSpace(aCourse.Name))
            {
                var existsAlreadyCode = await _unitOfWork.CourseRepository.FindSingleAsync(x => x.Name == aCourse.Name);
                if (existsAlreadyCode != null)
                {
                    throw new ApplicationValidationException("your updated name already present in our system");
                }

                course.Name = aCourse.Name;
            }

            _unitOfWork.CourseRepository.Update(course);
            if (await _unitOfWork.SaveCompletedAsync())
            {
                return course;
            }

            throw new ApplicationValidationException("in update have some problem");
        }

        public async Task<Course> DeleteAsync(string code)
        {
            var course = await _unitOfWork.CourseRepository.FindSingleAsync(x => x.Code == code);

            if (course == null)
            {
                throw new ApplicationValidationException("course not found");
            }

            _unitOfWork.CourseRepository.Delete(course);
            if (await _unitOfWork.SaveCompletedAsync())
            {
                return course;
            }

            throw new ApplicationValidationException("some problem for delete data");
        }

        public async Task<Course> GetAAsync(string code)
        {
            var course = await _unitOfWork.CourseRepository.FindSingleAsync(x => x.Code == code);

            if (course == null)
            {
                throw new ApplicationValidationException("course not found");
            }

            return course;
        }

        public async Task<bool> IsCodeExists(string code)
        {
            var department = await _unitOfWork.CourseRepository.FindSingleAsync(x => x.Code == code);;

            if (department == null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IsNameExists(string name)
        {
            var department = await _unitOfWork.CourseRepository.FindSingleAsync(x => x.Name == name);;

            if (department == null)
            {
                return true;
            }

            return false;
        }
        
        public async Task<bool> IsIdExists(int id)
        {
            var course = await _unitOfWork.CourseRepository.FindSingleAsync(x => x.CourseId == id);;

            if (course == null)
            {
                return true;
            }

            return false;
        }

        
    }
}