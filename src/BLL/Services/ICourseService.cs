using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using BLL.Request;
using DLL.Model;
using DLL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Minio;
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
        private readonly IConfiguration _configuration;
        private readonly string _server;
        private readonly string _accesskey;
        private readonly string _secretKey;
        private readonly string _bucketName;


        public CourseService(IUnitOfWork unitOfWork,IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _server = configuration.GetValue<string>("MediaServer:ImageServer");
            _accesskey = configuration.GetValue<string>("MediaServer:AccessKey");
            _secretKey = configuration.GetValue<string>("MediaServer:SecretKey");
            _bucketName = configuration.GetValue<string>("MediaServer:BucketName");
        }

        public async Task<Course> InsertAsync(CourseInsertRequestViewModel request)
        {
            var course = new Course();
            course.Code = request.Code;
            course.Name = request.Name;
            course.Credit = request.Credit;
            course.ImageUrl = await ForImageUpload(request.CourseImage);
            await _unitOfWork.CourseRepository.CreateAsync(course);

            if (await _unitOfWork.SaveCompletedAsync())
            {
                course.ImageUrl = _configuration.GetValue<string>("MediaServer:ImageAccessUrl") + course.ImageUrl;
                return course;
            }

            throw new ApplicationValidationException("course insert has some problem");
        }

        private async Task<string> ForImageUpload(IFormFile file)
        {
            var client = new MinioClient(_server,_accesskey,_secretKey);
            await SetupBucket(client, _bucketName);
            var extension = Path.GetExtension((file.FileName)) ?? ".png";
            var fileName = Guid.NewGuid().ToString() + extension;
            var imagePath = _configuration.GetValue<string>("MediaServer:LocalImageStorage");
            var path = Path.Combine(Directory.GetCurrentDirectory(), imagePath, fileName).ToLower();
            await using var bits = new FileStream(path,FileMode.Create);
            await file.CopyToAsync(bits);
            bits.Close();

            await client.PutObjectAsync(_bucketName, fileName, path, "image/jpeg");
            File.Delete(path);
            return fileName;
        }

        private async Task SetupBucket(MinioClient client, string bucket)
        {
            var found = await client.BucketExistsAsync(bucket);
            if (!found)
            {
                await client.MakeBucketAsync(bucket);
            }
        }

        public async Task<List<Course>> GetAllAsync()
        {
            var allCourse = await _unitOfWork.CourseRepository.GetList();
           var latest =  allCourse.Select(c =>
            {
                c.ImageUrl = _configuration.GetValue<string>("MediaServer:ImageAccessUrl") + c.ImageUrl;
                return c;
            }).ToList();
            return latest;
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
           course.ImageUrl =  _configuration.GetValue<string>("MediaServer:ImageAccessUrl") + course.ImageUrl;

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