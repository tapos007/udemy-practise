using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Request;
using DLL.Model;
using DLL.Repositories;
using Utility.Exceptions;

namespace BLL.Services
{
    public interface IStudentService
    {
        Task<Student> InsertAsync(StudentInsertRequestViewModel studentRequest);
        Task<List<Student>> GetAllAsync();
        Task<Student> UpdateAsync(string email, Student student);
        Task<Student> DeleteAsync(string email);
        Task<Student> GetAAsync(string email);
        Task<bool> EmailExists(string email);
    }
    
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;


        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Student> InsertAsync(StudentInsertRequestViewModel studentRequest)
        {
            var student = new Student()
            {
                Email = studentRequest.Email,
                Name = studentRequest.Name,
                DepartmentId =  studentRequest.DepartmentId
            };
            await _unitOfWork.StudentRepository.CreateAsync(student);
            if (await _unitOfWork.SaveCompletedAsync())
            {
                return student;
            }

            throw new ApplicationValidationException("insert has some problem");
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _unitOfWork.StudentRepository.GetList();
        }

        public async Task<Student> UpdateAsync(string email, Student student)
        {
            var dbStudent = await _unitOfWork.StudentRepository.FindSingleAsync(x => x.Email == email);

            if (dbStudent == null)
            {
                throw new ApplicationValidationException("student not found");
            }

            dbStudent.Name = student.Name;
            _unitOfWork.StudentRepository.Update(dbStudent);
            if (await _unitOfWork.SaveCompletedAsync())
            {
                return dbStudent;
            }

            throw new ApplicationValidationException("update has some issue");
        }

        public async Task<Student> DeleteAsync(string email)
        {
            var dbStudent = await _unitOfWork.StudentRepository.FindSingleAsync(x => x.Email == email);

            if (dbStudent == null)
            {
                throw new ApplicationValidationException("student not found");
            }

          
            _unitOfWork.StudentRepository.Delete(dbStudent);
            if (await _unitOfWork.SaveCompletedAsync())
            {
                return dbStudent;
            }

            throw new ApplicationValidationException("delete has some issue");
        }

        public async Task<Student> GetAAsync(string email)
        {
            return await _unitOfWork.StudentRepository.FindSingleAsync(x=>x.Email == email);
        }

        public async Task<bool> EmailExists(string email)
        {
            var student = await _unitOfWork.StudentRepository.FindSingleAsync(x => x.Email == email);

            if (student == null)
            {
                return true;
            }

            return false;
        }
    }
 }