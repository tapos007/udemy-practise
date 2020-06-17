using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Request;
using DLL.Model;
using DLL.Repositories;
using Utility.Exceptions;

namespace BLL.Services
{
    public interface IDepartmentService
    {
        Task<Department> InsertAsync(DepartmentInsertRequestViewModel request);
        IQueryable<Department> GetAllAsync();
        Task<Department> UpdateAsync(string code, Department department);
        Task<Department> DeleteAsync(string code);
        Task<Department> GetAAsync(string code);

        Task<bool> IsCodeExists(string code);
        Task<bool> IsNameExists(string name);
        Task<bool> IsIdExists(int id);
    }

    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;


        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }

        public async Task<Department> InsertAsync(DepartmentInsertRequestViewModel request)
        {
            Department aDepartment = new Department();
            aDepartment.Code = request.Code;
            aDepartment.Name = request.Name;
            await _unitOfWork.DepartmentRepository.CreateAsync(aDepartment);

            if (await _unitOfWork.SaveCompletedAsync())
            {
                return aDepartment;
            }

            throw new ApplicationValidationException("department insert has some problem");
        }

        public IQueryable<Department> GetAllAsync()
        {
            return _unitOfWork.DepartmentRepository.QueryAll();
        }


        public async Task<Department> UpdateAsync(string code, Department adepartment)
        {
            var department = await _unitOfWork.DepartmentRepository.FindSingleAsync(x => x.Code == code);

            if (department == null)
            {
                throw new ApplicationValidationException("department not found");
            }

            if (!string.IsNullOrWhiteSpace(adepartment.Code))
            {
                var existsAlreadyCode = await _unitOfWork.DepartmentRepository.FindSingleAsync(x => x.Code == code);
                if (existsAlreadyCode != null)
                {
                    throw new ApplicationValidationException("your updated code already present in our system");
                }

                department.Code = adepartment.Code;
            }

            if (!string.IsNullOrWhiteSpace(adepartment.Name))
            {
                var existsAlreadyCode = await _unitOfWork.DepartmentRepository.FindSingleAsync(x => x.Name == adepartment.Name);
                if (existsAlreadyCode != null)
                {
                    throw new ApplicationValidationException("your updated name already present in our system");
                }

                department.Name = adepartment.Name;
            }

            _unitOfWork.DepartmentRepository.Update(department);
            if (await _unitOfWork.SaveCompletedAsync())
            {
                return department;
            }

            throw new ApplicationValidationException("in update have some problem");
        }

        public async Task<Department> DeleteAsync(string code)
        {
            var department = await _unitOfWork.DepartmentRepository.FindSingleAsync(x => x.Code == code);

            if (department == null)
            {
                throw new ApplicationValidationException("department not found");
            }

            _unitOfWork.DepartmentRepository.Delete(department);
            if (await _unitOfWork.SaveCompletedAsync())
            {
                return department;
            }

            throw new ApplicationValidationException("some problem for delete data");
        }

        public async Task<Department> GetAAsync(string code)
        {
            var department = await _unitOfWork.DepartmentRepository.FindSingleAsync(x => x.Code == code);

            if (department == null)
            {
                throw new ApplicationValidationException("department not found");
            }

            return department;
        }

        public async Task<bool> IsCodeExists(string code)
        {
            var department = await _unitOfWork.DepartmentRepository.FindSingleAsync(x => x.Code == code);;

            if (department == null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IsNameExists(string name)
        {
            var department = await _unitOfWork.DepartmentRepository.FindSingleAsync(x => x.Name == name);;

            if (department == null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IsIdExists(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.FindSingleAsync(x => x.DepartmentId == id);;

            if (department == null)
            {
                return true;
            }

            return false;
        }
    }
}