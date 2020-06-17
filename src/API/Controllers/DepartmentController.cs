using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Request;
using BLL.Services;
using DLL.Model;
using DLL.Repositories;
using LightQuery.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    public class DepartmentController : MainApiController
    {
        private readonly IDepartmentService _departmentService;


        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [AsyncLightQuery(forcePagination: true, defaultPageSize: 10, defaultSort: "departmentId desc")]
        [HttpGet]
        public IActionResult  GetAll()
        {
            
            return Ok( _departmentService.GetAllAsync());
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetA(string code)
        {
            return Ok(await _departmentService.GetAAsync(code));
        }
        
        
        [HttpPost]
        public async Task<IActionResult> Insert(DepartmentInsertRequestViewModel request)
        {
            return Ok(await _departmentService.InsertAsync(request));
        }
        
        [HttpPut("{code}")]
        public async Task<IActionResult> Update(string code,Department department)
        {
            return Ok(await _departmentService.UpdateAsync(code,department));
        }
        
        [HttpDelete("{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            return Ok(await _departmentService.DeleteAsync(code));
        }
        
    }

    
}