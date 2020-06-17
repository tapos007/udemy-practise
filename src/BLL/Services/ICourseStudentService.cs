using System.Threading.Tasks;
using BLL.Request;
using DLL.Model;
using DLL.Repositories;
using DLL.ResponseViewModel;
using Utility.Exceptions;
using Utility.Models;

namespace BLL.Services
{
    public interface ICourseStudentService
    {
        Task<ApiSuccessResponse> InsertAsync(CourseAssignInsertViewModel request);

        Task<StudentCourseViewModel> CourseListAsync(int studentId);
    }

    public class CourseStudentService : ICourseStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseStudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiSuccessResponse> InsertAsync(CourseAssignInsertViewModel request)
        {
            var isStudentAlreadyEntroll = await _unitOfWork.CourseStudentRepository.FindSingleAsync(x =>
                x.CourseId == request.CourseId &&
                x.StudentId == request.StudentId);

            if (isStudentAlreadyEntroll != null)
            {
                throw new ApplicationValidationException("this student already enroll this course");
            }
            
            var courseStudent = new CourseStudent()
            {
                CourseId = request.CourseId,
                StudentId = request.StudentId
            };

            await _unitOfWork.CourseStudentRepository.CreateAsync(courseStudent);

            if (await _unitOfWork.SaveCompletedAsync())
            {
                return new ApiSuccessResponse()
                {
                    StatusCode = 200,
                    Message = "student enroll successfully"
                };
            }
            throw new ApplicationValidationException("something wrong for enrollment");
        }

        public async Task<StudentCourseViewModel> CourseListAsync(int studentId)
        {
            return await _unitOfWork.StudentRepository.GetSpecificStudentCourseListAsync(studentId);
        }
    }
}