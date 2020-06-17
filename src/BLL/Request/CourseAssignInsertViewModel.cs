using System;
using System.Threading;
using System.Threading.Tasks;
using BLL.Services;
using DLL.Repositories;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Request
{
    public class CourseAssignInsertViewModel
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
    
    public class CourseAssignInsertViewModelValidator : AbstractValidator<CourseAssignInsertViewModel> {
        private readonly IServiceProvider _serviceProvider;


        public CourseAssignInsertViewModelValidator(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;

            RuleFor(x => x.CourseId).NotNull()
                .NotEmpty().MustAsync(courseIdExists).WithMessage("course not exits in our system");
            RuleFor(x => x.StudentId).NotNull()
                .NotEmpty().MustAsync(studentIdExists).WithMessage("student id not exists in our system");
            
        }

        private async Task<bool> courseIdExists(int courseId, CancellationToken arg2)
        {
            

            var requiredService = _serviceProvider.GetRequiredService<ICourseService>();

            return ! await requiredService.IsIdExists(courseId);



        }

        private async Task<bool> studentIdExists(int studentId, CancellationToken arg2)
        {
            

            var requiredService = _serviceProvider.GetRequiredService<IStudentService>();

            return ! await requiredService.IsIdExists(studentId);
        }
    }
}